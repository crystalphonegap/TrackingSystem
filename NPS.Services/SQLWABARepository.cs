using Microsoft.EntityFrameworkCore;
using NPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NPS.Services
{
    public class SQLWABARepository : IWABARepository
    {
        //private readonly AppDbContext context;
        private readonly Func<AppDbContext> _contextFactory;

        public SQLWABARepository(Func<AppDbContext> context)
        {
            _contextFactory = context;
        }
      

        public List<Pl_Store_Locator> Get_Store_Locator(string Pincode)
        {

            using (var context = _contextFactory.Invoke())
            {
                return context.pl_store_locator
                .FromSqlRaw<Pl_Store_Locator>("sp_GetStoreLocatorByPincode {0}", Pincode)
                .ToList();
            }

        }

        public Pl_Product_Selection Get_ProductID(string Type_House, string City_Weather, string B2B_Bath, string Bath_Timing)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.pl_Product_Selection
                .FromSqlRaw<Pl_Product_Selection>("sp_GetProductID {0},{1},{2},{3}" , Type_House, City_Weather, B2B_Bath,Bath_Timing)
                .ToList().FirstOrDefault();
            }
        }

        public List<Pl_Product_Line> Get_Product(string Product_ID)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.pl_product_line
                .FromSqlRaw<Pl_Product_Line>("sp_GetProductByProductID {0}", Product_ID)
                .ToList();
            }
        }

       
    }
}
