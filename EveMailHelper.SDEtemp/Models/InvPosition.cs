using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class InvPosition
    {
        public int ItemId { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public float? Yaw { get; set; }
        public float? Pitch { get; set; }
        public float? Roll { get; set; }
    }
}
