// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace iSpan_final_service.Models
{
    public partial class GymOrderEquipment
    {
        public int GymOrderId { get; set; }
        public int EquipmentId { get; set; }
        public int Num { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual GymOrder GymOrder { get; set; }
    }
}