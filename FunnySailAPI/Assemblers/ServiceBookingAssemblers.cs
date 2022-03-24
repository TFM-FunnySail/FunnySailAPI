using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.DTO.Output.Booking;
using System;

namespace FunnySailAPI.Assemblers
{
    public class ServiceBookingAssemblers
    {
        public static ServiceBookingOutputDTO Convert(ServiceBookingEN serviceBookingEN)
        {
            ServiceBookingOutputDTO serviceBookingOutputDTO = new ServiceBookingOutputDTO
            {
                ServiceId = serviceBookingEN.ServiceId,
                Price = serviceBookingEN.Price
            };

            return serviceBookingOutputDTO;
        }
    }
}