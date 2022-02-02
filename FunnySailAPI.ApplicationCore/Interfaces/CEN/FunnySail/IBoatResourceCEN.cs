﻿using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatResourceCEN
    {
        Task<(int, string)> AddBoatResource(BoatResourceEN boatResourceEN);
    }
}
