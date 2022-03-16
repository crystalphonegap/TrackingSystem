using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPS.Models
{
    public class Pl_NPS
    {
        [Key]
        public string SR_No { get; set; }
        public Pl_NPS_SR surveyResult { get; set; }
        public string Is_Satisfied { get; set; }
        public int nps_score { get; set; }
        [NotMapped]
        public List<string> detrators_nps_reason { get; set; }
        [NotMapped]
        public List<string> passive_nps_reason { get; set; }
        [NotMapped]
        public List<string> promoter_nps_reason { get; set; }
        [NotMapped]
        public string nps_reason { get; set; }
        public string feedback { get; set; }
        public string Created_By { get; set; }
        public DateTime Creation_Date { get; set; }


    }
    public class Pl_NPS_SR
    {
        [Key]
        public string SR_No { get; set; }
        public string Region { get; set; }
        public string Is_NPS { get; set; }
        public string Source { get; set; }


    }

    //public class Parameter
    //{
    //    public string Is_Satisfied { get; set; }
    //    public int nps_score { get; set; }
    //    public List<string> detrators_nps_reason { get; set; }
    //    public List<string> passive_nps_reason { get; set; }
    //    public string feedback { get; set; }



    //}
    [Table("NPS_Survey")]
    public class Pl_NPS_Chart
    {
        [Key]
        [JsonProperty(PropertyName = "SR_No")]
        public string SR_No { get; set; }

        [JsonProperty(PropertyName = "Region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "Satisfaction")]
        public string Satisfaction { get; set; }

        [JsonProperty(PropertyName = "Rating")]
        public int Rating { get; set; }

        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }


        
    }

    public class Pl_Region_NPS
    {
        [Key]
        [JsonProperty(PropertyName = "Region")]
        public string Region { get; set; }

        [JsonProperty(PropertyName = "DETRACTORS")]
        public int DETRACTORS { get; set; }
        [JsonProperty(PropertyName = "PASSIVES")]
        public int PASSIVES { get; set; }
        [JsonProperty(PropertyName = "PROMOTERS")]
        public int PROMOTERS { get; set; }

        [JsonProperty(PropertyName = "NPS")]
        public int NPS { get; set; }

    }

    public class Pl_NPS_Category
    {
        [Key]
        [JsonProperty(PropertyName = "Category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }

    }


    public class Pl_NPS_Aws
    {
        [Key]
        public string SR_No { get; set; }
        public string Region { get; set; }

        public string Is_Satisfied { get; set; }
        public int Call_Rating { get; set; }
        public string Cust_Remark { get; set; }
        public string Source { get; set; }

        public string nps_reason { get; set; }
        
        public string Created_By { get; set; }

        public DateTime Creation_Date { get; set; }

    }
}
