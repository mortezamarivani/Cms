using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.Services.Repositories;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ReciveInfoesController : Controller
    {
        private IReciveInfoRepository _db;

        public ReciveInfoesController(IReciveInfoRepository db)
        {
            _db = db;
        }

        // GET: AdminPanel/ReciveInfoes
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            return View( _db.GetAllReciveInfo());
        }

        // GET: AdminPanel/ReciveInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var reciveInfo = _db.GetReciveInfo(id.Value);
            if (reciveInfo == null)
                return NotFound();
            return View(reciveInfo);
        }

        // GET: AdminPanel/ReciveInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/ReciveInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReciveInfoID,SenderName,SenderEmail,ReciveMessage,Status,CreatedDate")] ReciveInfo reciveInfo)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (ModelState.IsValid)
            {
                reciveInfo.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);

                _db.InsertReciveInfo(reciveInfo);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(reciveInfo);
        }

        // GET: AdminPanel/ReciveInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        // POST: AdminPanel/ReciveInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReciveInfoID,SenderName,SenderEmail,ReciveMessage,Status,CreatedDate")] ReciveInfo reciveInfo)
        {
            return View();
        }

        // GET: AdminPanel/ReciveInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var reciveInfo = _db.GetReciveInfo(id.Value);
            if (reciveInfo == null)
                return NotFound();

            return View(reciveInfo);
        }

        // POST: AdminPanel/ReciveInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            var reciveInfo =  _db.GetReciveInfo(id);
            _db.DeleteReciveInfo(reciveInfo);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciveInfoExists(int id)
        {
            return _db.ReciveInfoExists(id);
        }

    }
}
