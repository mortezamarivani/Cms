using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Gallery;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleryController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GalleryController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        [Route("Image/{imageName}")]
        public IActionResult GetImage(string imageName)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Gallery\" + imageName;
            if (System.IO.File.Exists(imageaddress ) )
            {
                var image = System.IO.File.OpenRead(imageaddress);
                response.Content = new StreamContent(image);

                return File(image, "image/jpg");
            }

            return NotFound();
        }



        [Route("GetGaleryByPageId/{pageinfo}")]
        public IEnumerable<Gallery> GetGaleryByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.Gallery.OrderBy(n => n.GalleryID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("GalleryCount")]
        public int GalleryCount()
        {
            return _context.Gallery.Count() / 4;
        }

        [HttpGet]
        [Route("AllGalleryCount")]
        public int AllSkillsCount()
        {
            return _context.Gallery.Count();
        }


        // GET: api/Gallery
        //[Authorize]
        [HttpGet("GetAllGallery")]
        public IEnumerable<Gallery> GetAllGallery()
       {
            var userid = GetUserId();
            return _context.Gallery;
        }



        // GET: api/Gallery/5
        //[Authorize]
        [HttpGet("GetGalleryWithId/{id:int}")]
        public async Task<IActionResult> GetGalleryWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Gallery = await _context.Gallery.FindAsync(id);

            if (Gallery == null)
                return NotFound();
            return Ok(Gallery);
        }

        // PUT: api/Gallery/5
        //[Authorize]
        [HttpPut("EditGallery/{id:int}")]
        public async Task<IActionResult> EditGallery([FromRoute] int id, [FromBody] Gallery Gallery)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != Gallery.GalleryID)
                return BadRequest();

            _context.Entry(Gallery).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GalleryExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpPost("CreateGalleryWithimage")]
        public async Task<IActionResult> CreateGalleryWithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Gallery gallery = new Gallery();
                gallery.GalleryName = Guid.NewGuid().ToString() + data["suffixFile"];
                gallery.GalleryDesc = data["galleryDesc"];
                gallery.SuffixFile = data["suffixFile"];
                gallery.PicRow = Convert.ToInt32(data["picRow"]);
                gallery.Status = Convert.ToBoolean(data["status"]);
                gallery.CreatedDate = Convert.ToInt32(data["createdDate"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Gallery\" + gallery.GalleryName;

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
                _context.Gallery.Add(gallery);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetGalleryWithId", new { id = gallery.GalleryID }, gallery);
            }
            return BadRequest();
        }

        [HttpPut("editegallerywithimage")]
        public async Task<IActionResult> editegallerywithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Gallery gallery = new Gallery();
                gallery.GalleryID = Convert.ToInt32(data["galleryID"]);
                gallery.GalleryName = data["galleryname"];
                gallery.SuffixFile = data["suffixFile"];
                gallery.GalleryDesc = data["galleryDesc"];
                gallery.Languge = Convert.ToInt32(data["languge"]);
                gallery.SuffixFile = data["suffixFile"];
                gallery.PicRow = Convert.ToInt32(data["picRow"]);
                gallery.Status = Convert.ToBoolean(data["status"]);
                gallery.CreatedDate = Convert.ToInt32(data["createdDate"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Gallery\" + gallery.GalleryName;

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
                _context.Entry(gallery).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetGalleryWithId", new { id = gallery.GalleryID }, gallery);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalleryExists(gallery.GalleryID))
                        return NotFound();
                    else
                        return BadRequest();
                }
            }
            return BadRequest();

        }



        // POST: api/Gallery
        //[Authorize]
        [HttpPost("CreateGallery")]
        public async Task<IActionResult> CreateGallery([FromBody] Gallery Gallery , IFormFile imgup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.Gallery.Add(Gallery);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetGalleryWithId", new {  id = Gallery.GalleryID }, Gallery);
            //return Ok(Gallery);
        }

        // DELETE: api/Gallery/5
        //[Authorize]
        [HttpDelete("DeleteGallery/{id:int}")]
        public async Task<IActionResult> DeleteGallery([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != Gallery.GalleryID)
            //    return BadRequest();

            var Gallery = await _context.Gallery.FindAsync(id);
            if (Gallery == null)
                return NotFound();
            _context.Gallery.Remove(Gallery);
            await _context.SaveChangesAsync();

            return Ok(Gallery);
        }

        private bool GalleryExists(int id)
        {
            return _context.Gallery.Any(e => e.GalleryID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}