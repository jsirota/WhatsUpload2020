using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.API.Data;
using WhatsUpload.Data.Models;

namespace WhatsUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly WhatsUploadDBContext _context;
        private IWebHostEnvironment _env;

        public UploadsController(WhatsUploadDBContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Uploads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Upload>>> GetUpload()
        {
            return await _context.Upload.ToListAsync();
        }

        // GET: api/Uploads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Upload>> GetUpload(int id)
        {
            var upload = await _context.Upload.FindAsync(id);

            if (upload == null)
            {
                return NotFound();
            }

            return upload;
        }

        // PUT: api/Uploads/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUpload(int id, Upload upload)
        {
            if (id != upload.FileID)
            {
                return BadRequest();
            }

            _context.Entry(upload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Uploads
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Upload>> PostUpload(IFormCollection formdata)
        {
            // JTS - put this code in CORE, to handle upload

            var files = HttpContext.Request.Form.Files;
            foreach (var file in files)
            {
                var uploadPath = _env.ContentRootPath + @"\Uploads\";

                // Create Directory
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Create Unique filename
                var fileName = Path.GetFileName(Guid.NewGuid().ToString() + "_" + file.FileName);
                string fullPath = uploadPath + fileName;

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    // Upload File
                    await file.CopyToAsync(fileStream);
                }

                // Save metadata
                // Confirm that the file was uploaded and exists
                if (System.IO.File.Exists(fullPath))
                {
                    Upload upload = new Upload();
                    upload.DateAdded = DateTime.Now;
                    upload.FileName = file.FileName;
                    upload.FilePath = fullPath;
                    upload.Description = "fuck";

                    _context.Upload.Add(upload);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetUpload", new { id = upload.FileID }, upload);
                }
                else
                {
                    // File didn't make it to upload folder
                    return RedirectToPage("./Index?msg=uploadfailure");
                }

            }


            return Ok();
        }

        // DELETE: api/Uploads/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Upload>> DeleteUpload(int id)
        {
            var upload = await _context.Upload.FindAsync(id);
            if (upload == null)
            {
                return NotFound();
            }

            _context.Upload.Remove(upload);
            await _context.SaveChangesAsync();

            return upload;
        }

        private bool UploadExists(int id)
        {
            return _context.Upload.Any(e => e.FileID == id);
        }
    }
}
