using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Data;
using TestApp.Models;
using TestApp.Models.ViewModels;

namespace TestApp.Services
{
    public class FeeCalculationService : IFeeCalculationService
    {
        private readonly IFinancialItemService _financialItemService;
        private readonly IPartnerService _partnerService;

        public FeeCalculationService(IFinancialItemService financialItemService, IPartnerService partnerService)
        {
            _financialItemService = financialItemService;
            _partnerService = partnerService;
        }

        public List<FeeCalculationVM> CalculateFees()
        {
            var partnerList = _partnerService.GetAllPartners();
            return CalculateFees(partnerList);
        }

        private List<FeeCalculationVM> CalculateFees(List<Partner> partnerList)
        {
            List<FeeCalculationVM> feeCalculations = new();

            var financialItems = _financialItemService.GetAllFinancialItems();
            foreach (var partner in partnerList)
            {
                var directChildren = partnerList.Where(p => p.ParentPartnerId == partner.PartnerId).ToList();

                decimal teamPurhases = 0;
                decimal totalCommission = 0;

                decimal ownPurchases = financialItems.Where(p => p.PartnerId == partner.PartnerId).Sum(x => x.Amount);
                totalCommission += (partner.FeePercent / 100) * ownPurchases;

                if (directChildren.Count > 0)
                {
                    foreach (var child in directChildren)
                    {
                        Stack<decimal> partnerStack = new Stack<decimal>();
                        partnerStack.Push(child.PartnerId);
                        teamPurhases += CalculateTeamPurchases(partnerStack, partnerList, financialItems, 0);
                        totalCommission += partner.FeePercent > child.FeePercent ? ((partner.FeePercent - child.FeePercent) / 100) * teamPurhases : 0;
                    }
                }

                decimal totalPurchases = ownPurchases + teamPurhases;

                FeeCalculationVM feeCalculation = new();
                feeCalculation.PartnerId = partner.PartnerId;
                feeCalculation.ParentPartnerId = partner.ParentPartnerId;
                feeCalculation.PartnerName = partner.Name;
                feeCalculation.TeamPurchase = teamPurhases;
                feeCalculation.TotalPurchase = totalPurchases;
                feeCalculation.TotalCommission = totalCommission;

                feeCalculations.Add(feeCalculation);
            }

            return feeCalculations;
        }

        /*
         * Recursive function for calculating total of partner's team purchases
         */
        private decimal CalculateTeamPurchases(
            Stack<decimal> partnerStack,
            List<Partner> partnerList,
            List<FinancialItem> financialItems,
            decimal rootPartnerId,
            decimal sum = 0
            )
        {
            var childrenStack = new Stack<decimal>(partnerStack.Reverse());

            foreach (var item in childrenStack)
            {
                // Root partner purchases are not included in total
                sum += partnerStack.Count > 0 && partnerStack.First() != rootPartnerId ? financialItems.Where(p => p.PartnerId == partnerStack.First()).Sum(x => x.Amount) : 0;

                // Remove the item that is already included into the calculation
                partnerStack.Pop();

                var children = partnerList.Where(p => p.ParentPartnerId == item).ToList();
                if (children.Count > 0)
                {
                    foreach (var child in children)
                    {
                        partnerStack.Push(child.PartnerId);
                    }

                    return CalculateTeamPurchases(partnerStack, partnerList, financialItems, rootPartnerId, sum);
                }
            }

            return sum;
        }
    }
}
