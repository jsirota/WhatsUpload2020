using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.FE.Data;
using WhatsUpload.FE.Models;

namespace WhatsUpload.FE.Pages
{
    public class EditModel : PageModel
    {
        private readonly WhatsUpload.FE.Data.WhatsUploadContext _context;

        public EditModel(WhatsUpload.FE.Data.WhatsUploadContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            Upload.Name = Upload.Name.Trim();
            Upload.Description = Upload.Description.Trim();
            
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Upload).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UploadExists(Upload.FileID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UploadExists(int id)
        {
            return _context.Upload.Any(e => e.FileID == id);
        }
    }
}
