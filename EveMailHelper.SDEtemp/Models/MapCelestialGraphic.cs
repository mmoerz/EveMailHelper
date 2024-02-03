using System;
using System.Collections.Generic;

namespace EveMailHelper.SDEtemp.Models
{
    public partial class MapCelestialGraphic
    {
        public int CelestialId { get; set; }
        public int? HeightMap1 { get; set; }
        public int? HeightMap2 { get; set; }
        public int? ShaderPreset { get; set; }
        public bool? Population { get; set; }
    }
}
