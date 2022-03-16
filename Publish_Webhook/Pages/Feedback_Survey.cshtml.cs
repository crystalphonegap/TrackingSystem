using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NPS.Services;
using NPS.Models;
using System.Text.Json;

namespace WhatsApp_Webhook.Pages
{
    public class Feedback_Survey : PageModel
    {
        private readonly INPSRespository npsRepository;
        public Pl_NPS NPS { get; set; }
        public Pl_NPS_SR NPS_SR { get; set; }
        public string SurveyResult { get; set; }
        public string Message { get; set; }

        public Feedback_Survey(INPSRespository npsRepository)
        {
            this.npsRepository = npsRepository;
        }
        public IActionResult OnGet(string SR_No, string Region, string Source)
        {
            NPS_SR = npsRepository.Get_NPS_SR(SR_No, Region, Source);
            SurveyResult = JsonSerializer.Serialize<Pl_NPS_SR>(NPS_SR);

            if (NPS_SR.Is_NPS == "Y")
            {
                Message = "Thank you for your feedback. We've recorded your response. This will help us in improving further.";
                return RedirectToPage("Message", new { Message = Message } );
            }
            return Page();

    }
     

        public async Task<IActionResult> OnPostSaveAsync(string data)
        {

            NPS = JsonSerializer.Deserialize<Pl_NPS>(data);
            NPS.Created_By = "270203";
            NPS.Creation_Date = DateTime.Now;

            if (NPS.detrators_nps_reason != null)
            {
                NPS.nps_reason = JsonSerializer.Serialize<List<string>>(NPS.detrators_nps_reason);
            }
            else if (NPS.passive_nps_reason != null)
            {
                NPS.nps_reason = JsonSerializer.Serialize<List<string>>(NPS.passive_nps_reason);
            }


            npsRepository.Add_NPS(NPS);
            //var json  = JsonSerializer.Serialize<List<string>>(NPS.detrators_nps_reason);


            return await Task.FromResult<IActionResult>(RedirectToPage(""));
        }
    }
}
