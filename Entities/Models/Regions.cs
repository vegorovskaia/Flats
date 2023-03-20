using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Regions
    {
        public Regions()
        {
            Districts = new HashSet<Districts>();
        }

        public short RegionId { get; set; }
        public string RegionName { get; set; }

        public virtual ICollection<Districts> Districts { get; set; }
    }
}
