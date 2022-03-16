using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency;
using NPS.Models;
using NPS.Services;
using TableDependency.SqlClient;
using WhatsApp_Webhook.Hubs;
using System.Data;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR;
using System.Data.SqlClient;

namespace WhatsApp_Webhook.Hubs
{

    public class NPSDatabaseSubscription : IDatabaseSubscription
    {
        private bool _disposedValue = false;
        private readonly INPSRespository _repository;
        private readonly IHubContext<Chart> _hubContext;
        private SqlTableDependency<Pl_NPS_Chart> _tableDependency;
        //private SqlConnection _sqlConnection;
        //private SqlCommand _sqlcommand;
        public NPSDatabaseSubscription(INPSRespository repository, IHubContext<Chart> hubContext)
        {
            _repository = repository;
            _hubContext = hubContext;
        }
        public void Configure(string connectionString)
        {
            //_sqlConnection = new SqlConnection(connectionString);
            //_sqlcommand.Connection = _sqlConnection;
            //_sqlcommand.Connection.OpenAsync();
            //_sqlcommand.CommandType = CommandType.StoredProcedure;
            //_sqlcommand.CommandText = "sp_Get_NPS_Month";

            _tableDependency = new SqlTableDependency<Pl_NPS_Chart>(connectionString);
            //_tableDependency = new SqlTableDependency<Pl_NPS_Chart>(connectionString);
            _tableDependency.OnChanged += Changed;
            _tableDependency.OnError += TableDependency_OnError;
            _tableDependency.Start();

            Console.WriteLine("Waiting for receiving notifications...");
        }



        private void TableDependency_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
        }

        private void Changed(object sender, RecordChangedEventArgs<Pl_NPS_Chart> e)
        {
            if (e.ChangeType != ChangeType.None)
            {
                // TODO: manage the changed entity
                var changedEntity = e.Entity;
                // var js = JsonConvert.SerializeObject(_repository.Get_NPS_Month.GroupBy(l => l.Category).Select(g => new { Category = g.Key, Count = g.Select(l => l.Category).Count() }));
                //_hubContext.Clients.All.Invoke();\
                //Chart c = new Chart(_repository);
                //c.GetPieChart();
                // Get Piechart
                //var Pie_Chart = _repository.Get_NPS_Month.GroupBy(l => l.Category).Select(g => new { Category = g.Key, Count = g.Select(l => l.Category).Count() }).OrderBy(x => x.Category);
                _hubContext.Clients.All.SendAsync("chartPie", _repository.Get_NPS_Month_Category);
                // Get Rating Counts
                var Rating_Counts = _repository.Get_NPS_Month.GroupBy(l => l.Rating).Select(g => new { Rating = g.Key, Count = g.Select(l => l.Rating).Count() }).OrderBy(x => x.Rating);
                _hubContext.Clients.All.SendAsync("Rating", Rating_Counts);

                // Get Mixed Bar chart
                _hubContext.Clients.All.SendAsync("RegionChartBar", _repository.Get_NPS_Month_Region);


            }
        }

        //private async Task  Changed(object sender, RecordChangedEventArgs<Pl_NPS_Chart> e)
        //{
        //    if (e.ChangeType != ChangeType.None)
        //    {
        //        // TODO: manage the changed entity
        //        var changedEntity = e.Entity;
        //        var js = JsonConvert.SerializeObject(_repository.Get_NPS_Month);
        //        //Clients.All.SendAsync("chart", js);

        //        await _hubContext.Clients.All.inv=("chart", js);
        //    }
        //}


        ~NPSDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}