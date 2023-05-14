using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCms.Services.Repositories;
using MyCms.ViewModels.Polls;

namespace MyCms.Web.Areas.Polls.Controllers
{
    [Area("Polls")]
    public class PollsController : Controller
    {
        private IPollsRepository _db;
        private readonly IHttpContextAccessor httpContextAccessor;
        public PollsController(IPollsRepository db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            this.httpContextAccessor = httpContextAccessor;
        }
        // GET: Login/UserXes
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShowPollsViewModel> polls = getAllPolls();
            return View("Polls", polls);
        }

        public async Task<IActionResult> CheckPolls(MyCms.DomainClasses.Polls.Polls PI_Polls)
        {
            PersianCalendar jc = new PersianCalendar();
            DateTime thisDate = DateTime.Now;
            string m = jc.GetMonth(thisDate).ToString();
            string y = jc.GetYear(thisDate).ToString();
            string d = jc.GetDayOfMonth(thisDate).ToString();
            string remoteIpAddress = "";
            try
            {
                remoteIpAddress = this.httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                 ViewBag.IP = remoteIpAddress;

            if (m.Length < 2)
                m = "0" + m;

            if (d.Length < 2)
                d = "0" + d;

            string strCreateDate = y + m + d;

            PI_Polls.CreateDate = Convert.ToInt32(strCreateDate);
            PI_Polls.Ip = remoteIpAddress;
            ViewBag.Success = "";
            ViewBag.Err = "";

            if (!pollsExists(PI_Polls.Ip))
            {
                ViewBag.Err = "";
                addPolls(PI_Polls);
                ViewBag.Success = "رای شما با موفقیت ثبت شد";
                
            }
            else
            {
                ViewBag.Success = "";
                ViewBag.Err = "قبلا رای خود را ارسال کرده اید";
            }

            ViewBag.IP2 += "IP5";
            IEnumerable<ShowPollsViewModel> polls = getAllPolls();
                return View("Polls", polls);
            }
            catch (Exception)
            {

                throw;
            }
            return View("Polls");
        }
      
        private bool pollsExists(string ip)
        {
            return _db.PollsExists(ip);
        }

        private void addPolls(MyCms.DomainClasses.Polls.Polls PI_Polls)
        {
             _db.InsertPolls(PI_Polls);
            _db.Save();
        }

        private IEnumerable<ShowPollsViewModel> getAllPolls()
        {
            return _db.GetPolls();
        }


    }
}
