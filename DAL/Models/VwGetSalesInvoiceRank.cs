﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class VwGetSalesInvoiceRank
    {
        public long? RowRank { get; set; }
        public int InvId { get; set; }
        public int? BookId { get; set; }
        public int TrNo { get; set; }
    }
}
