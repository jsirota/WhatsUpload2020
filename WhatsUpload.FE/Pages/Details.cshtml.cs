using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.FE.Data;
using WhatsUpload.FE.Models;

namespace WhatsUpload.FE.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WhatsUpload.FE.Data.WhatsUploadContext _context;

        public DetailsModel(WhatsUpload.FE.Data.WhatsUploadContext context)
        {
            _context = context;
        }

        public Upload Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Upload = await _context.Upload.FirstOrDefaultAsync(m => m.FileID == id);

            if (Upload == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
