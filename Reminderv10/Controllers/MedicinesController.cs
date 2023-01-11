using Reminderv10.Models;
using Reminderv10.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NuGet.DependencyResolver;

namespace Reminderv10.Controllers
{
    [Authorize]
    public class MedicinesController : Controller
    {
        private readonly MedicineService _medicineService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MedicinesController(MedicineService medicineService, UserManager<ApplicationUser> userManager)
        {
            _medicineService = medicineService;
            _userManager = userManager;

        }

        [AllowAnonymous]
        public ActionResult<List<Medicine>> Index() {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_medicineService.Get(userId));
        }
        

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Medicine>> Create(Medicine medicine)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            medicine.OwnerID = userId;

            

            if (ModelState.IsValid)
            {
                _medicineService.Create(medicine);
               
            }
            return RedirectToAction("Index");
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Medicine medicineIn)
        {
            if (ModelState.IsValid)
            {
                _medicineService.Update(id,medicineIn);
                return RedirectToAction("Index");
            }
            return View(medicineIn);
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            _medicineService.Remove(id);
            return RedirectToAction("Index");
        }

    }
}