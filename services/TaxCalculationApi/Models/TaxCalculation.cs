using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxCalculationApi.Models
{
    public class TaxCalculation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid RequestId { get; set; }
        public int OwnerId { get; set; }
        public double TaxValue {get; set;}
    }
}