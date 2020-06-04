using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.FE.Models;

namespace WhatsUpload.FE.Data
{
    public class WhatsUploadContext : DbContext
    {
        public WhatsUploadContext (DbContextOptions<WhatsUploadContext> options)
            : base(options)
        {
        }

        public DbSet<WhatsUpload.FE.Models.Upload> Upload { get; set; }
    }
}
