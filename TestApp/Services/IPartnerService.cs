using System.Collections.Generic;
using TestApp.Models;

namespace TestApp.Services
{
    public interface IPartnerService
    {
        List<Partner> GetAllPartners();
        void SeedData();
    }
}
