using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models.ViewModels
{
    public class FinancialItemVM
    {
        public FinancialItem FinancialItem { get; set; }
        public IEnumerable<SelectListItem> PartnerDropDown { get; set; }
    }
}
