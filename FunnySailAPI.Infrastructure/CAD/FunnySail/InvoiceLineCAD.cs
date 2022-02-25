using FunnySailAPI.ApplicationCore.Interfaces.CAD;
using FunnySailAPI.ApplicationCore.Interfaces.CAD.FunnySail;
using FunnySailAPI.ApplicationCore.Models.FunnySailEN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.Infrastructure.CAD.FunnySail
{
    public class InvoiceLineCAD : BaseCAD<InvoiceLineEN>, IInvoiceLineCAD
    {
        public InvoiceLineCAD(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<InvoiceLineEN> FindByIds(int idBooking, int? idClientInvoice)
        {
            if (idClientInvoice != null)
            {
                return await _dbContext.InvoiceLines.FindAsync(idBooking, idClientInvoice);
            }
            else
            {
                return await _dbContext.InvoiceLines.FindAsync(idBooking);
            }
        }
    }
}
