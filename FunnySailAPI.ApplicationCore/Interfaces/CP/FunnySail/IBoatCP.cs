using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IBoatCP
    {
        Task<int> CreateBoat(AddBoatInputDTO addBoatInput);
        Task<BoatEN> DisapproveBoat(DisapproveBoatInputDTO disapproveBoatInput);
        Task<decimal> CalculatePrice();
    }
}
