using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        public async Task<IActionResult> DirectToHome()
        {
            return  RedirectToAction("Index", "Home", new { area = "Default" });
        }

        public async Task<IActionResult> TemplateBootstrap()
        {
            return RedirectToAction("Index", "TemplateBootstrap", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }

        [Route("Admin/About")]
        public async Task<IActionResult> About()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? false : true)
                return RedirectToAction("Index", "About", new { area = "AdminPanel" });

            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }


        [Route("Admin/ReciveInfoes")]
        public async Task<IActionResult> Reciveinfo()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? false : true)
                return RedirectToAction("Index", "ReciveInfoes", new { area = "AdminPanel" });

            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }


        //[Route("Admin/Galleries")]
        public async Task<IActionResult> Galleries()
        {
            if (HttpContext.Request.Cookies["userAuth"] == null ? false : true)
                return RedirectToAction("Index", "Galleries", new { area = "AdminPanel" });
            //return View("Galleries");
            //

            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }

        public async Task<IActionResult> index()
        {
            if( HttpContext.Request.Cookies["userAuth"] == null ? false : true )
                return View("Admin");

            return RedirectToAction("Index", "UserX", new { area = "Login" });
        }

    }
}
