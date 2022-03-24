using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail
{
    public interface IPortMooringCP
    {
        Task<int> AddMooring(int portId, string alias, MooringEnum type);
    }
}
