﻿using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBoatCP BoatCP { get; private set; }
        public IBookingCP BookingCP { get; private set; }
        public IOwnerInvoiceCP OwnerInvoiceCP { get; private set; }
        public IPortMooringCP PortMooringCP { get; private set; }
        public IRefundCP RefundCP { get; private set; }
        public ITechnicalServiceCP TechnicalServiceCP { get; private set; }
        public IBoatCEN BoatCEN { get; private set; }
        public IActivityBookingCEN ActivityBookingCEN { get; private set; }
        public IActivityCEN ActivityCEN { get; private set; }
        public IBookingCEN BookingCEN { get; private set; }
        public IClientInvoiceCEN ClientInvoiceCEN { get; private set; }
        public IMooringCEN MooringCEN { get; private set; }
        public IOwnerInvoiceCEN OwnerInvoiceCEN { get; private set; }
        public IPortCEN PortCEN { get; private set; }
        public IRefundCEN RefundCEN { get; private set; }
        public IRequiredBoatTitlesCEN RequiredBoatTitlesCEN { get; private set; }
        public IResourcesCEN ResourcesCEN { get; private set; }
        public IReviewCEN ReviewCEN { get; private set; }
        public IServiceCEN ServiceCEN { get; private set; }
        public ITechnicalServiceCEN TechnicalServiceCEN { get; private set; }
        public IUserCEN UserCEN { get; private set; }

        public UnitOfWork(IBoatCP BoatCP,
                          IBookingCP BookingCP,
                          IOwnerInvoiceCP OwnerInvoiceCP,
                          IPortMooringCP PortMooringCP,
                          IRefundCP RefundCP,
                          ITechnicalServiceCP TechnicalServiceCP,
                          IBoatCEN BoatCEN,
                          IActivityBookingCEN ActivityBookingCEN,
                          IActivityCEN ActivityCEN,
                          IBookingCEN BookingCEN,
                          IClientInvoiceCEN ClientInvoiceCEN,
                          IMooringCEN MooringCEN,
                          IOwnerInvoiceCEN OwnerInvoiceCEN,
                          IPortCEN PortCEN,
                          IRefundCEN RefundCEN,
                          IRequiredBoatTitlesCEN RequiredBoatTitlesCEN,
                          IResourcesCEN ResourcesCEN,
                          IReviewCEN ReviewCEN,
                          IServiceCEN ServiceCEN,
                          ITechnicalServiceCEN TechnicalServiceCEN,
                          IUserCEN UserCEN)
        {
            this.BoatCP = BoatCP;
            this.BookingCP = BookingCP;
            this.OwnerInvoiceCP = OwnerInvoiceCP;
            this.PortMooringCP = PortMooringCP;
            this.RefundCP = RefundCP;
            this.TechnicalServiceCP = TechnicalServiceCP;
            this.BoatCEN = BoatCEN;
            this.ActivityBookingCEN = ActivityBookingCEN;
            this.ActivityCEN = ActivityCEN;
            this.BookingCEN = BookingCEN;
            this.ClientInvoiceCEN = ClientInvoiceCEN;
            this.MooringCEN = MooringCEN;
            this.OwnerInvoiceCEN = OwnerInvoiceCEN;
            this.RefundCEN = RefundCEN;
            this.PortCEN = PortCEN;
            this.RequiredBoatTitlesCEN = RequiredBoatTitlesCEN;
            this.ResourcesCEN = ResourcesCEN;
            this.ReviewCEN = ReviewCEN;
            this.ServiceCEN = ServiceCEN;
            this.TechnicalServiceCEN = TechnicalServiceCEN;
            this.UserCEN = UserCEN;
        }
    }
}