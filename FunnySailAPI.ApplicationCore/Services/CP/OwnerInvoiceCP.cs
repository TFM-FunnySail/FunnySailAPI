using FunnySailAPI.ApplicationCore.Interfaces;
using FunnySailAPI.ApplicationCore.Interfaces.CEN;
using FunnySailAPI.ApplicationCore.Interfaces.CP.FunnySail;
using FunnySailAPI.ApplicationCore.Models.DTO.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunnySailAPI.ApplicationCore.Services.CP
{
    public class OwnerInvoiceCP : IOwnerInvoiceCP
    {
        private readonly IOwnerInvoiceTypeFactory _ownerInvoiceTypeFactory;
        private readonly IDatabaseTransactionFactory _databaseTransactionFactory;

        public OwnerInvoiceCP(IOwnerInvoiceTypeFactory ownerInvoiceTypeFactory,
                              IDatabaseTransactionFactory databaseTransactionFactory)
        {
            _ownerInvoiceTypeFactory = ownerInvoiceTypeFactory;
            _databaseTransactionFactory = databaseTransactionFactory;
        }

        public async Task<int> CreateOwnerInvoice(AddOwnerInvoiceInputDTO addOwnerInvoiceInput)
        {
            int newOwnerInvoiceId = 0;
            IOwnerInvoiceTypes ownerInvoiceType = _ownerInvoiceTypeFactory.GetOwnerInvoiceType(addOwnerInvoiceInput.Type);

            await ownerInvoiceType.ValidateAndPrepare(addOwnerInvoiceInput);

            using (var databaseTransaction = _databaseTransactionFactory.BeginTransaction())
            {
                try
                {
                    newOwnerInvoiceId = await ownerInvoiceType.CreateOwnerInvoice();

                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    throw ex;
                }
            }

            return newOwnerInvoiceId;
        }
    }
}
