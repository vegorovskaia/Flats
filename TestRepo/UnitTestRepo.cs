using Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Repo;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Linq.Expressions;
using System.Reflection;

namespace TestRepo
{
    [TestClass]
    public class UnitTestRepo
    {
        static List<Apartments> fixtureList;
        [ClassInitialize]
        public static void SetupTest(TestContext context)
        {
            Districts dist1 = new Districts() { DistrictId = 11, DistrictName = "First District" };
            Districts dist2 = new Districts() { DistrictId = 22, DistrictName = "Second District" };

            Houses house1 = new Houses() { HouseId = 111, DistrictId = 11, District = dist1 };
            Houses house2 = new Houses() { HouseId = 222, DistrictId = 22, District = dist2 };

            Apartments a1 = new Apartments() { ApartmentId = 1, HouseId = 111, Price = 4000001, House = house1 };
            Apartments a2 = new Apartments() { ApartmentId = 2, HouseId = 222, Price = 5000001, House = house2 };

            fixtureList = new List<Apartments>()
            {
                a1,a2
            };
    
        }

        private void TestSort(ApartmentParameters apartParameters)
        {
            Comparison<Apartments> comp = null;

            if (apartParameters.OrderBy == "price")
                if (apartParameters.OrderDirection == "asc")
                    comp = (a1, a2) => a1.Price > a2.Price ? 1 : -1;
                else
                    comp = (a1, a2) => a1.Price < a2.Price ? 1 : -1;
            
            if (apartParameters.OrderBy == "districtname")
                if (apartParameters.OrderDirection == "asc")
                    comp = (a1, a2) => a1.House.District.DistrictName.CompareTo( a2.House.District.DistrictName );
                else
                    comp = (a1, a2) => - a1.House.District.DistrictName.CompareTo(a2.House.District.DistrictName);

            fixtureList.Sort(comp);
            var repo = new ApartmentsRepository(null);
            var listTest = repo.ToSort(fixtureList.AsQueryable<Apartments>(), apartParameters).ToList();
            CollectionAssert.AreEqual(fixtureList, listTest);
        }



        [TestMethod]
        public void TestMethodSort_PriceAsc()
        {
            ApartmentParameters apartParameters = new ApartmentParameters(); 
            TestSort(apartParameters);
        }

        [TestMethod]
        public void TestMethodSort_PriceDesc()
        {
            ApartmentParameters apartParameters = new ApartmentParameters() { OrderDirection = "deSc" };
            TestSort(apartParameters);
        }
        [TestMethod]
        public void TestMethodSort_DistrictNameAsc()
        {
            ApartmentParameters apartParameters = new ApartmentParameters() { OrderBy = "districtName"  };
            TestSort(apartParameters);
        }

        [TestMethod]
        public void TestMethodSort_DistrictNameDesc()
        {
            ApartmentParameters apartParameters = new ApartmentParameters() { OrderBy = "districtName" , OrderDirection = "deSc" };
            TestSort(apartParameters);
        }

        private void  TestFilter(ApartmentParameters apartParameters, Predicate<Apartments> predicate)
        { //use reflection to test private method
            object[] prm = { null };
            Type type = typeof(ApartmentsRepository);
            var repo = new ApartmentsRepository(null);
            MethodInfo method = type.GetMethod("FilterExpression", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parametersArray = new object[] { apartParameters };
            Expression<Func<Apartments, bool>> resultExpr = (Expression<Func<Apartments, bool>>)method.Invoke(repo, parametersArray);
            Func<Apartments, bool> resultFunc = resultExpr.Compile();
            Predicate<Apartments> resultPredicate = new Predicate<Apartments>(resultFunc);
            var listTest = fixtureList.FindAll(resultPredicate);
            var listExpected = fixtureList.FindAll(predicate);
            CollectionAssert.AreEquivalent(listExpected, listTest);
        }


        [TestMethod]
        public void TestMethodDistrict()
        {
            short districtID = 11;
            ApartmentParameters apartParameters = new ApartmentParameters() { DistrictId = districtID };
            TestFilter(apartParameters, a => a.House.DistrictId == districtID);
        }


        [TestMethod]
        public void TestMethodHouse()
        {
            int houseID = 111;
            ApartmentParameters apartParameters = new ApartmentParameters() { HouseID = houseID };
            TestFilter(apartParameters, a => a.HouseId == houseID);
        }

        [TestMethod]
        public void TestMethodMinPrice()
        {
            decimal minPrice = 5000000;
            ApartmentParameters apartParameters = new ApartmentParameters() { MinPrice = minPrice };
            TestFilter(apartParameters, a => a.Price >= minPrice);
        }

        [TestMethod]
        public void TestMethodMaxPrice()
        {
            decimal maxPrice = 5000000;
            ApartmentParameters apartParameters = new ApartmentParameters() { MaxPrice = maxPrice };
            TestFilter(apartParameters, a => a.Price <= maxPrice);
        }

        [TestMethod]
        public void TestMethodComposed()
        {
            int houseID = 111;
            decimal minPrice = 4000001;
            decimal maxPrice = 4000001;
            ApartmentParameters apartParameters = new ApartmentParameters() { HouseID = houseID, MinPrice = minPrice ,  MaxPrice = maxPrice };
            TestFilter(apartParameters, a => a.HouseId == houseID && a.Price >= minPrice &&  a.Price <= maxPrice  );
        }

        [TestMethod]
        public void TestMethod_District_vs_House()  //if we know houseID, then districtID ignored
        {
            int houseID = 222;
            short districtID = 11;
            ApartmentParameters apartParameters = new ApartmentParameters() { HouseID = houseID, DistrictId = districtID };
            TestFilter(apartParameters, a => a.HouseId == houseID );
        }


    }
}
