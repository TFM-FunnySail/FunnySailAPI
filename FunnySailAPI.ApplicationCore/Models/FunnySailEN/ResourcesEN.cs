using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    public class ResourcesEN
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Uri { get; set; }
        public bool Main { get; set; }
        public ResourcesEnum Type { get; set; }
    }
}
