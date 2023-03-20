namespace Entities.DTOs
{
    public partial class ApartmentsDTO
    {
        public int ApartmentId { get; set; }
        public short RoomsCount { get; set; }
        public decimal Sall { get; set; }
        public short Floor { get; set; }
        public decimal Price { get; set; }

        public int HouseId { get; set; }

        //from house

        public short HouseStage { get; set; }
        public string HouseNumber { get; set; }
        public string ComplexNameOfHouse { get; set; }


        //from district
        public string DistrictName { get; set; }
        public string RegionName { get; set; }


    }
}
