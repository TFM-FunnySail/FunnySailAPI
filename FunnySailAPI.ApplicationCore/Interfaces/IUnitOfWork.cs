using FunnySailAPI.ApplicationCore.Interfaces.CEN.FunnySail;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        public IBoatCP BoatCP { get;  }
        public IBookingCP BookingCP { get;  }
        public IOwnerInvoiceCP OwnerInvoiceCP { get;  }
        public IPortMooringCP PortMooringCP { get;  }
        public IRefundCP RefundCP { get;  }
        public ITechnicalServiceCP TechnicalServiceCP { get;  }
        public IBoatCEN BoatCEN { get;  }
        public IActivityBookingCEN ActivityBookingCEN { get;  }
        public IActivityCEN ActivityCEN { get;  }
        public IBookingCEN BookingCEN { get;  }
        public IClientInvoiceCEN ClientInvoiceCEN { get;  }
        public IMooringCEN MooringCEN { get;  }
        public IOwnerInvoiceCEN OwnerInvoiceCEN { get;  }
        public IPortCEN PortCEN { get;  }
        public IRefundCEN RefundCEN { get;  }
        public IRequiredBoatTitlesCEN RequiredBoatTitlesCEN { get;  }
        public IResourcesCEN ResourcesCEN { get;  }
        public IReviewCEN ReviewCEN { get;  }
        public IServiceCEN ServiceCEN { get;  }
        public ITechnicalServiceCEN TechnicalServiceCEN { get;  }
        public IUserCEN UserCEN { get;  }
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }
        public IUserCP UserCP { get; }
        public IOwnerInvoiceLineCEN OwnerInvoiceLineCEN { get; }
        public IBoatTitlesCEN BoatTitlesCEN { get; }
        public IBoatTypeCEN BoatTypeCEN { get; }
    }
}
