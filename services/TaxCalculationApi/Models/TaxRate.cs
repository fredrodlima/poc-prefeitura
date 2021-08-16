namespace TaxCalculationApi.Models
{
    public class TaxRate
    {
        public int Id { get; set; }
        public int ImmobileTypeId { get; set; }
        public double Rate { get; set; }
    }
}