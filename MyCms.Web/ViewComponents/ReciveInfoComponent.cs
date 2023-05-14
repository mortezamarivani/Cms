using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCms.DomainClasses.Config;
using MyCms.DomainClasses.ReciveInfo;
using MyCms.Services.Repositories;
using MyCms.Utilities.Convertor;

namespace MyCms.Web.ViewComponents
{
    public class ReciveInfoComponent : ViewComponent
    {
        IReciveInfoRepository _reciveinfoRepository;
        IConfigRepository _configRepository;
        public ReciveInfoComponent(IReciveInfoRepository reciveinfoRepository , IConfigRepository configRepository)
        {
            _reciveinfoRepository = reciveinfoRepository;
            _configRepository = configRepository;
        }

        [HttpPost]
        public void CreateReciveInfo([Bind("SenderName,SenderEmail,ReciveMessage")] ReciveInfo ReciveInfo)
        {
            //if (ModelState.IsValid)
            //{
            ReciveInfo.CreatedDate = DateConvertor.ToIntShamsi(DateTime.Now);

            _reciveinfoRepository.InsertReciveInfo(ReciveInfo);
            _reciveinfoRepository.Save();
            //}
            //return View("Index");
            //return RedirectToAction("Index", "Home", new { area = "Default" });

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)
                View("ReciveInfoComponent", _reciveinfoRepository.GetAllReciveInfo()) );
        }

    }
}
