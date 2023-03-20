using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Houses
    {
        public Houses()
        {
            Apartments = new HashSet<Apartments>();
        }

        public int HouseId { get; set; }
        public short StageNumber { get; set; }
        public string HouseNumber { get; set; }
        public string ComplexName { get; set; }
        public short DistrictId { get; set; }

        public virtual Districts District { get; set; }
        public virtual ICollection<Apartments> Apartments { get; set; }
    }
}
