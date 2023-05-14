using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Gallery;
using MyCms.Services.Repositories;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class GalleriesController : Controller
    {
        private IGalleryRepository _db;

        public GalleriesController(IGalleryRepository context)
        {
            _db = context;
        }

        // GET: AdminPanel/Galleries
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            return View(_db.GetAllGallery((int)MyCms.Utilities.Enum.CommonEnum.LangugeEnum.En));
        }

        // GET: AdminPanel/Galleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var gallery = _db.GetGallery(id.Value);
            if (gallery == null)
                return NotFound();

            return View(gallery);
        }

        // GET: AdminPanel/Galleries/Create
        public IActionResult Create()
        {

            ViewBag.PicRow = _db.GetMaxPicRow() + 1;

            return View();
        }

        // POST: AdminPanel/Galleries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow,Languge")] Gallery gallery,IFormFile imgup)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (ModelState.IsValid)
            {
                string suffix;
                gallery.PicRow = _db.GetMaxPicRow()+1;
                if (imgup !=null)
                {

                    gallery.GalleryName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                    gallery.SuffixFile = Path.GetExtension(imgup.FileName);
                    string savepath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot/Gallery",gallery.GalleryName);

                    using (var stream = new FileStream(savepath, FileMode.Create))
                    {
                      await  imgup.CopyToAsync(stream);
                    }
                }

                gallery.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);
                _db.InsertGallery(gallery);
                _db.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: AdminPanel/Galleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var gallery = _db.GetGallery(id.Value);
            if (gallery == null)
                return NotFound();

            return View(gallery);
        }

        // POST: AdminPanel/Galleries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow,Languge")] Gallery gallery,IFormFile imgup)
        {
            if (id != gallery.GalleryID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (imgup != null)
                    {
                        gallery.GalleryName = Guid.NewGuid().ToString() + Path.GetExtension(imgup.FileName);
                        gallery.SuffixFile = Path.GetExtension(imgup.FileName);
                        string savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Gallery", gallery.GalleryName);

                        using (var stream = new FileStream(savepath, FileMode.Create))
                        {
                            await imgup.CopyToAsync(stream);
                        }
                    }
                    gallery.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);
                    
                    _db.UpdateGallery(gallery);
                    _db.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.GalleryID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gallery);
        }

        // GET: AdminPanel/Galleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true : false)
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            if (id == null)
                return NotFound();

            var gallery = _db.GetGallery(id.Value);
            if (gallery == null)
                return NotFound();

            return View(gallery);
        }

        // POST: AdminPanel/Galleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,IFormFile imgup)
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? true  : false )
                return RedirectToAction("Index", "UserX", new { area = "Login" });

            var gallery = _db.GetGallery(id);
            if(gallery.GalleryName != null)
            {
                string savepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Gallery", gallery.GalleryName);
                if (System.IO.File.Exists(savepath))
                    System.IO.File.Delete(savepath);
            }
            _db.DeleteGallery(gallery);
            _db.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool GalleryExists(int id)
        {
            return _db.GalleryExists(id);
        }
    }
}
