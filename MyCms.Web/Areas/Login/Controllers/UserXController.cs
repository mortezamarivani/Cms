using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.UserX;
using MyCms.Services.Repositories;

namespace MyCms.Web.Areas.Login.Controllers
{
    [Area("Login")]
    public class UserXController : Controller
    {
        private IUserXRepository _db;
        public UserXController(IUserXRepository db)
        {
            _db = db;
        }
        // GET: Login/UserXes
        public async Task<IActionResult> Index()
        {
            return View("Login");
        }

        public async Task<IActionResult> CheckUser(UserX user)
        {
            //if(!ModelState.IsValid)
            //    return View("Login");

            bool ValidatUser = UserXExists(user.UserName , user.Password);

            Response.Cookies.Append("userAuth", ValidatUser.ToString(),
            new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(20),
                    Path = "/"
                }
                );

            if (ValidatUser)
                return RedirectToAction("Index", "Admin", new { area = "AdminPanel"  });
            return View("Login");
        }
      
        private bool UserXExists(string user , string pass)
        {
            return _db.UserExists(user, pass);
        }

        //// GET: Login/UserXes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userX = await _context.UserX
        //        .FirstOrDefaultAsync(m => m.UserID == id);
        //    if (userX == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userX);
        //}

        //// GET: Login/UserXes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Login/UserXes/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserID,UserName,Password,Name,Family,Email,Status")] UserX userX)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(userX);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userX);
        //}

        //// GET: Login/UserXes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userX = await _context.UserX.FindAsync(id);
        //    if (userX == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(userX);
        //}

        //// POST: Login/UserXes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Password,Name,Family,Email,Status")] UserX userX)
        //{
        //    if (id != userX.UserID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userX);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserXExists(userX.UserID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userX);
        //}

        //// GET: Login/UserXes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userX = await _context.UserX
        //        .FirstOrDefaultAsync(m => m.UserID == id);
        //    if (userX == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userX);
        //}

        //// POST: Login/UserXes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var userX = await _context.UserX.FindAsync(id);
        //    _context.UserX.Remove(userX);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
