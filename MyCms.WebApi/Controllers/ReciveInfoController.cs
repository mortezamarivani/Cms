using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MyCms.DomainClasses.ReciveInfo;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReciveInfoController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReciveInfoController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }




        [Route("GetReciveInfoByPageId/{pageinfo}")]
        public IEnumerable<ReciveInfo> GetReciveInfoByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.ReciveInfo.OrderBy(n => n.ReciveInfoID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("ReciveInfoCount")]
        public int ReciveInfoCount()
        {
            return _context.ReciveInfo.Count() / 4;
        }

        [HttpGet]
        [Route("AllReciveInfoCount")]
        public int AllReciveInfoCount()
        {
            return _context.ReciveInfo.Count();
        }


        // GET: api/ReciveInfo
        //[Authorize]
        [HttpGet("GetAllReciveInfo")]
        public IEnumerable<ReciveInfo> GetAllReciveInfo()
       {
            var userid = GetUserId();
            return _context.ReciveInfo;
        }



        // GET: api/ReciveInfo/5
        //[Authorize]
        [HttpGet("GetReciveInfoWithId/{id:int}")]
        public async Task<IActionResult> GetReciveInfoWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ReciveInfo = await _context.ReciveInfo.FindAsync(id);

            if (ReciveInfo == null)
                return NotFound();
            return Ok(ReciveInfo);
        }

        // PUT: api/ReciveInfo/5
        //[Authorize]
        [HttpPut("EditReciveInfo/{id:int}")]
        public async Task<IActionResult> EditReciveInfo([FromRoute] int id, [FromBody] ReciveInfo ReciveInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != ReciveInfo.ReciveInfoID)
                return BadRequest();

            _context.Entry(ReciveInfo).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReciveInfoExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }



        // POST: api/ReciveInfo
        //[Authorize]
        [HttpPost("CreateReciveInfo")]
        public async Task<IActionResult> CreateReciveInfo([FromBody] ReciveInfo ReciveInfo)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.ReciveInfo.Add(ReciveInfo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetReciveInfoWithId", new {  id = ReciveInfo.ReciveInfoID }, ReciveInfo);
            //return Ok(ReciveInfo);
        }

        // DELETE: api/ReciveInfo/5
        //[Authorize]
        [HttpDelete("DeleteReciveInfo/{id:int}")]
        public async Task<IActionResult> DeleteReciveInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != ReciveInfo.ReciveInfoID)
            //    return BadRequest();

            var ReciveInfo = await _context.ReciveInfo.FindAsync(id);
            if (ReciveInfo == null)
                return NotFound();
            _context.ReciveInfo.Remove(ReciveInfo);
            await _context.SaveChangesAsync();

            return Ok(ReciveInfo);
        }

        private bool ReciveInfoExists(int id)
        {
            return _context.ReciveInfo.Any(e => e.ReciveInfoID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}