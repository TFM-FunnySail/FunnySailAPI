using FunnySailAPI.ApplicationCore.Models.Globals;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Models.FunnySailEN
{
    [Table("OrderRates")]
    public class OrderRatesEN
    {
        public int OrderId { get; set; }
        public CurrencyEnum Currency { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal AmountPaid { get; set; }

        [Column(TypeName = "decimal(9,4)")]
        public decimal TasaCurrency { get; set; }

        public OrderEN Order { get; set; }
    }
}
