using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCms.DomainClasses.Course;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using MyCms.DomainClasses.Gallery;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly MyCmsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CourseController(MyCmsDbContext context, IHttpContextAccessor httpContextAccessor)
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

        

        [Route("GetCourseByPageId/{pageinfo}")]
        public IEnumerable<Course> GetCourseByPageId(string pageinfo)
        {
            if (pageinfo == null)
                return null;
            int take = Convert.ToInt32(pageinfo.Split(";")[0]);
            int pageId = Convert.ToInt32(pageinfo.Split(";")[1]);
            if (pageId == 0)
                pageId = 1;
            int skip = (pageId - 1) * take;
            return _context.Course.OrderBy(n => n.CourseID).Skip(skip).Take(take).ToList();
        }

        [HttpGet]
        [Route("CourseCount")]
        public int CourseCount()
        {
            return _context.Course.Count() / 4;
        }

        [HttpGet]
        [Route("AllCourseCount")]
        public int AllCourseCount()
        {
            return _context.Course.Count();
        }


        // GET: api/Gallery
        //[Authorize]
        [HttpGet("GetAllCourse")]
        public IEnumerable<Course> GetAllCourse()
       {
            var userid = GetUserId();
            return _context.Course;
        }



        // GET: api/Gallery/5
        //[Authorize]
        [HttpGet("GetCourseWithId/{id:int}")]
        public async Task<IActionResult> GetCourseWithId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Course = await _context.Course.FindAsync(id);

            if (Course == null)
                return NotFound();
            return Ok(Course);
        }

        // PUT: api/Gallery/5
        //[Authorize]
        [HttpPut("EditCourse/{id:int}")]
        public async Task<IActionResult> EditCourse([FromRoute] int id, [FromBody] Course Course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != Course.CourseID)
                return BadRequest();

            _context.Entry(Course).State =EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }


        [HttpPost("CreateCourseWithimage")]
        public async Task<IActionResult> CreateCourseWithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Course course = new Course();
                course.CourseName = data["courseName"]; 
                course.CourseDesc = data["courseDesc"];
                course.SuffixFile = data["suffixFile"];
                course.Status = Convert.ToBoolean(data["status"]);
                course.CreatedDate = Convert.ToInt32(data["createdDate"]);
                course.Languge = Convert.ToInt32(data["languge"]);
                course.Institute = data["institute"];
                course.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                course.DocName = Guid.NewGuid().ToString() + data["suffixFile"]; 
                course.EndDate = Convert.ToInt32(data["endDate"]);
                course.StartDate = Convert.ToInt32(data["startDate"]);
                course.Tools = data["tools"];
                course.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\Doc\" + course.DocName;

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
                _context.Course.Add(course);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetCourseWithId", new { id = course.CourseID }, course);
            }
            return BadRequest();
        }

        [HttpPut("editecoursewithimage")]
        public async Task<IActionResult> editecoursewithimage(IFormCollection data)// [Bind("GalleryID,GalleryDesc,GalleryName,Status,CreatedDate,CreatorUserID,PicRow")] Gallery gallery ,IFormFile imgup
        {
            if (ModelState.IsValid)
            {
                //if (id != Convert.ToInt32(data["galleryID"]) )
                //    return BadRequest();

                var FileName = ((Microsoft.AspNetCore.Http.Internal.FormFile)((Microsoft.AspNetCore.Http.FormCollection)data).Files[0]).FileName;
                Course course = new Course();
                course.CourseID = Convert.ToInt32(data["courseID"]);
                course.CourseName = data["courseName"];
                course.CourseDesc = data["courseDesc"];
                course.SuffixFile = data["suffixFile"];
                course.Status = Convert.ToBoolean(data["status"]);
                course.CreatedDate = Convert.ToInt32(data["createdDate"]);
                course.Languge = Convert.ToInt32(data["languge"]);
                course.Institute = data["institute"];
                course.CreatorUserID = Convert.ToInt32(data["creatorUserID"]);
                course.DocName = Guid.NewGuid().ToString() + data["suffixFile"];
                course.EndDate = Convert.ToInt32(data["endDate"]);
                course.StartDate = Convert.ToInt32(data["startDate"]);
                course.Tools = data["tools"];
                course.Period = Convert.ToInt32(data["period"]);

                var fileBytes = new List<byte[]>();
                var files = _httpContextAccessor.HttpContext.Request.Form.Files;
                string imageaddress = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 3) + @"\wwwroot\doc\" + course.DocName;

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
                _context.Entry(course).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetCourseWithId", new { id = course.CourseID }, course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseID))
                        return NotFound();
                    else
                        return BadRequest();
                }
            }
            return BadRequest();

        }



        // POST: api/Gallery
        //[Authorize]
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] Course course, IFormFile imgup)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userid = GetUserId();
            _context.Course.Add(course);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCourseWithId", new {  id = course.CourseID }, course);
            //return Ok(Gallery);
        }

        // DELETE: api/Gallery/5
        //[Authorize]
        [HttpDelete("DeleteCourse/{id:int}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //if(id != Gallery.GalleryID)
            //    return BadRequest();

            var Course = await _context.Course.FindAsync(id);
            if (Course == null)
                return NotFound();
            _context.Course.Remove(Course);
            await _context.SaveChangesAsync();

            return Ok(Course);
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseID == id);
        }

        public string GetUserId()
        {
            if (!User.Identity.IsAuthenticated)
                return null;

            return HttpContext.User.Claims.First().Value;
        }

    }
}