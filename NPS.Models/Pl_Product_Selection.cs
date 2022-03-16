using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NPS.Models
{
    public class Pl_Product_Selection
    {
        [Key]
        public string Product_ID { get; set; }
    }
   
    public class Pl_Product_Header
    {
        [Key]
        public string Product_ID { get; set; }
        public string Type_House { get; set; }
        public string City_Weather { get; set; }
        public int B2B_Bath { get; set; }
        public string Bath_Timing { get; set; }
        public string Active { get; set; }
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

    }

    public class Pl_Product_Line
    {
        [Key]
        public int SrNo { get; set; }
        public string Product_ID { get; set; }
        public string Bath_Usage { get; set; }
        public string Product_Type { get; set; }
        public string Category { get; set; }
        public string Product { get; set; }
        public string Short_URL { get; set; }
        public string Active { get; set; }
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }

    }
}
