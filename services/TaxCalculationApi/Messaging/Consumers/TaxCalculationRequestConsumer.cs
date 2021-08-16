using System.Threading;
using System.Threading.Tasks;
using CitizensApi.Services;
using TaxCalculationApi.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class TaxCalculationRequestConsumer : ITypedConsumer<RequestTaxCalculationCreated>
{
    private readonly TaxesDbContext _context;
    public TaxCalculationRequestConsumer(TaxesDbContext context)
    {
        _context = context;
    }
    public async Task ConsumeAsync(RequestTaxCalculationCreated message, CancellationToken cancellationToken)
    {
        var citizenLocalitiesServices = new CitizenLocalitiesService();
        var citizenLocalities = citizenLocalitiesServices.GetMyLocalities(message.CitizenId);
        var taxRates = await _context.TaxRates.ToListAsync();
        var immobiles = await _context.Immobiles.ToListAsync();
        var totalTax = 0.0;
        foreach(var locality in citizenLocalities)
        {
            var rate = taxRates.FirstOrDefault(tr => tr.ImmobileTypeId == locality.LocationTypeId);
            var immobile = immobiles.FirstOrDefault(i => i.OwnerId == locality.CitizenId);
            totalTax += immobile.MarketValue * rate.Rate;
        }

        var taxCalculation = new TaxCalculation();
        //taxCalculation.Id = _context.TaxCalculations.Count() + 1;
        taxCalculation.RequestId = message.Id;
        taxCalculation.OwnerId = message.CitizenId;
        taxCalculation.TaxValue = totalTax;

        await _context.TaxCalculations.AddAsync(taxCalculation);
        //entity.State = EntityState.Added;
        await _context.SaveChangesAsync();
        

    }
}