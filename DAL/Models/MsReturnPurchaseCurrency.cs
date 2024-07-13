﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class MsReturnPurchaseCurrency
    {
        public int RetPurchCurId { get; set; }
        public int? RetPurchId { get; set; }
        public int? CurrencyCategoryId { get; set; }
        public decimal? Value { get; set; }
        public decimal? Count { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }

        public virtual MsReturnPurchase? RetPurch { get; set; }
    }
}
