using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.FE.Data;
using WhatsUpload.FE.Models;

namespace WhatsUpload.FE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WhatsUpload.FE.Data.WhatsUploadContext _context;
        private IWebHostEnvironment _env;

        public IndexModel(WhatsUpload.FE.Data.WhatsUploadContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IList<Upload> Upload { get;set; }

        public async Task OnGetAsync()
        {
            Upload = await _context.Upload.ToListAsync();
        }

        public ActionResult OnPostDownload()
        {
            string fileName = @"\1c6bbfad-45ad-4b8e-911a-f6ff6263e0aa_adams song.mp3";
            var wwwPath = @"\Uploads\";
            var downloadPath = _env.WebRootPath + wwwPath + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(downloadPath);
            System.IO.File.WriteAllBytes(downloadPath, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "NewName.mp3");
        }
    }
}
