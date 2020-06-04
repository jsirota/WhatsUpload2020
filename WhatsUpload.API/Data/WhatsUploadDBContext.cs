using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhatsUpload.Data.Models;

namespace WhatsUpload.API.Data
{
    public class WhatsUploadDBContext : DbContext
    {
        public WhatsUploadDBContext (DbContextOptions<WhatsUploadDBContext> options)
            : base(options)
        {
        }

        public DbSet<WhatsUpload.Data.Models.Upload> Upload { get; set; }
    }
}
