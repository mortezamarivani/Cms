using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Skills;
using MyCms.Services.Repositories;

namespace MyCms.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SkillsController : Controller
    {
        private ISkillsRepoitory _Iskills;
        public SkillsController(ISkillsRepoitory skillsRepoitory)
        {
            _Iskills = skillsRepoitory;
        }

        public async Task<IActionResult> DirectToHome()
        {
            return  RedirectToAction("Index", "Home", new { area = "Default" });
        }
        
        public async Task<IActionResult> TemplateBootstrap()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }

        public async Task<IActionResult> About()
        {
            return RedirectToAction("Index", "Abouts", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Reciveinfo()
        {
            return RedirectToAction("Index", "ReciveInfoes", new { area = "AdminPanel" });
        }

        [Route("/Galleries")]
        public async Task<IActionResult> Galleries()
        {
            return RedirectToAction("Index", "Galleries", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Admin()
        {
            return View();
        }

        // GET: AdminPanel/Skills
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            var skills = _Iskills.GetAllSkills();
            return View(skills);
        }

        // GET: AdminPanel/Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
            {
                return NotFound();
            }

            var skills =  _Iskills.GetSkillsById(id.Value);
                
            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        // GET: AdminPanel/Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Skills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkillsID,SkillsTitle,SkillsDescription,BootstarpClassName,Progress,Status,Languge")] Skills skills)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (ModelState.IsValid)
            {
                _Iskills.InsertSkills(skills);
                _Iskills.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        //GET: AdminPanel/Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var skills = _Iskills.GetSkillsById(id.Value);
            if (skills == null)
            {
                return NotFound();
            }
            return View(skills);
        }

        // POST: AdminPanel/Skills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkillsID,SkillsTitle,SkillsDescription,BootstarpClassName,Progress,Status,Languge")] Skills skills)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id != skills.SkillsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _Iskills.UpdateSkills(skills);
                    _Iskills.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_Iskills.SkillsExists(skills.SkillsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(skills);
        }

        // GET: AdminPanel/Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
            {
                return NotFound();
            }

            var skills = _Iskills.GetSkillsById(id.Value);

            if (skills == null)
            {
                return NotFound();
            }

            return View(skills);
        }

        //// POST: AdminPanel/Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            _Iskills.DeleteSkills(id);
            _Iskills.Save();
           return RedirectToAction(nameof(Index));
        }

        private bool SkillsExists(int id)
        {
            return _Iskills.SkillsExists(id);
        }
    }
}
