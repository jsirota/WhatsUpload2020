using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsUpload.Core.DocumentServices;

namespace WhatsUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        // JTSTEMP - DI Reference to my service
        private IDocumentService documentService;

        // JTSTEMP - Controller Constructor that instantiates my service
        public DocumentController(IDocumentService documentService) 
        {
            this.documentService = documentService;
        }

        // GET: api/Document
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Document/docID
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "GET by ID be got";
        }

        // POST: api/Document
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return "POST be done";
        }

        // PUT: api/Document/docID
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return "PUT was done";
        }

        // DELETE: api/Document/docID
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return "deleted bia";
        }
    }
}
