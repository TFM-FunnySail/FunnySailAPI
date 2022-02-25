using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IMooringCEN
    {
        Task<int> AddMooring(int portId, string alias, MooringEnum type);
        Task DeleteMooring(int mooringId);
        Task<MooringEN> UpdateMooring(UpdateMooringDTO updateMooringInput);
    }
}
