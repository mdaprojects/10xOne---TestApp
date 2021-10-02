
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Helpers;
using TestApp.Models;

namespace TestApp.Data
{
    public class PartnerData
    {
        IHttpContextAccessor _httpContextAccessor;
        public PartnerData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Seed()
        {
            List<Partner> partners = new();

            for (int i = 1; i <= 15; i++)
            {
                Partner partner = new();
                partner.PartnerId = Convert.ToDecimal(i);
                partner.Name = "Partner_" + i.ToString();
                partner.FeePercent = RandomNumber.Decimal(0, 20);
                partner.ParentPartnerId = GetParentPartnerRandomId(partners);

                partners.Add(partner);
            }

            _httpContextAccessor.HttpContext.Session.SetString("PartnerList", JsonConvert.SerializeObject(partners));
        }

        private static decimal? GetParentPartnerRandomId(List<Partner> partners)
        {
            var partnerCount = partners.Count();
            if (partnerCount != 0)
            {
                return partners[RandomNumber.Int(0, partnerCount)].PartnerId;
            }

            return null;
        }

        public List<Partner> GetAllPartners()
        {
            if (_httpContextAccessor.HttpContext.Session.GetString("PartnerList") != null)
            {
                return JsonConvert.DeserializeObject<List<Partner>>(_httpContextAccessor.HttpContext.Session.GetString("PartnerList"));
            }

            return new List<Partner>();
        }
    }
}
