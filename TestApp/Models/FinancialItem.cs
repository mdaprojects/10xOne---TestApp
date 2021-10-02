using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class FinancialItem
    {
        public decimal Id { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!")]
        public decimal Amount { get; set; }
        [Required]
        [DisplayName("Partner")]
        public decimal PartnerId { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
