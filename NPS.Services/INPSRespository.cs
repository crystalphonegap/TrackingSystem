using System;
using System.Collections.Generic;
using NPS.Models;

namespace NPS.Services
{
    public interface INPSRespository
    {
        Pl_NPS_SR Get_NPS_SR(string SR_No, string Region, string Source);


        Pl_NPS Add_NPS(Pl_NPS pl_nps);

        IEnumerable<Pl_NPS_Chart> Get_NPS_Month { get; }

        IEnumerable<Pl_Region_NPS> Get_NPS_Month_Region { get; }

        IEnumerable<Pl_NPS_Category> Get_NPS_Month_Category { get; }

        Pl_NPS_Aws Add_NPS_AWS(Pl_NPS_Aws pl_new_nps_aws);

        //List<Pl_Store_Locator> Get_Store_Locator(string Pincode);

    }
}
