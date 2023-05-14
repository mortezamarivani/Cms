using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCms.DomainClasses.Rank;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Immutable;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RankController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        [Route("Doc/{DocName}")]
        public IActionResult GetDoc(string DocName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Doc\" + DocName;
            if (System.IO.File.Exists(imageaddress ) )
            {
                var image = System.IO.File.OpenRead(imageaddress);
                response.Content = new StreamContent(image);

                return File(image, "image/jpg");
            }

            return NotFound();
        }

        

        [Route("GetRankByPageId/{pageinfo}")]
        public IEnumerable<Rank> GetRankByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.Rank.Where(n=> n.Type == 1).OrderBy(n => n.RankID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("RankCount")]
        public int RankCount()
        {
            return _context.Rank.Where(n => n.Type == 1).Count() / 4;
        }

        [HttpGet]
        [Route("AllRankCount")]
        public int AllRankCount()
        {
            return _context.Rank.Where(n => n.Type == 1).Count();
        }


        // GET: api/Gallery
        //[Authorize]
        [HttpGet("GetAllRank")]
        public IEnumerable<Rank> GetAllRank()
       {
            var userid = GetUserId();
            return _context.Rank.Where(n => n.Type == 1);
        }



        // GET: api/Rank/5
        //[Authorize]
        [HttpGet("GetRankWithId/{id:int}")]
        public async Task<IActionResult> GetRankWithId([FromRoute] int id )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Rank = await _context.Rank.Where(n=> n.Type== 1 && n.RankID==id).FirstAsync();

            if (Rank == null)
                return NotFound();
            return Ok(Rank);
        }

        // PUT: api/Gallery/5
        //[Authorize]
        [HttpPut("EditRank/{id:int}")]
        public async Task<IActionResult> EditRank([FromRoute] int id, [FromBody] Rank Rank)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != Rank.RankID)
                return BadRequest();

            _context.Entry(Rank).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RankExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpPost("CreateRankWithimage")]
        public async Task<IActionResult> CreateRankWithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Rank rank = new Rank();
                rank.RankName = data["rankName"];
                rank.RankDesc = data["rankDesc"]; 
                rank.Type = Convert.ToInt32(data["type"]);
                rank.SuffixFile = data["suffixFile"];
                rank.Status = Convert.ToBoolean(data["status"]);
                rank.CreatedDate = Convert.ToInt32(data["createdDate"]);
                rank.Languge = Convert.ToInt32(data["languge"]);
                rank.Institute = data["institute"];
                rank.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                rank.DocName = Guid.NewGuid().ToString() + data["suffixFile"];
                rank.EndDate = Convert.ToInt32(data["endDate"]);
                rank.StartDate = Convert.ToInt32(data["startDate"]);
                rank.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Doc\" + rank.DocName;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(imageaddress, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                var userid = GetUserId();
                _context.Rank.Add(rank);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetRankWithId", new { id = rank.RankID }, rank);
            }
            return BadRequest();
        }

        [HttpPut("editerankwithimage")]
        public async Task<IActionResult> editerankwithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                //if (id != Convert.ToInt32(data["galleryID"]) )
                //    return BadRequest();

                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Rank rank = new Rank();
                rank.RankID = Convert.ToInt32(data["rankID"]);
                rank.RankName = data["rankName"]; 
                rank.Type = Convert.ToInt32(data["type"]);
                rank.RankDesc = data["rankDesc"];
                rank.SuffixFile = data["suffixFile"];
                rank.Status = Convert.ToBoolean(data["status"]);
                rank.CreatedDate = Convert.ToInt32(data["createdDate"]);
                rank.Languge = Convert.ToInt32(data["languge"]);
                rank.Institute = data["institute"];
                rank.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                rank.DocName = Guid.NewGuid().ToString() + data["suffixFile"];
                rank.EndDate = Convert.ToInt32(data["endDate"]);
                rank.StartDate = Convert.ToInt32(data["startDate"]);
                rank.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\doc\" + rank.DocName;

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(imageaddress, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                }
                _context.Entry(rank).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetRankWithId", new { id = rank.RankID }, rank);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankExists(rank.RankID))
                        return NotFound();
                    else
                        return BadRequest();
                }
            }
            return BadRequest();

        }



        // POST: api/Gallery
        //[Authorize]
        [HttpPost("CreateRank")]
        public async Task<IActionResult> CreateRank([FromBody] Rank rank, IFormFile imgup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.Rank.Add(rank);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRankWithId", new {  id = rank.RankID }, rank);
            //return Ok(Gallery);
        }

        // DELETE: api/Gallery/5
        //[Authorize]
        [HttpDelete("DeleteRank/{id:int}")]
        public async Task<IActionResult> DeleteRank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != Gallery.GalleryID)
            //    return BadRequest();

            var Rank = await _context.Rank.FindAsync(id);
            if (Rank == null)
                return NotFound();
            _context.Rank.Remove(Rank);
            await _context.SaveChangesAsync();

            return Ok(Rank);
        }

        private bool RankExists(int id)
        {
            return _context.Rank.Any(e => e.RankID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}