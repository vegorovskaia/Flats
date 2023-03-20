using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Repo
{
	public class ApartmentsRepository : RepositoryBase<Apartments>, IApartmentsRepository
	{
		
		public ApartmentsRepository(flatsdbContext repositoryContext ): base(repositoryContext)
		{
		}

		public PagedList<Apartments> GetApartments(ApartmentParameters apartParameters)
		{
			Expression<Func<Apartments, bool>> expression = FilterExpression( apartParameters);

			var aparts = FindByCondition(expression);

			aparts = ToSort( aparts,  apartParameters);

			return PagedList<Apartments>.ToPagedList(aparts,
					apartParameters.PageNumber,
					apartParameters.PageSize);
		}

        // to form expression for filter
        private Expression<Func<Apartments, bool>> FilterExpression(ApartmentParameters apartParameters)
		{
			Expression finalExpression, expressHouseID, expressDistrictID, expressMinPrice, expressMaxPrice;
			expressHouseID = expressDistrictID = expressMinPrice = expressMaxPrice = Expression.Constant(true);

			var paramApart = Expression.Parameter(typeof(Apartments), "a");

			if (apartParameters.HouseID.HasValue)
			{
				var memberHouseID = Expression.Property(paramApart, "HouseID");
				var constant = Expression.Constant(apartParameters.HouseID.Value);
				expressHouseID = Expression.Equal(memberHouseID, constant);
			}
			else //filter by District have sense if there are no filter by House
				if (apartParameters.DistrictId.HasValue)
			{
				var memberHouse = Expression.Property(paramApart, "House");
				var memberDistrictID = Expression.Property(memberHouse, "DistrictID");
				var constantD = Expression.Constant(apartParameters.DistrictId);
				expressDistrictID = Expression.Equal(memberDistrictID, constantD);
			}
			var memberPrice = Expression.Property(paramApart, "Price");
			if (apartParameters.MinPrice.HasValue)
			{
				var constantMinPrice = Expression.Constant(apartParameters.MinPrice);
				expressMinPrice = Expression.LessThanOrEqual(constantMinPrice, memberPrice);
			}
			if (apartParameters.MaxPrice.HasValue)
			{
				var constantMaxPrice = Expression.Constant(apartParameters.MaxPrice);
				expressMaxPrice = Expression.LessThanOrEqual(memberPrice, constantMaxPrice);
			}

			finalExpression = Expression.And(Expression.And(expressHouseID, expressDistrictID), Expression.And(expressMinPrice, expressMaxPrice));

			Expression<Func<Apartments, bool>> expression = Expression.Lambda<Func<Apartments, bool>>(finalExpression, new[] { paramApart });
			return expression;
		}

			   		 

		public IOrderedQueryable<Apartments> ToSort(IQueryable<Apartments> aparts, ApartmentParameters apartParameters)
		{
			if (apartParameters.OrderDirection == "desc")
				if (apartParameters.OrderBy == "price")
					aparts = aparts.OrderByDescending(a => a.Price);
				else
					aparts = aparts.OrderByDescending(a => a.House.District.DistrictName);
			else
				if (apartParameters.OrderBy == "price")
				aparts = aparts.OrderBy(a => a.Price);
			else
				aparts = aparts.OrderBy(a => a.House.District.DistrictName);
			
			return (IOrderedQueryable<Apartments>)aparts;
		}

		public new IQueryable<Apartments> FindByCondition(Expression<Func<Apartments, bool>> expression)
		{

			return RepositoryContext.Apartments.Include(a => a.House).ThenInclude(a => a.District).ThenInclude(a => a.Region)
			.Where(expression)
			.AsNoTracking();
		}
		public Apartments GetApartmentById(int id)
		{
			return RepositoryContext.Apartments.AsNoTracking().First(a => a.ApartmentId == id);
		}
		public void UpdateApartment(Apartments apart)
		{
			Update(apart);
		}


		public void DeleteApartment(Apartments apart)
		{
			Delete(apart);
		}
	}
}