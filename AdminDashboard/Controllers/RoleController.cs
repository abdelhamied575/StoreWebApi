using AdminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace AdminDashboard.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task <IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }


        [HttpPost]
        public async Task<IActionResult>Create(RoleFormViewModel role)
        {

            if(ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role.Name);

                if(roleExists)
                {
                    ModelState.AddModelError("Name", "This Role already exist");
                    return RedirectToAction("Index"); 
                }

                await _roleManager.CreateAsync(new IdentityRole { Name=role.Name.Trim()});

                return RedirectToAction("Index");


            }

            return RedirectToAction("Index");



        }



        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role is not null)
            {
                await _roleManager.DeleteAsync(role);

            }

            return RedirectToAction("Index");

        }



        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var mappedRole = new RoleViewModel { Id = role.Id, Name = role.Name.Trim() };

            return View(mappedRole);


        }
          [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {

            if (ModelState.IsValid)
            {
                var roleExists = await _roleManager.RoleExistsAsync(model.Name);

                if (roleExists)
                {
                    ModelState.AddModelError("Name", "This Role already exist");
                    return RedirectToAction("Index");
                }

                var role = await _roleManager.FindByIdAsync(model.Id);

                role.Name = model.Name.Trim();

                await _roleManager.UpdateAsync(role);


                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");




        }





    }
}
