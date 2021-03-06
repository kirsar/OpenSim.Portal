﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OpenSim.Portal.Model
{
    public class Presentation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }

        internal ICollection<ServerPresentation> ServerPresentationBackRef { get; set; }

        internal ICollection<SimulationPresentation> SimulationPresentationBackRef { get; set; }
        [NotMapped] public IEnumerable<Simulation> Simulations => SimulationPresentationBackRef.Select(s => s.Simulation);
    }
}