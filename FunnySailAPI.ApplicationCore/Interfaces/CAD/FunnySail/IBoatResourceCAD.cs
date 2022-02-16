using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBoatResourceCAD : IBaseCAD<BoatResourceEN>
    {
        Task<(int, int)> AddBoatResource(int boatId, int resourceId);
    }
}
