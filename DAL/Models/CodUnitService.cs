﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class CodUnitService
    {
        public CodUnitService()
        {
            ProjProjUnitServices = new HashSet<ProjProjUnitService>();
        }

        public int UnitServId { get; set; }
        public int Code { get; set; }
        public string? DescA { get; set; }
        public string? DescE { get; set; }
        public int? Aid { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? RemarksA { get; set; }
        public string? RemarksE { get; set; }

        public virtual ICollection<ProjProjUnitService> ProjProjUnitServices { get; set; }
    }
}
