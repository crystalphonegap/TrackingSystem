var connetion_status = document.getElementById('connetion-status');


let MonthYear = () => {
    const monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var d = new Date();
    return  monthNames[d.getMonth()] + ' ' + d.getFullYear();

};

let startConnection = () => {
    connection.start()
        .then(e => {
            $(connetion_status).text("Live");
            $(connetion_status).addClass("badge badge-success");
            connection.invoke('GetRatingCount').catch(err => console.error(err.toString()));
            connection.invoke('GetPieChart').catch(err => console.error(err.toString()));
            connection.invoke('GetBarChart').catch(err => console.error(err.toString()));
        })
        //.catch(err => console.log(err));
        .catch(err => {
            $(connetion_status).text("offline");
            $(connetion_status).addClass("badge badge-danger");
        });
};

let GetRating = (Rating_Counts) => {
        //All span value will be zero
        for (var j = 0; j <= 10; j++) {
            var rating_span = document.getElementById('rating_' + j);
            $(rating_span).text(0);

        }
    //All value assign to span
    for (var i = 0; i < Rating_Counts.length; i++) {
        var rating_span = document.getElementById('rating_' + Rating_Counts[i].rating);
        switch (Rating_Counts[i].rating) {
                case 0:
                    //rating_1.text(Rating_Counts[i].count);
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 1:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 2:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 3:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 4:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 5:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 6:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 7:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 8:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 9:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                case 10:
                    $(rating_span).text(Rating_Counts[i].count);
                    break;
                default:
                    $(rating_span).text(0);
            }
        }
};


let arrayUnique = (array) => {
    var a = array.concat();
    for (var i = 0; i < a.length; ++i) {
        for (var j = i + 1; j < a.length; ++j) {
            if (a[i].category === a[j].category)
                a.splice(j--, 1);
        }
    }

    return a;
};

let GetDoughnut = (Donghnut) => {

    console.log(Donghnut);

    console.log(Donghnut.length);

    if (Donghnut.length < 3) {
        const array = [{ category: "DETRACTORS", count: 0 }, { category: "PASSIVES", count: 0 }, { category: "PROMOTERS", count: 0 }]
        Donghnut = arrayUnique(Donghnut.concat(array)).sort((a, b) => (a.category > b.category) ? 1 : -1)
    }
    console.log(Donghnut);
    config_Doughnut.data.datasets.forEach(function (dataset) {
        dataset.data = Donghnut.map(function (e) {
            return e.count;
        });
        config_Doughnut.data.labels = Donghnut.map(function (e) {
            return e.category;
        });

        let sum = dataset.data.reduce((a, b) => a + b, 0);
        let NPS_To = dataset.data[2] - dataset.data[0];
        let NPS = Math.round((NPS_To / sum) * 100);
        config_Doughnut.options.elements.center.text = NPS + '%';


        // NPS Score
        $("#span_nps_score").text(NPS + '%');

        //NPS Month Year
        $("#span_nps_month").text('/ for  '  + MonthYear());


        if (NPS <= 0) { $("#icon_nps").removeClass(); $("#span_nps_info").removeClass(); $("#icon_nps").addClass("mdi mdi-emoticon-sad bg-danger"); $("#span_nps_info").text('NEED IMPROVEMENT (-100 - 0 %)'); $("#span_nps_info").addClass('text-danger')  }
        else if (NPS <= 30) { $("#icon_nps").removeClass(); $("#span_nps_info").removeClass(); $("#icon_nps").addClass("mdi mdi-emoticon-neutral bg-warning"); $("#span_nps_info").text('GOOD (0 - 30 %)'); $("#span_nps_info").addClass('text-warning') }
        else if (NPS <= 70) { $("#icon_nps").removeClass(); $("#span_nps_info").removeClass(); $("#icon_nps").addClass("mdi mdi-emoticon bg-success"); $("#span_nps_info").text('GREAT (30 - 70 %)'); $("#span_nps_info").addClass('text-success') }
        else if (NPS > 70) { $("#icon_nps").removeClass(); $("#span_nps_info").removeClass(); $("#icon_nps").addClass("mdi mdi-emoticon-cool bg-success"); $("#span_nps_info").text('EXCELLENT (ABOVE 70 %)'); $("#span_nps_info").addClass('text-success') }
        // Value assigned to count 

        for (var i = 0; i < Donghnut.length; i++) {
            switch (Donghnut[i].category) {
                case "DETRACTORS":

                    //rating_1.text(Rating_Counts[i].count);
                    $("#rating_detractors").text(Donghnut[i].count);
                    break;
                case "PASSIVES":
                    $("#rating_passives").text(Donghnut[i].count);
                    break;
                case "PROMOTERS":
                    $("#rating_promotors").text(Donghnut[i].count);
                    break;
            }
        }

    });


    window.myDoughnut.update();
};

let GetMixedBar = (Get_NPS_Month_Region) => {
    //config_MixedChart.data.datasets.forEach(function (dataset) {

    config_MixedChart.data.datasets[0].data = Get_NPS_Month_Region.map(e => { return e.nps });
    config_MixedChart.data.datasets[1].data = Get_NPS_Month_Region.map(e => { return e.detractors });
    config_MixedChart.data.datasets[2].data = Get_NPS_Month_Region.map(e => { return e.passives });
    config_MixedChart.data.datasets[3].data = Get_NPS_Month_Region.map(e => { return e.promoters });
    config_MixedChart.data.labels = Get_NPS_Month_Region.map(e => { return e.region });
        //    return e.nps;
        //});
        //console.log(dataset[0].data);
        //dataset[1].data = Get_NPS_Month_Region.map(function (e) {
        //    return e.detractors;
        //});
        //dataset[2].data = Get_NPS_Month_Region.map(function (e) {
        //    return e.passives;
        //});
        //dataset[3].data = Get_NPS_Month_Region.map(function (e) {
        //    return e.promoters;
        //});
        //config_MixedChart.data.labels = Get_NPS_Month_Region.map(function (e) {
        //    return e.region;
        //});



        window.myMixedChart.update();
    //});
};


let connection = new signalR.HubConnectionBuilder().withUrl("/charts").build();
startConnection();
//connection.start().then(() => {
//    connection.invoke('GetRatingCount').catch(err => console.error(err.toString()));
//})


connection.onClosed = e => {
    if (e) {
        $(connetion_status).text("offline " + e);
        $(connetion_status).addClass("badge badge-danger");
    }
    else {
        $(connetion_status).text("offline " + e);
        $(connetion_status).addClass("badge badge-danger");
    }
    startConnection();
};

// Ratingcounts
connection.on('Rating', Rating_Counts => {
    GetRating(Rating_Counts);
});

// Pie chart
connection.on('chartPie', Pie_Chart => {
    GetDoughnut(Pie_Chart);
});

// Mixed Bar chart
connection.on('RegionChartBar', Get_NPS_Month_Region => {
    GetMixedBar(Get_NPS_Month_Region);
});





Chart.pluginService.register({
    id: 'p1',
    beforeDraw: function (chart) {
        var width = chart.chart.width,
            height = chart.chart.height,
            ctx = chart.chart.ctx;
        ctx.restore();
        var fontSize = (height / 180).toFixed(2);
        ctx.font = fontSize + "em sans-serif";
        //class="font-16"

        
        ctx.textBaseline = "middle";
        var text = chart.config.options.elements.center.text,
            textX = Math.round((width - ctx.measureText(text).width) / 2),
            textY = height / 1.6;
        ctx.fillText(text, textX, textY);
        ctx.save();
    }
});

//Chart.helpers.merge(Chart.defaults.global, {
//    id: 'p2',
//    aspectRatio: 4 / 3,
//    tooltips: false,
//    layout: {
//        padding: {
//            top: 42,
//            right: 16,
//            bottom: 32,
//            left: 8
//        }
//    },
//    elements: {
//        line: {
//            fill: false
//        },
//        point: {
//            hoverRadius: 7,
//            radius: 5
//        }
//    },
//    plugins: {
//        legend: false,
//        title: false
//    }
//});



let config_Doughnut = {
    type: 'doughnut',
    data: {
        datasets: [{
            data: [],
            backgroundColor: [
                '#FF5733',
                '#fcbe2d',
                '#02c58d'
            ],
            label: 'Dataset 1'
        }],
        labels: []
    },
    options: {
        responsive: true,
        title: {
            display: true,
            text: "Doughnut Chart NPS: " + MonthYear(),
        },
        tooltips: {
            enabled: true
        },
        elements: {
            center: {
                text: '',
                color: '#36A2EB', //Default black
                fontStyle: 'Helvetica', //Default Arial
                sidePadding: 15 //Default 20 (as a percentage)
            }
        },
        plugins: {
            p2: false, 

            datalabels: {

                formatter: (value, ctx) => {

                    let datasets = ctx.chart.data.datasets;

                    if (datasets.indexOf(ctx.dataset) === datasets.length - 1) {
                        let sum = datasets[0].data.reduce((a, b) => a + b, 0);
                        let percentage = Math.round((value / sum) * 100) + '%';
                        return percentage;
                    } else {
                        return percentage;
                    }
                },
                color: '#fff',
            }
        }
    }
};




let config_MixedChart = {
    type: 'bar',
    data: {
        //labels: ["NORTH", "SOUTH", "WEST", "EAST", "MUMBAI", "TOTAL"],
        labels: [],
        datasets: [{
            data: [
               
            ],
            type: 'line',
            label: 'NPS',
            fill: true,
            lineTension: 0.5,
            backgroundColor: "rgba(48, 65, 155, 0.2)",
            borderColor: "#30419b",
            borderCapStyle: 'butt',
            borderDash: [],
            borderDashOffset: 0.0,
            borderJoinStyle: 'miter',
            pointBorderColor: "#30419b",
            
            pointBackgroundColor: "#30419b",
            pointBorderWidth: 1,
            pointHoverRadius: 5,
            pointHoverBackgroundColor: "#30419b",
            pointHoverBorderColor: "#30419b",
            pointHoverBorderWidth: 2,
            pointRadius: 4,
            pointHitRadius: 10
        },
            {
            label: 'DETRACTORS',
            backgroundColor: "#FF5733",
            yAxisID: "bar-y-axis",
            data: [
               
            ]
        }, {
            label: 'PASSIVES',
            backgroundColor: "#fcbe2d",
            yAxisID: "bar-y-axis",
            data: [
              
            ]
        }, {
            label: 'PROMOTERS',
            backgroundColor: '#02c58d',
              yAxisID: "bar-y-axis",

            data: [
               
            ]
        }]
    },
    options: {
        plugins: {
            p1: false,
            stacked100: { enable: true },
            datalabels: {
                //display: function (context) {
                //    return context.dataset.data[context.dataIndex] > 15;
                //},
                formatter: function (value, ctx1) {
                    return Math.round(ctx1.chart.tooltip._data.calculatedData[ctx1.datasetIndex][ctx1.dataIndex]) + '%';
                   
                },

                color: '#fff',
            }
        },
       
        title: {
            display: true,
            text: "Mixed Bar Chart NPS: " + MonthYear(),
        },
        tooltips: {
            enabled: true
        },
        responsive: true,
        scales: {
            xAxes: [{
                stacked: true,
                min: 0,
                max: 100,
                callback: function (value) { return value + "%" }
            }],
            yAxes: [{
                stacked: false,
                ticks: {
                    beginAtZero: true,
                    min: 0,
                    max: 100,
                    callback: function (value) { return value + "%" }
                }
            }, {
                id: "bar-y-axis",
                stacked: true,
                display: false, //optional
                ticks: {
                    beginAtZero: true,
                    min: 0,
                    max: 100,
                    callback: function (value) { return value + "%" }
                },
                type: 'linear'
            }]
        }
    }



}

window.onload = function () {
    let ctx = document.getElementById('doughnut').getContext('2d');
    window.myDoughnut = new Chart(ctx, config_Doughnut);

    //let ctx1 = document.getElementById('mixed').getContext('2d');
    //window.myMixedChart = new Chart(ctx1, config_MixedChart);


    var ctx1 = document.getElementById('mixed').getContext('2d');
    window.myMixedChart = new Chart(ctx1, config_MixedChart);
    
};
