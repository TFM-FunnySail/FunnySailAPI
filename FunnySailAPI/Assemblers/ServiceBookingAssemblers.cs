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
                Id = serviceBookingEN.ServiceId,
                Price = Math.Round(serviceBookingEN.Price,2)
            };

            if(serviceBookingEN.service != null)
            {
                serviceBookingOutputDTO.Name = serviceBookingEN.service.Name;
            }

            return serviceBookingOutputDTO;
        }
    }
}