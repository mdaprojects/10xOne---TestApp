using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Helpers;
using TestApp.Models;

namespace TestApp.Data
{
    public class FinancialItemsData
    {
        IHttpContextAccessor _httpContextAccessor;
        public FinancialItemsData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<FinancialItem> GetAllFinancialItems()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("FinancialItems") != null)
            {
                return JsonConvert.DeserializeObject<List<FinancialItem>>(_httpContextAccessor.HttpContext.Session.GetString("FinancialItems"));
            }
            
            return new List<FinancialItem>();
        }

        public FinancialItem GetFinancialItemById(decimal id)
        {
            return GetAllFinancialItems().Find(x => x.Id == id);
        }

        public void CreateFinancialItem(FinancialItem item)
        {
            List<FinancialItem> financialItems = GetAllFinancialItems();

            IEnumerable<Partner> partnerList = JsonConvert.DeserializeObject<IEnumerable<Partner>>(_httpContextAccessor.HttpContext.Session.GetString("PartnerList"));

            item.Partner = partnerList.FirstOrDefault(x => x.PartnerId == item.PartnerId);
            item.Id = financialItems.Count() > 0 ? financialItems.Max(x => x.Id) + 1 : 1;



            financialItems.Add(item);

            _httpContextAccessor.HttpContext.Session.SetString("FinancialItems", JsonConvert.SerializeObject(financialItems));
        }

        public void UpdateFinancialItem(FinancialItem item)
        {
            List<FinancialItem> financialItems = GetAllFinancialItems();
            financialItems.Where(x => x.Id == item.Id)
                .Select(x => { x.PartnerId = item.PartnerId; return x; })
                .Select(x => { x.Date = item.Date; return x; })
                .Select(x => { x.Amount = item.Amount; return x; }).ToList();

            _httpContextAccessor.HttpContext.Session.SetString("FinancialItems", JsonConvert.SerializeObject(financialItems));
        }

        public void DeleteFinancialItem(decimal id)
        {
            List<FinancialItem> financialItems = GetAllFinancialItems();
            financialItems.RemoveAll(x => x.Id == id);

            _httpContextAccessor.HttpContext.Session.SetString("FinancialItems", JsonConvert.SerializeObject(financialItems));
        }

        public void DeleteAll()
        {
            List<FinancialItem> financialItems = new List<FinancialItem>();
            _httpContextAccessor.HttpContext.Session.SetString("FinancialItems", JsonConvert.SerializeObject(financialItems));
        }

        public void Seed()
        {
            IEnumerable<Partner> partnerList = JsonConvert.DeserializeObject<IEnumerable<Partner>>(_httpContextAccessor.HttpContext.Session.GetString("PartnerList"));
            List<FinancialItem> items = new();

            int counter = 1;

            foreach (var partner in partnerList)
            {
                for (int i = 0; i <= RandomNumber.Int(1, 4); i++)
                {
                    FinancialItem item = new();
                    item.Id = counter;
                    item.PartnerId = partner.PartnerId;
                    item.Partner = partner;
                    item.Date = DateTime.Now.AddDays(-(RandomNumber.Int(1, 20)));
                    item.Amount = RandomNumber.Decimal(10, 200);

                    items.Add(item);
                    counter++;
                }
            }

            _httpContextAccessor.HttpContext.Session.SetString("FinancialItems", JsonConvert.SerializeObject(items));
        }
    }
}
