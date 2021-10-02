using System.Collections.Generic;
using TestApp.Data;
using TestApp.Models;

namespace TestApp.Services
{
    public class FinancialItemService : IFinancialItemService
    {
        private readonly FinancialItemsData _financialItemsData;

        public FinancialItemService(FinancialItemsData financialItemsData)
        {
            _financialItemsData = financialItemsData;
        }

        public List<FinancialItem> GetAllFinancialItems()
        {
            List<FinancialItem> financialItems = _financialItemsData.GetAllFinancialItems();
            return financialItems;
        }

        public void CreateFinancialItem(FinancialItem item)
        {
            _financialItemsData.CreateFinancialItem(item);
        }

        public void DeleteFinancialItem(decimal id)
        {
            _financialItemsData.DeleteFinancialItem(id);
        }

        public void DeleteAll()
        {
            _financialItemsData.DeleteAll();
        }

        public FinancialItem GetFinancialItemById(decimal id)
        {
            return _financialItemsData.GetFinancialItemById((decimal)id);
        }

        public void UpdateFinancialItem(FinancialItem item)
        {
            _financialItemsData.UpdateFinancialItem(item);
        }

        public void SeedData()
        {
            _financialItemsData.Seed();
        }
    }
}
