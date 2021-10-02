using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models.ViewModels
{
    public class FeeCalculationVM
    {
        public decimal PartnerId { get; set; }
        public decimal? ParentPartnerId { get; set; }
        public string PartnerName { get; set; }
        public decimal TeamPurchase { get; set; }
        public decimal TotalPurchase { get; set; }
        public decimal TotalCommission { get; set; }
    }
}
