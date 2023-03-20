namespace Entities.Models
{
    public partial class Apartments
    {
        public int ApartmentId { get; set; }
        public int HouseId { get; set; }
        public short RoomsCount { get; set; }
        public decimal Sall { get; set; }
        public short Floor { get; set; }
        public decimal Price { get; set; }

        public virtual Houses House { get; set; }
    }
}
