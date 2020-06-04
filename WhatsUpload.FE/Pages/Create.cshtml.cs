using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhatsUpload.FE.Data;
using WhatsUpload.FE.Models;

namespace WhatsUpload.FE.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WhatsUpload.FE.Data.WhatsUploadContext _context;
        private IWebHostEnvironment _env;


        public CreateModel(WhatsUpload.FE.Data.WhatsUploadContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Upload Upload { get; set; }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Upload the file before saving metadata
            if (file != null && file.Length > 0)
            {
                var wwwPath = @"\Uploads\";
                var uploadPath = _env.WebRootPath + wwwPath;

                // Create Directory
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Create Unique filename
                var fileName = Path.GetFileName(Guid.NewGuid().ToString() + "_" + file.FileName.ToLower());
                string fullPath = uploadPath + fileName;

                // Add upload path to Upload object
                Upload.FileName = file.FileName;
                Upload.FilePath = fullPath;
                Upload.DateAdded = DateTime.Now;

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    // Upload File
                    await file.CopyToAsync(fileStream);
                }

                ViewData["FileLocation"] = fullPath;
            }
            else
            {
                // No file provided
            }

            // Save metadata
            // Confirm that the file was uploaded and exists
            if (System.IO.File.Exists(Upload.FilePath))
            {
                _context.Upload.Add(Upload);
                await _context.SaveChangesAsync();
            }
            else
            {
                // File didn't make it to upload folder
                return RedirectToPage("./Index?msg=uploadfailure");
            }

            return RedirectToPage("./Index");
        }
    }
}
