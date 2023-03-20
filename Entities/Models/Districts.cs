using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Districts
    {
        public Districts()
        {
            Houses = new HashSet<Houses>();
        }

        public short DistrictId { get; set; }
        public string DistrictName { get; set; }
        public short RegionId { get; set; }

        public virtual Regions Region { get; set; }
        public virtual ICollection<Houses> Houses { get; set; }
    }
}
