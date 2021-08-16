using System;

namespace CitizensApi.Models.TaxCalculation
{
    public class RequestTaxCalculationCreated
    {
        public Guid Id { get; set; }
        public int CitizenId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
