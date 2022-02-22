using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services.CEN.FunnySail
{
    public class BoatBaseCEN : IBoatBaseCEN
    {
        protected readonly string _enName;
        protected readonly string _esName;

        public BoatBaseCEN()
        {
            _enName = "Boat";
            _esName = "La embarcación";
        }
    }
}
