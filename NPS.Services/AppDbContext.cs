using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NPS.Models;

namespace NPS.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Pl_NPS> pl_nps { get; set; }
        public DbSet<Pl_NPS_SR> pl_nps_Sr { get; set; }

        public DbSet<Pl_NPS_Chart> pl_nps_chart { get; set; }

        public DbSet<Pl_Region_NPS> pl_region_nps { get; set; }

        public DbSet<Pl_NPS_Category> pl_category_nps { get; set; }


        public DbSet<Pl_Store_Locator> pl_store_locator { get; set; }

        public DbSet<Pl_Product_Header> pl_product_header { get; set; }

        public DbSet<Pl_Product_Line> pl_product_line { get; set; }

        public DbSet<Pl_Product_Selection> pl_Product_Selection { get; set; }

        //public DbSet<Pl_Gupshup> pl_json { get; set; }

        public DbSet<Pl_NPS_Aws> pl_NPS_AWS { get; set; }
        

    }

}