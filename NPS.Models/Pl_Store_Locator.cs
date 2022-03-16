using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPS.Models
{
    public class Pl_Store_Locator
    {
        [Key]
        public int Store_Code { get; set; }
        public string Business_Name { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal_Code { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Short_URL { get; set; }
        public string Primary_Phone { get; set; }
        public string Additional_Phones { get; set; }
        public string Store_Type { get; set; }
        public string Weekly_Off { get; set; }
        public string Credit_Cards_Payments { get; set; }
        public string Digital_Payments { get; set; }
        public string Sunday_Hours { get; set; }
        public string Monday_Hours { get; set; }
        public string Tuesday_Hours { get; set; }
        public string Wednesday_Hours { get; set; }
        public string Thursday_Hours { get; set; }
        public string Friday_Hours { get; set; }
        public string Saturday_Hours { get; set; }
        public string Special_Hours { get; set; }
        public string Active { get; set; }
        public string Created_By { get; set; }

        [NotMapped]
        public string Creation_Date { get; set; }

        [NotMapped]
        public string Modified_By { get; set; }

        [NotMapped]
        public string Modification_Date { get; set; }

    }
}
