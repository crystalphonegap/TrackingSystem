using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPS.Services;
using NPS.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace WhatsApp_Webhook.Pages
{
    public class Feedback_Survey : PageModel
    {
        private readonly INPSRespository npsRepository;
        public Pl_NPS NPS { get; set; }
        public Pl_NPS_SR NPS_SR { get; set; }

        public IEnumerable<Pl_NPS_Chart>  NPS_Chart { get; set; }
        public string SurveyResult { get; set; }
        public string Message { get; set; }

        public Feedback_Survey(INPSRespository npsRepository)
        {
            this.npsRepository = npsRepository;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Created_By { get; set; }

        }
        public IActionResult OnGet(string SR_No, string Region,string Created_By, string Source)

        {


            
            NPS_SR = npsRepository.Get_NPS_SR(SR_No, Region,Source);
       
            SurveyResult = JsonSerializer.Serialize<Pl_NPS_SR>(NPS_SR);

            if (NPS_SR.Is_NPS == "Y")
            {
                Message = "Thank you for your feedback. We've recorded your response. This will help us in improving further.";
                return RedirectToPage("Message", new { Message = Message } );
            }
            Input = new InputModel
            {
                Created_By = Created_By
            };
            return Page();

    }


        public IActionResult OnPostSave(string data)
        {
            //var x =  npsRepository.Get_NPS_Month.GroupBy(l => l.Category).Select(g => new { Category = g.Key, Count = g.Select(l => l.Category).Count()});
            NPS = JsonSerializer.Deserialize<Pl_NPS>(data);
            NPS.Created_By = Input.Created_By;

            NPS.Creation_Date = DateTime.Now;

            if (NPS.detrators_nps_reason != null)
            {
                NPS.nps_reason = JsonSerializer.Serialize<List<string>>(NPS.detrators_nps_reason);
            }
            else if (NPS.passive_nps_reason != null)
            {
                NPS.nps_reason = JsonSerializer.Serialize<List<string>>(NPS.passive_nps_reason);
            }
            else if (NPS.promoter_nps_reason != null)
            {
                NPS.nps_reason = JsonSerializer.Serialize<List<string>>(NPS.promoter_nps_reason);
            }


            npsRepository.Add_NPS(NPS);
            //var json  = JsonSerializer.Serialize<List<string>>(NPS.detrators_nps_reason);



            return Page();
            //return await Task.FromResult<IActionResult>(RedirectToPage(""));
        }
    }
}
