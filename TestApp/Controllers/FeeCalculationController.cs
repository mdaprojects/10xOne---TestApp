using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Data;
using TestApp.Models;
using TestApp.Models.ViewModels;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class FeeCalculationController : Controller
    {
        private readonly FinancialItemsData _financialItemsData;
        private readonly PartnerData _partnerData;
        private readonly IFeeCalculationService _feeCalculationService;
        

        public FeeCalculationController(IFeeCalculationService feeCalculationService)
        {
            _feeCalculationService = feeCalculationService;
        }

        public IActionResult Index()
        {
            var feeCalculations = _feeCalculationService.CalculateFees();
            return View(feeCalculations);
        }
    }
}
