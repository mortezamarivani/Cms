using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCms.DomainClasses.Project;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
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

        

        [Route("GetProjectByPageId/{pageinfo}")]
        public IEnumerable<Project> GetProjectByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.Project.OrderBy(n => n.ProjectID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("ProjectCount")]
        public int ProjectCount()
        {
            return _context.Project.Count() / 4;
        }

        [HttpGet]
        [Route("AllProjectCount")]
        public int AllProjectCount()
        {
            return _context.Project.Count();
        }


        // GET: api/Gallery
        //[Authorize]
        [HttpGet("GetAllProject")]
        public IEnumerable<Project> GetAllProject()
       {
            var userid = GetUserId();
            return _context.Project;
        }



        // GET: api/Project/5
        //[Authorize]
        [HttpGet("GetProjectWithId/{id:int}")]
        public async Task<IActionResult> GetProjectWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Project = await _context.Project.FindAsync(id);

            if (Project == null)
                return NotFound();
            return Ok(Project);
        }

        // PUT: api/Gallery/5
        //[Authorize]
        [HttpPut("EditProject/{id:int}")]
        public async Task<IActionResult> EditProject([FromRoute] int id, [FromBody] Project Project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != Project.ProjectID)
                return BadRequest();

            _context.Entry(Project).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpPost("CreateProjectWithimage")]
        public async Task<IActionResult> CreateProjectWithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Project project = new Project();
                project.ProjectName = data["projectName"];
                project.ProjectDesc = data["projectDesc"]; 
                project.Position = data["position"]; 
                project.SuffixFile = data["suffixFile"];
                project.Status = Convert.ToBoolean(data["status"]);
                project.CreatedDate = Convert.ToInt32(data["createdDate"]);
                project.Languge = Convert.ToInt32(data["languge"]);
                project.Institute = data["institute"];
                project.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                project.DocName = Guid.NewGuid().ToString() + data["suffixFile"];
                project.EndDate = Convert.ToInt32(data["endDate"]);
                project.StartDate = Convert.ToInt32(data["startDate"]);
                project.Tools = data["tools"];
                project.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Doc\" + project.DocName;

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
                _context.Project.Add(project);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetProjectWithId", new { id = project.ProjectID }, project);
            }
            return BadRequest();
        }

        [HttpPut("editeprojectwithimage")]
        public async Task<IActionResult> editeprojectwithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                //if (id != Convert.ToInt32(data["galleryID"]) )
                //    return BadRequest();

                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Project project = new Project();
                project.ProjectID = Convert.ToInt32(data["projectID"]);
                project.ProjectName = data["projectName"]; 
                project.Position = data["position"];
                project.ProjectDesc = data["projectDesc"];
                project.SuffixFile = data["suffixFile"];
                project.Status = Convert.ToBoolean(data["status"]);
                project.CreatedDate = Convert.ToInt32(data["createdDate"]);
                project.Languge = Convert.ToInt32(data["languge"]);
                project.Institute = data["institute"];
                project.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                project.DocName = Guid.NewGuid().ToString() + data["suffixFile"];
                project.EndDate = Convert.ToInt32(data["endDate"]);
                project.StartDate = Convert.ToInt32(data["startDate"]);
                project.Tools = data["tools"];
                project.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\doc\" + project.DocName;

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
                _context.Entry(project).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetProjectWithId", new { id = project.ProjectID }, project);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectID))
                        return NotFound();
                    else
                        return BadRequest();
                }
            }
            return BadRequest();

        }



        // POST: api/Gallery
        //[Authorize]
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject([FromBody] Project project, IFormFile imgup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProjectWithId", new {  id = project.ProjectID }, project);
            //return Ok(Gallery);
        }

        // DELETE: api/Gallery/5
        //[Authorize]
        [HttpDelete("DeleteProject/{id:int}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != Gallery.GalleryID)
            //    return BadRequest();

            var Project = await _context.Project.FindAsync(id);
            if (Project == null)
                return NotFound();
            _context.Project.Remove(Project);
            await _context.SaveChangesAsync();

            return Ok(Project);
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}