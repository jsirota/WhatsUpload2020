using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WhatsUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DownloadController : ControllerBase
    {
        private IWebHostEnvironment _env;

        public DownloadController(IWebHostEnvironment env)
        {
            _env = env;
        }


        // GET: api/Document/docID
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return Ok("get worked");
        }

        [HttpPost("{fileName}", Name = "Post")]
        public ActionResult Post(string fileName)
        {
            //string fileName = @"\1c6bbfad-45ad-4b8e-911a-f6ff6263e0aa_adams song.mp3";
            var wwwPath = _env.ContentRootPath + @"\Uploads\";
            var downloadPath = wwwPath + fileName;

            // Check that the file exists
            byte[] fileBytes = System.IO.File.ReadAllBytes(downloadPath);
            System.IO.File.WriteAllBytes(downloadPath, fileBytes);
            MemoryStream ms = new MemoryStream(fileBytes);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "NewName.mp3");
        }
    }
}