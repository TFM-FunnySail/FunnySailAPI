using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Mooring;
using FunnySailAPI.DTO.Output.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunnySailAPI.DTO.Output.Activity
{
    public class ActivityOutputDTO
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public List<ActivityResourcesOutputDTO> ActivityResources { get; set; }

        public ActivityOutputDTO()
        {

        }

    }
}
