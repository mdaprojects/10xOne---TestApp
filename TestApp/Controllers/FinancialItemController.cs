using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TestApp.Data;
using TestApp.Models;
using TestApp.Models.ViewModels;
using TestApp.Services;

namespace TestApp.Controllers
{
    public class FinancialItemController : Controller
    {
        private readonly IPartnerService _partnerService;
        private readonly IFinancialItemService _financialItemService;

        public FinancialItemController(IPartnerService partnerService, IFinancialItemService financialItemService)
        {
            _partnerService = partnerService;
            _financialItemService = financialItemService;
        }
        public IActionResult Index()
        {
            if (_partnerService.GetAllPartners().Count == 0)
            {
                _partnerService.SeedData();
            }
            List<FinancialItem> financialItems = _financialItemService.GetAllFinancialItems();
            return View(financialItems);
        }

        // GET-Creates
        public IActionResult Create()
        {
            FinancialItemVM financialItemVM = new FinancialItemVM()
            {
                FinancialItem = new FinancialItem(),
                PartnerDropDown = _partnerService.GetAllPartners().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.PartnerId.ToString()
                })
            };

            return View(financialItemVM);
        }

        // POST-Create
        [HttpPost]
        public IActionResult Create(FinancialItemVM model)
        {
            if (ModelState.IsValid)
            {
                _financialItemService.CreateFinancialItem(model.FinancialItem);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET Update
        public IActionResult Update(decimal? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            FinancialItemVM finacialItemVM = new FinancialItemVM()
            {
                FinancialItem = _financialItemService.GetFinancialItemById((decimal)id),
                PartnerDropDown = _partnerService.GetAllPartners().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.PartnerId.ToString()
                })
            };

            if (finacialItemVM.FinancialItem == null)
            {
                return NotFound();
            }

            return View(finacialItemVM);

        }

        // POST UPDATE
        [HttpPost]
        public IActionResult Update(FinancialItemVM model)
        {
            if (ModelState.IsValid)
            {
                _financialItemService.UpdateFinancialItem(model.FinancialItem);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET Delete
        public IActionResult Delete(decimal? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var financialItem = _financialItemService.GetFinancialItemById((decimal)id);
            if (financialItem == null)
            {
                return NotFound();
            }
            return View(financialItem);
        }

        // POST Delete
        [HttpPost]
        public IActionResult DeleteItem(decimal id)
        {
            _financialItemService.DeleteFinancialItem(id);
            return RedirectToAction("Index");
        }

        // POST Delete
        [HttpPost]
        public IActionResult DeleteAll()
        {
            _financialItemService.DeleteAll();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SeedData()
        {
            _financialItemService.SeedData();
            return RedirectToAction("Index");
        }
    }
}
