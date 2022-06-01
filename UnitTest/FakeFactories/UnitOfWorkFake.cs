using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using FunnySailAPI.ApplicationCore.Services;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Services.CEN.FunnySail.OwnerInvoicesTypes;
using FunnySailAPI.ApplicationCore.Services.CP;
using FunnySailAPI.Controllers;
using FunnySailAPI.Infrastructure;
using FunnySailAPI.Infrastructure.CAD.FunnySail;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.FakeFactories
{
    public class UnitOfWorkFake
    {
        public UnitOfWork unitOfWork;
        private IBoatCAD _BoatCAD;
        private IBoatCEN _BoatCEN;
        private IBoatInfoCAD _BoatInfoCAD;
        private IBoatInfoCEN _BoatInfoCEN;
        private IBoatResourceCAD _BoatResourceCAD;
        private IBoatResourceCEN _BoatResourceCEN;
        private IBoatPricesCAD _BoatPricesCAD;
        private IBoatPricesCEN _BoatPricesCEN;
        private IBoatTypeCAD _BoatTypeCAD;
        private IBoatTypeCEN _BoatTypeCEN;
        private IBoatCP _BoatCP;
        private IBookingCAD _BookingCAD;
        private IBookingCEN _BookingCEN;
        private IBookingCP _BookingCP;
        private IOwnerInvoiceCAD _OwnerInvoiceCAD;
        private IOwnerInvoiceCEN _OwnerInvoiceCEN;
        private IOwnerInvoiceCP _OwnerInvoiceCP;
        private IPortCAD _PortCAD;
        private IPortCEN _PortCEN;
        private IPortMooringCP _PortMooringCP;
        private IRefundCAD _RefundCAD;
        private IRefundCEN _RefundCEN;
        private IRefundCP _RefundCP;
        private ITechnicalServiceCAD _TechnicalServiceCAD;
        private ITechnicalServiceCEN _TechnicalServiceCEN;
        private ITechnicalServiceCP _TechnicalServiceCP;
        private IActivityBookingCAD _ActivityBookingCAD;
        private IActivityBookingCEN _ActivityBookingCEN;
        private IActivityCAD _ActivityCAD;
        private IActivityCEN _ActivityCEN;
        private IClientInvoiceCAD _ClientInvoiceCAD;
        private IClientInvoiceCEN _ClientInvoiceCEN;
        private IClientInvoiceLineCAD _ClientInvoiceLineCAD;
        private IClientInvoiceLineCEN _ClientInvoiceLineCEN;
        private IMooringCAD _MooringCAD;
        private IMooringCEN _MooringCEN;
        private IRequiredBoatTitleCAD _RequiredBoatTitleCAD;
        private IRequiredBoatTitlesCEN _RequiredBoatTitlesCEN;
        private IResourcesCAD _ResourcesCAD;
        private IResourcesCEN _ResourcesCEN;
        private IReviewCAD _ReviewCAD;
        private IReviewCEN _ReviewCEN;
        private IServiceCAD _ServiceCAD;
        private IServiceCEN _ServiceCEN;
        private IUserCAD _UserCAD;
        private IUserCEN _UserCEN;
        private UserManagerMock _UserManagerMock;
        private SignInManagerMock _SignInManagerMock;
        private IUserCP _UserCP;
        private IOwnerInvoiceLineCAD _OwnerInvoiceLineCAD;
        private IOwnerInvoiceLineCEN _OwnerInvoiceLineCEN;
        private IServiceBookingCAD _ServiceBookingCAD;
        private IServiceBookingCEN _ServiceBookingCEN;
        private IBoatBookingCAD _BoatBookingCAD;
        private IBoatBookingCEN _BoatBookingCEN;
        private ITechnicalServiceBoatCAD _TechnicalServiceBoatCAD;
        private IDatabaseTransactionFactory _DatabaseTransactionFactory;
        private IOwnerInvoiceTypeFactory _OwnerInvoiceTypeFactory;
        private IBoatTitleCAD _BoatTitleCAD;
        private IBoatTitlesCEN _BoatTitlesCEN;
        public UnitOfWorkFake() {
            var applicationDbContextFake = new ApplicationDbContextFake();
            _DatabaseTransactionFactory = new DatabaseTransactionFactory
              (applicationDbContextFake._dbContextFake);
            _UserManagerMock = new UserManagerMock();
            _UserManagerMock.SetupForCreateUser();
            _SignInManagerMock = new SignInManagerMock();
            _SignInManagerMock.SetupForLoginPassFailed();
            

            _BoatCAD = new BoatCAD(applicationDbContextFake._dbContextFake);
            _BoatInfoCAD = new BoatInfoCAD(applicationDbContextFake._dbContextFake);
            _BoatPricesCAD = new BoatPricesCAD(applicationDbContextFake._dbContextFake);
            _BoatResourceCAD = new BoatResourceCAD(applicationDbContextFake._dbContextFake);
            _BoatTypeCAD = new BoatTypeCAD(applicationDbContextFake._dbContextFake);
            _BoatTitleCAD = new BoatTitleCAD(applicationDbContextFake._dbContextFake);
            _BookingCAD = new BookingCAD(applicationDbContextFake._dbContextFake);
            _OwnerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _PortCAD = new PortCAD(applicationDbContextFake._dbContextFake);
            _RefundCAD = new RefundCAD(applicationDbContextFake._dbContextFake);
            _TechnicalServiceCAD = new TechnicalServiceCAD(applicationDbContextFake._dbContextFake);
            _ActivityBookingCAD = new ActivityBookingCAD(applicationDbContextFake._dbContextFake);
            _ActivityCAD = new ActivityCAD(applicationDbContextFake._dbContextFake);
            _ClientInvoiceCAD = new ClientInvoiceCAD(applicationDbContextFake._dbContextFake);
            _MooringCAD = new MooringCAD(applicationDbContextFake._dbContextFake);
            _OwnerInvoiceCAD = new OwnerInvoiceCAD(applicationDbContextFake._dbContextFake);
            _RequiredBoatTitleCAD = new RequiredBoatTitleCAD(applicationDbContextFake._dbContextFake);
            _ResourcesCAD = new ResourcesCAD(applicationDbContextFake._dbContextFake);
            _ReviewCAD = new ReviewCAD(applicationDbContextFake._dbContextFake);
            _ServiceCAD = new ServiceCAD(applicationDbContextFake._dbContextFake);
            _TechnicalServiceCAD = new TechnicalServiceCAD(applicationDbContextFake._dbContextFake);
            _UserCAD = new UsersCAD(applicationDbContextFake._dbContextFake);
            _ServiceBookingCAD = new ServiceBookingCAD(applicationDbContextFake._dbContextFake);
            _BoatBookingCAD = new BoatBookingCAD(applicationDbContextFake._dbContextFake);
            _ClientInvoiceLineCAD = new ClientInvoiceLineCAD(applicationDbContextFake._dbContextFake);
            _TechnicalServiceBoatCAD = new TechnicalServiceBoatCAD(applicationDbContextFake._dbContextFake);
            _OwnerInvoiceLineCAD = new OwnerInvoiceLineCAD(applicationDbContextFake._dbContextFake);

            _BoatCEN = new BoatCEN(_BoatCAD);
            _BoatInfoCEN = new BoatInfoCEN(_BoatInfoCAD);
            _BoatPricesCEN = new BoatPricesCEN(_BoatPricesCAD);
            _BoatResourceCEN = new BoatResourceCEN(_BoatResourceCAD);
            _BoatTypeCEN = new BoatTypeCEN(_BoatTypeCAD);
            _BoatTitlesCEN = new BoatTitlesCEN(_BoatTitleCAD);
            _ActivityBookingCEN = new ActivityBookingCEN(_ActivityBookingCAD);
            _ActivityCEN = new ActivityCEN(_ActivityCAD);
            _BoatBookingCEN = new BoatBookingCEN(_BoatBookingCAD);
            _ServiceBookingCEN = new ServiceBookingCEN(_ServiceBookingCAD);
            _ClientInvoiceLineCEN = new ClientInvoiceLineCEN(_ClientInvoiceLineCAD);
            _OwnerInvoiceLineCEN = new OwnerInvoiceLineCEN(_OwnerInvoiceLineCAD);
            _MooringCEN = new MooringCEN(_MooringCAD);
            _PortCEN = new PortCEN(_PortCAD);
            _BookingCEN = new BookingCEN(_BookingCAD, _ActivityBookingCAD, _ServiceBookingCAD, _BoatBookingCAD, _UserCAD);
            _OwnerInvoiceCEN = new OwnerInvoiceCEN(_OwnerInvoiceCAD, _OwnerInvoiceLineCAD, _UserCAD);
            _RefundCEN = new RefundCEN(_RefundCAD, _BookingCEN);
            _ServiceCEN = new ServiceCEN(_ServiceCAD, _ServiceBookingCAD);
            _BookingCEN = new BookingCEN(_BookingCAD, _ActivityBookingCAD, _ServiceBookingCAD, _BoatBookingCAD, _UserCAD);
            _ClientInvoiceCEN = new ClientInvoiceCEN(_ClientInvoiceCAD, _ClientInvoiceLineCAD);
            _OwnerInvoiceCEN = new OwnerInvoiceCEN(_OwnerInvoiceCAD, _OwnerInvoiceLineCAD, _UserCAD);
            _RefundCEN = new RefundCEN(_RefundCAD, _BookingCEN);
            _RequiredBoatTitlesCEN = new RequiredBoatTitlesCEN(_RequiredBoatTitleCAD);
            _ResourcesCEN = new ResourcesCEN(_ResourcesCAD, null);
            _ReviewCEN = new ReviewCEN(_ReviewCAD);
            _ServiceCEN = new ServiceCEN(_ServiceCAD, _ServiceBookingCAD);
            _TechnicalServiceCEN = new TechnicalServiceCEN(_TechnicalServiceCAD, _TechnicalServiceBoatCAD);
            _UserCEN = new UserCEN(_UserCAD, _SignInManagerMock.singInManager.Object, _UserManagerMock.userManager.Object);

            _OwnerInvoiceTypeFactory = new OwnerInvoiceTypeFactory(_OwnerInvoiceCAD, _OwnerInvoiceLineCAD, _UserCAD, _TechnicalServiceBoatCAD);

            _RefundCP = new RefundCP(_RefundCEN, _DatabaseTransactionFactory);
            _PortMooringCP = new PortMooringCP(_MooringCEN, _PortCEN);
            _TechnicalServiceCP = new TechnicalServiceCP(_TechnicalServiceCEN, _BoatCEN);
            _UserCP = new UserCP(_UserCEN, _SignInManagerMock.singInManager.Object, _UserManagerMock.userManager.Object, _DatabaseTransactionFactory);
            _BoatCP = new BoatCP(_BoatCEN, _BoatInfoCEN, _BoatTypeCEN,_BoatResourceCEN, _BoatPricesCEN, _RequiredBoatTitlesCEN, _DatabaseTransactionFactory,_ReviewCEN,_UserCEN,_MooringCEN,_ResourcesCEN);
            _BookingCP = new BookingCP(_BookingCEN, _UserCEN, _ClientInvoiceLineCEN, _OwnerInvoiceLineCEN, _BoatBookingCEN, _ServiceBookingCEN, _ActivityBookingCEN, _ActivityCEN, _BoatCEN, _ServiceCEN, _BoatCP, _ClientInvoiceCEN, _DatabaseTransactionFactory, _RefundCEN, _BoatPricesCEN, _OwnerInvoiceCEN);
            _OwnerInvoiceCP = new OwnerInvoiceCP(_OwnerInvoiceTypeFactory, _DatabaseTransactionFactory);
            _PortMooringCP = new PortMooringCP(_MooringCEN,_PortCEN);
            _RefundCP = new RefundCP(_RefundCEN, _DatabaseTransactionFactory);
            _TechnicalServiceCP = new TechnicalServiceCP(_TechnicalServiceCEN, _BoatCEN);

            unitOfWork = new UnitOfWork(
                _BoatCP,
                _BookingCP,
                _OwnerInvoiceCP,
                _PortMooringCP,
                _RefundCP,
                _TechnicalServiceCP,
                _BoatCEN,
                _ActivityBookingCEN,
                _ActivityCEN,
                _BookingCEN,
                _ClientInvoiceCEN,
                _MooringCEN,
                _OwnerInvoiceCEN,
                _PortCEN,
                _RefundCEN,
                _RequiredBoatTitlesCEN,
                _ResourcesCEN,
                _ReviewCEN,
                _ServiceCEN,
                _TechnicalServiceCEN,
                _UserCEN,
                _UserManagerMock.userManager.Object,
                _SignInManagerMock.singInManager.Object,
                _UserCP,
                _OwnerInvoiceLineCEN,
                _BoatTitlesCEN,
                _BoatTypeCEN,
                _BoatPricesCEN
               );
        }
    }
}
