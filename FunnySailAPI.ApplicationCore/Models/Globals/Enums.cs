using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.Globals
{
    public enum BoatTiteEnum
    {
        Patronja,
        Captaincy,
        NavigationLicence
    }

    public enum UserRoleEnum
    {
        Client,
        BoatOwner,
        Admin
    }

    public enum OrderStatusEnum
    {
        Booking,
        Rented,
        Cancelled,
        Completed
    }

    public enum BoatResourcesEnum
    {
        Image,
        Video,
        Audio
    }

    public enum CurrencyEnum
    {
        EUR,
        USD
    }
}
