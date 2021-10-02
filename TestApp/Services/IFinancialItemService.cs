using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.Models.ViewModels;

namespace TestApp.Services
{
    public interface IFinancialItemService
    {
        List<FinancialItem> GetAllFinancialItems();
        FinancialItem GetFinancialItemById(decimal id);
        void CreateFinancialItem(FinancialItem item);
        void UpdateFinancialItem(FinancialItem item);
        void DeleteFinancialItem(decimal id);
        void DeleteAll();
        void SeedData();
    }
}
