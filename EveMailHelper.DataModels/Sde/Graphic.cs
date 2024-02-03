using EveMailHelper.DataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace EveMailHelper.DataModels.Sde
{
    public partial class Graphic : IBaseEveId, IBaseEveObject
    {
        public int EveId { get; set; }
        public string? SofFactionName { get; set; }
        public string? GraphicFile { get; set; }
        public string? SofHullName { get; set; }
        public string? SofRaceName { get; set; }
        public string Description { get; set; } = String.Empty;
        public DateTime EveLastUpdated { get; set; } = DateTime.Now;
        public bool EveDeletedInGame { get; set; } = false;
    }
}
