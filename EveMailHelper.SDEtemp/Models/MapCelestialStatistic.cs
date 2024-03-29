﻿using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class MapCelestialStatistic
    {
        public int CelestialId { get; set; }
        public double? Temperature { get; set; }
        public string? SpectralClass { get; set; }
        public double? Luminosity { get; set; }
        public double? Age { get; set; }
        public double? Life { get; set; }
        public double? OrbitRadius { get; set; }
        public double? Eccentricity { get; set; }
        public double? MassDust { get; set; }
        public double? MassGas { get; set; }
        public bool? Fragmented { get; set; }
        public double? Density { get; set; }
        public double? SurfaceGravity { get; set; }
        public double? EscapeVelocity { get; set; }
        public double? OrbitPeriod { get; set; }
        public double? RotationRate { get; set; }
        public bool? Locked { get; set; }
        public double? Pressure { get; set; }
        public double? Radius { get; set; }
        public int? Mass { get; set; }
    }
}
