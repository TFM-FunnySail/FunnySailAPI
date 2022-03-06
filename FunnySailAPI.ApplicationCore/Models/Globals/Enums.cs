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

    public enum BookingStatusEnum
    {
        Booking,
        Rented,
        Cancelled,
        Completed
    }

    public enum ResourcesEnum
    {
        Image,
        Video,
        Audio
    }

    public enum CurrencyEnum
    {
        EUR
    }

    public enum MooringEnum
    {
        Small,
        Medium,
        Big
    }

    public enum ExceptionTypesEnum
    {
        IsRequired,
        NotFound,
        DontExist
    }

    public enum OwnerInvoicesEnum
    {
        Booking,
        TechnicalService
    }
}
