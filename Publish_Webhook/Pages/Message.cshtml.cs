using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WhatsApp_Webhook.Pages
{
    public class MessageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        public void OnGet()
        {
           TempData["message"] = Message;
        }
    }
}
