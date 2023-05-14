using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.About;
using MyCms.Services.Repositories;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AboutsController : Controller
    {

        private IAboutRepository _about;
        public AboutsController(IAboutRepository about)
        {
            _about = about;
        }

        // GET: About/Abouts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            return View(_about.GetAllAboute(99));

        }

        // GET: About/Abouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var about = _about.GetAboutById(id.Value);
            if (about == null)
                return NotFound();

            return View(about);
        }

        // GET: About/Abouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: About/Abouts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutID,Name,HomeDesc,Mobile,TelPhon,Email,Address,AboutHeader,AboutTitle,AboutDescription,InstagramID,LinkdinAddress,TwitterAddress,FacebookAddress,Status,CreatedDate,CreatorUserID,Languge")]MyCms.DomainClasses.About.About about)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (ModelState.IsValid)
            {
                about.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);
                _about.InsertAboute(about);
                _about.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: About/Abouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var about = _about.GetAboutById(id.Value);
            if (about == null)
                return NotFound();

            return View(about);
        }

        // POST: About/Abouts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AboutID,Name,HomeDesc,Mobile,TelPhon,Email,Address,AboutHeader,AboutTitle,AboutDescription,InstagramID,LinkdinAddress,TwitterAddress,FacebookAddress,Status,CreatedDate,CreatorUserID,Languge")] MyCms.DomainClasses.About.About about)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id != about.AboutID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    about.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);

                    _about.UpdateAboute(about);
                    _about.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.AboutID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(about);
        }

        // GET: About/Abouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var about = _about.GetAboutById(id.Value);
            if (about == null)
                return NotFound();

            //_about.DeleteAboute(about);
            //_about.Save();

            return View(about);
        }

        // POST: About/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            _about.DeleteAboute(id);
            _about.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutExists(int id)
        {
            return _about.AbouteExists(id);
        }
    }
}
