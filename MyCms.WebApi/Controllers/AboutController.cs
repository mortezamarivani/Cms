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
using MyCms.DomainClasses.About;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AboutController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }




        [Route("GetAboutByPageId/{pageinfo}")]
        public IEnumerable<About> GetAboutByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.About.OrderBy(n => n.AboutID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("AboutCount")]
        public int AboutCount()
        {
            return _context.About.Count() / 4;
        }

        [HttpGet]
        [Route("AllAboutCount")]
        public int AllAboutCount()
        {
            return _context.About.Count();
        }


        // GET: api/Gallery
        //[Authorize]
        [HttpGet("GetAllAbout")]
        public IEnumerable<About> GetAllAbout()
       {
            var userid = GetUserId();
            return _context.About;
        }



        // GET: api/About/5
        //[Authorize]
        [HttpGet("GetAboutWithId/{id:int}")]
        public async Task<IActionResult> GetAboutWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var About = await _context.About.FindAsync(id);

            if (About == null)
                return NotFound();
            return Ok(About);
        }

        //[Authorize]
        [HttpGet("GetAboutWithLngId/{id:int}")]
        public  IEnumerable<About> GetAboutWithLngId([FromRoute] int id)
        {
            return  _context.About.Select(r => r).Where(r => r.Languge == id).ToList();
        }




        // PUT: api/Gallery/5
        //[Authorize]
        [HttpPut("EditAbout/{id:int}")]
        public async Task<IActionResult> EditAbout([FromRoute] int id, [FromBody] About About)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != About.AboutID)
                return BadRequest();

            _context.Entry(About).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AboutExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }




        // POST: api/Gallery
        //[Authorize]
        [HttpPost("CreateAbout")]
        public async Task<IActionResult> CreateAbout([FromBody] About About)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.About.Add(About);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAboutWithId", new {  id = About.AboutID }, About);
            //return Ok(About);
        }

        // DELETE: api/About/5
        //[Authorize]
        [HttpDelete("DeleteAbout/{id:int}")]
        public async Task<IActionResult> DeleteAbout([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != About.AboutID)
            //    return BadRequest();

            var About = await _context.About.FindAsync(id);
            if (About == null)
                return NotFound();
            _context.About.Remove(About);
            await _context.SaveChangesAsync();

            return Ok(About);
        }

        private bool AboutExists(int id)
        {
            return _context.About.Any(e => e.AboutID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}