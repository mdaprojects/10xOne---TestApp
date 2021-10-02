using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Models.ViewModels;

namespace TestApp.Services
{
    public interface IFeeCalculationService
    {
        List<FeeCalculationVM> CalculateFees();
    }
}
