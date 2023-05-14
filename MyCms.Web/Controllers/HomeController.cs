using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCms.DomainClasses.Config;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.Services.Repositories;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.Controllers
{
    public class HomeController : Controller
    {
        private IReciveInfoRepository _db;
        private IConfigRepository _Config;

        public HomeController(IReciveInfoRepository context , IConfigRepository Config)
        {
            _db = context;
            _Config = Config;
        }

        private int _languge = 99;

        public IActionResult Index()
        {
            @ViewBag.Languge = Lang;

            List<Config> configs = new List<Config>();
            configs = _Config.GetConfig();
            @ViewBag.ShowReciveInfo = configs[0].ShowReciveInfo;
            @ViewBag.ShowHome = configs[0].ShowHome;
            @ViewBag.ShowGallery = configs[0].ShowGallery;
            @ViewBag.ShowProject = configs[0].ShowProject;
            @ViewBag.ShowSkills = configs[0].ShowSkills;
            @ViewBag.ShowRank = configs[0].ShowRank;
            @ViewBag.ShowContactMe = configs[0].ShowContactMe;
            @ViewBag.ShowIDO = configs[0].ShowIDO;
            @ViewBag.ShowCours = configs[0].ShowCours;
            @ViewBag.ShowEducation = configs[0].ShowEducation;
                
            return View();
        }
        
        public IActionResult Home()
        {
            return View();
        }

        [HttpPost]
        public IActionResult About()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SeGermanLanguge([FromRoute]string lan)
        {
            Lang = (int)MyCms.Utilities.Enum.CommonEnum.LangugeEnum.De;
            @ViewBag.Languge = Lang;

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult SetEnglishLanguge([FromRoute]string lan)
        {
            Lang = (int)MyCms.Utilities.Enum.CommonEnum.LangugeEnum.En;
            @ViewBag.Languge = Lang;

            return RedirectToAction("Index", "Home");
        }

        public void SetLanguge(int languge)
        {
            _languge = languge;
            _Config.UpdateConfig(new Config
            {
                Languge = _languge,
                ConfigID=1
            });
            _Config.Save();

            @ViewBag.Languge = _languge;
        }

        [TempData]
        public int Lang { get; set; } = 1;

        [HttpPost]
        public IActionResult SetPersianLanguge()
        {
            Lang = (int)MyCms.Utilities.Enum.CommonEnum.LangugeEnum.Per;
            @ViewBag.Languge = Lang;

            //SetLanguge((int)MyCms.Utilities.Enum.CommonEnum.LangugeEnum.Persian);

            return RedirectToAction("Index", "Home" , _languge);
        }

        string GetCookieValueFromResponse(HttpResponse response, string cookieName)
        {
            foreach (var headers in response.Headers.Values)
                foreach (var header in headers)
                    if (header.StartsWith($"{cookieName}="))
                    {
                        var p1 = header.IndexOf('=');
                        var p2 = header.IndexOf(';');
                        return header.Substring(p1 + 1, p2 - p1 - 1);
                    }
            return null;
        }

        [HttpPost]
        public IActionResult CreateReciveInfo([Bind("SenderName,SenderEmail,ReciveMessage")] ReciveInfo ReciveInfo)
        {
            if (ModelState.IsValid)
            {
                ReciveInfo.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);

                _db.InsertReciveInfo(ReciveInfo);
                _db.Save();
            }
            return RedirectToAction("Index", "Home");
        }

    }


}