using System.Collections.Generic;
using TestApp.Data;
using TestApp.Models;

namespace TestApp.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly PartnerData _partnerData;

        public PartnerService(PartnerData partnerData)
        {
            _partnerData = partnerData;
        }

        public List<Partner> GetAllPartners()
        {
            return _partnerData.GetAllPartners();
        }

        public void SeedData()
        {
            _partnerData.Seed();
        }
    }
}
