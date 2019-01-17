using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class Presentation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }
    }
}