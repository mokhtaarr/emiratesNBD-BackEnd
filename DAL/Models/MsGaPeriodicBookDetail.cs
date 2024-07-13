﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class MsGaPeriodicBookDetail
    {
        public int PeriodicBookDetailId { get; set; }
        public int? PeriodicBookId { get; set; }
        public int? LetterNormId { get; set; }
        public int? Serial { get; set; }
        public string? MotorSn { get; set; }
        public string? BodySn { get; set; }
        public int? MororId { get; set; }
        public int? CityId { get; set; }
        public int? CapcityId { get; set; }
        public int? MachineId { get; set; }
        /// <summary>
        /// 0 Cash ; 1 Installment
        /// </summary>
        public byte? BasicSaleType { get; set; }
        public string? Remarks { get; set; }
        public string? Model { get; set; }
        public string? AddField1 { get; set; }
        public string? AddField2 { get; set; }

        public virtual MsGaLetterNormal? LetterNorm { get; set; }
        public virtual MsGaPeriodicBook? PeriodicBook { get; set; }
    }
}
