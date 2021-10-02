using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public class Partner
    {
        public decimal PartnerId { get; set; }
        public string Name { get; set; }
        public decimal FeePercent { get; set; }
        public decimal? ParentPartnerId { get; set; }
    }
}
