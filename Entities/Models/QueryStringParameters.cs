namespace Entities.Models
{
	public abstract class QueryStringParameters
	{
		const int maxPageSize = 50; //TODO: move to Configuration
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 5;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}

		virtual public string OrderBy { get; set; }
		virtual public string OrderDirection { get; set; }
	}
}
