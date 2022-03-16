using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NPS.Models;

namespace NPS.Services
{
    public class SQLNpsRepository : INPSRespository
    {

        //private readonly AppDbContext context;
        private readonly Func<AppDbContext> _contextFactory;
        public IEnumerable<Pl_NPS_Chart> Get_NPS_Month => Get_Chart();

        public IEnumerable<Pl_Region_NPS> Get_NPS_Month_Region => Get_Region();

        public IEnumerable<Pl_NPS_Category> Get_NPS_Month_Category => Get_Chart().GroupBy(l => l.Category).Select(g => new Pl_NPS_Category { Category = g.Key, Count = g.Select(l => l.Category).Count() }).OrderBy(x => x.Category);

       

        public SQLNpsRepository(Func<AppDbContext> context)
        {
            _contextFactory = context;
        }

        
        public Pl_NPS Add_NPS(Pl_NPS pl_new_nps)
        {

            //context.pl_nps.Add(pl_new_nps);
            //context.SaveChanges();
            //return pl_new_nps;
            using (var context = _contextFactory.Invoke())
            { 

                context.Database.ExecuteSqlRaw("sp_Insert_NPS_Feedback {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                                       pl_new_nps.surveyResult.SR_No,
                                       pl_new_nps.surveyResult.Region,
                                       pl_new_nps.Is_Satisfied,
                                       pl_new_nps.nps_score,
                                       pl_new_nps.feedback,
                                       pl_new_nps.nps_reason,
                                       pl_new_nps.surveyResult.Source,
                                       pl_new_nps.Created_By,
                                       pl_new_nps.Creation_Date
                                       );
            return pl_new_nps;
            }
        }
        public Pl_NPS_SR Get_NPS_SR(string SR_No, string Region, string Source)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.pl_nps_Sr
                .FromSqlRaw<Pl_NPS_SR>("sp_Get_NPS_SRNo {0},{1},{2}", SR_No, Region, Source)
                .ToList()
                .FirstOrDefault();
            }
        }

        public IEnumerable<Pl_NPS_Chart> Get_Chart()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.pl_nps_chart
                .FromSqlRaw<Pl_NPS_Chart>("sp_Get_NPS_Month")
                .ToList();
            }
        }


        public IEnumerable<Pl_Region_NPS> Get_Region()
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.pl_region_nps
                .FromSqlRaw<Pl_Region_NPS>("sp_Get_NPS_Month_Region")
                .ToList();
            }
        }


        //public List<Pl_Store_Locator> Get_Store_Locator(string Pincode)
        //{

        //    using (var context = _contextFactory.Invoke())
        //    {
        //        return context.pl_store_locator
        //        .FromSqlRaw<Pl_Store_Locator>("sp_GetStoreLocatorByPincode {0}", Pincode)
        //        .ToList();
        //    }

        //}


        public Pl_NPS_Aws Add_NPS_AWS(Pl_NPS_Aws pl_new_nps_aws)
        {

            //context.pl_nps.Add(pl_new_nps);
            //context.SaveChanges();
            //return pl_new_nps;
            using (var context = _contextFactory.Invoke())
            {

                context.Database.ExecuteSqlRaw("sp_Insert_NPS_Feedback {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                                       pl_new_nps_aws.SR_No,
                                       pl_new_nps_aws.Region,
                                       pl_new_nps_aws.Is_Satisfied,
                                       pl_new_nps_aws.Call_Rating,
                                       pl_new_nps_aws.Cust_Remark,
                                       pl_new_nps_aws.nps_reason,
                                       pl_new_nps_aws.Source,
                                       pl_new_nps_aws.Created_By,
                                       pl_new_nps_aws.Creation_Date
                                       );
                return pl_new_nps_aws;
            }
        }


    }
}
