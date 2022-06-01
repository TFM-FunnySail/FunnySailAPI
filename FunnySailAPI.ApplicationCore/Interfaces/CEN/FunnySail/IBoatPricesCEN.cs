using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input.Boat;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail
{
    public interface IBoatPricesCEN
    {
        Task<int> AddBoatPrices(BoatPricesEN boatPricesEN);
        Task<BoatPricesEN> UpdateBoat(UpdateBoatPricesInputDTO updateBoatInput);
        decimal CalculatePrice(BoatPricesEN boatPrices,double days, double hours);
        IBoatPricesCAD GetBoatPricesCAD();
        decimal CalculatePrice(BoatPricesEN boatPrices, DateTime initialDate, DateTime endDate);
    }
}
