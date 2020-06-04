using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WhatsUpload.Data.Models
{
    public class Upload
    {
        [Key]
        public int FileID { get; set; }
        public string FilePath { get; set; }
        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Date Added")]
        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }

    }
}
