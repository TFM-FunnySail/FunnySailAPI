﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail
{
    public interface IBoatTypeCAD : IBaseCAD<BoatTypeEN>
    {
        Task<int> AddBoatType(string name, string description);
    }
}