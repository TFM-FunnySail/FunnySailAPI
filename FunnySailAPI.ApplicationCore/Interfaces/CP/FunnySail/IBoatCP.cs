using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IBoatCP
    {
        Task<int> CreateBoat(AddBoatInputDTO addBoatInput);
        Task<BoatEN> DisapproveBoat(int boatId, DisapproveBoatInputDTO disapproveBoatInput);
        Task<decimal> CalculatePrice();
        Task<BoatEN> UpdateBoat(UpdateBoatInputDTO updateBoatInput);
        Task<int> AddImage(int boatId, IFormFile image, bool main);
        Task RemoveImage(int id, int resourceId);
    }
}
