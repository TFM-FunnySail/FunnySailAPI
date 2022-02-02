using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.DTO.Output;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IBoatCP
    {
        Task<BoatOutputDTO> CreateBoat(AddBoatInputDTO addBoatInput);
    }
}
