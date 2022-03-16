using NPS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPS.Services
{
    public interface IWABARepository
    {
        List<Pl_Store_Locator> Get_Store_Locator(string Pincode);

        Pl_Product_Selection Get_ProductID(string Type_House, string City_Weather, string B2B_Bath, string Bath_Timing);


        List<Pl_Product_Line> Get_Product(string Product_ID);
    }
}
