using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NPS.Models;
using NPS.Services;
using Newtonsoft.Json;

namespace WhatsApp_Webhook.Hubs
{
    public class Chart : Hub
    {
        private readonly INPSRespository _repository;

        public IEnumerable<Pl_NPS_Chart> NPS_Chart { get; set; }
        public Chart(INPSRespository repository)
        {
            _repository = repository;
        }
        public void GetPieChart()
        {
          //var Pie_Chart = _repository.Get_NPS_Month.GroupBy(l => l.Category).Select(g => new { Category = g.Key, Count = g.Select(l => l.Category).Count() }).OrderBy(x => x.Category);
          Clients.All.SendAsync("chartPie", _repository.Get_NPS_Month_Category);
        }

        public void GetRatingCount()
        {
            var Rating_Counts = _repository.Get_NPS_Month.GroupBy(l => l.Rating).Select(g => new { Rating = g.Key, Count = g.Select(l => l.Rating).Count() }).OrderBy(x => x.Rating);
            Clients.All.SendAsync("Rating", Rating_Counts);
        }

        public void GetBarChart()
        {
            Clients.All.SendAsync("RegionChartBar", _repository.Get_NPS_Month_Region);
        }
    }
}
