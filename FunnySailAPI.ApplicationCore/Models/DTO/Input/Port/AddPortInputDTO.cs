using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.DTO.Input.Port
{
    public class AddPortInputDTO
    {

        [Required, StringLength(100)]
        public String Name { get; set; }

        [Required, StringLength(1000)]
        public String Location { get; set; }

        //public List<AddMooringDTO> Moorings { get; set; }
    }
}
