using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCms.DataLayer.Context;
using MyCms.DomainClasses.Skills;
using Microsoft.AspNetCore.Identity;
using MyCms.DomainClasses.Gallery;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : Controller
    {
        private readonly MyCmsDbContext _context;

        public SkillsController(MyCmsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("SkillsCount")]
        public int SkillsCount()
        {
            return _context.Skills.Count() / 4;
        }

        [HttpGet]
        [Route("AllSkillsCount")]
        public int AllSkillsCount()
        {
            return _context.Skills.Count() ;
        }


        [Route("GetSkillsByPageId/{pageinfo}")]
        public IEnumerable<Skills> GetSkillsByPageId(string pageinfo )
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId -1 ) * take;
            return _context.Skills.OrderBy(n => n.SkillsID).Skip(skip).Take(take).ToList();
        }

        // GET: api/Skills
        [HttpGet]
        //[Authorize]
        public IEnumerable<Skills> GetSkills()
       {
            var userid = GetUserId();
            return _context.Skills;
        }
       

        // GET: api/Skills/5
        //[Authorize]
        [HttpGet("GetSkillsWithId/{id:int}")]
        public async Task<IActionResult> GetSkillsWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Skills = await _context.Skills.FindAsync(id);

            if (Skills == null)
                return NotFound();
            return Ok(Skills);
        }

        // PUT: api/Skills/5
        //[Authorize]
        [HttpPut("EditSkills/{id:int}")]
        public async Task<IActionResult> EditSkills([FromRoute] int id, [FromBody] Skills skills)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != skills.SkillsID)
                return BadRequest();

            _context.Entry(skills).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillsExists(id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(skills); 
        }

        // POST: api/Skills
        //[Authorize]
        [HttpPost("CreateSkills")]
        public async Task<IActionResult> CreateSkills([FromBody] Skills skills)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.Skills.Add(skills);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSkills", new {  id = skills.SkillsID }, skills);
        }

        // DELETE: api/Skills/5
        //[Authorize]
        [HttpDelete("Deleteskills/{id:int}")]
        public async Task<IActionResult> Deleteskills([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != skills.SkillsID)
            //    return BadRequest();

            var Skills = await _context.Skills.FindAsync(id);
            if (Skills == null)
                return NotFound();
            _context.Skills.Remove(Skills);
            await _context.SaveChangesAsync();

            return Ok(Skills);
        }

        private bool SkillsExists(int id)
        {
            return _context.Skills.Any(e => e.SkillsID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}