namespace Entities.Models
{
	public class ApartmentParameters : QueryStringParameters
	{
		string _orderBy = "price";
		string _orderDirection = "asc";
		
		public decimal? MinPrice { get; set; } 
		public decimal? MaxPrice { get; set; } 

		public bool NotValidPrice => MinPrice.HasValue && MaxPrice.HasValue && MaxPrice < MinPrice;

		public int? HouseID { get; set; }

		public short? DistrictId { get; set; }

		override public string OrderBy 
		{ 
			get => _orderBy; 
			set 
			{ 
				if (value.ToLower() == "districtname") 
					_orderBy = "districtname";
				else
					_orderBy = "price";
			} 
		}
		override public string OrderDirection 
		{ 
			get => _orderDirection; 
			set 
			{ 
				if (value.ToLower() == "desc") 
					_orderDirection = "desc"; 
				else
					_orderDirection = "asc";
			} 
		}
	}
}
