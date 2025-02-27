
/**
* Theme: Syntra Admin Template
* Author: Mannat-themes
* chartjs
*/


(function ($) {
    "use strict";
    //Team chart
    //var ctx = document.getElementById("team-chart");
    //ctx.height = 150;
    //var myChart = new Chart(ctx, {
    //    type: 'line', data: {
    //        labels: ["2010", "2011", "2012", "2013", "2014", "2015", "2016"],
    //        type: 'line', defaultFontFamily: 'Montserrat',
    //        datasets: [{
    //            data: [0, 7, 3, 5, 2, 10, 7],
    //            label: "Expense",
    //            backgroundColor: '#b9dcec',
    //            borderColor: '#f572a1',
    //            borderWidth: 3.5,
    //            pointStyle: 'circle',
    //            pointRadius: 5,
    //            pointBorderColor: 'transparent',
    //            pointBackgroundColor: '#f572a1',
    //        }
    //            ,]
    //    }
    //    , options: {
    //        responsive: true, tooltips: {
    //            mode: 'index', titleFontSize: 12, titleFontColor: '#000', bodyFontColor: '#000', backgroundColor: '#fff', titleFontFamily: 'Montserrat', bodyFontFamily: 'Montserrat', cornerRadius: 3, intersect: false,
    //        }
    //        , legend: {
    //            display: false, position: 'top', labels: {
    //                usePointStyle: true, fontFamily: 'Montserrat',
    //            }
    //            ,
    //        }
    //        , scales: {
    //            xAxes: [{
    //                display: true, gridLines: {
    //                    display: false, drawBorder: false
    //                }
    //                , scaleLabel: {
    //                    display: false, labelString: 'Month'
    //                }
    //            }
    //            ], yAxes: [{
    //                display: true, gridLines: {
    //                    display: false, drawBorder: false
    //                }
    //                , scaleLabel: {
    //                    display: true, labelString: 'Value'
    //                }
    //            }
    //            ]
    //        }
    //        , title: {
    //            display: false,
    //        }
    //    }
    //}
    //);
    //bar chart
    //var ctx = document.getElementById("barChart");
    //ctx.height = 160;
    //var myChart = new Chart(ctx, {
    //    type: 'bar', data: {
    //        labels: ["January", "February", "March", "April", "May", "June", "July"], datasets: [{
    //            label: "My First dataset", data: [65, 59, 80, 81, 56, 55, 40], borderColor: "rgba(155, 241, 225, 0.5)", borderWidth: "0", backgroundColor: "rgba(155, 241, 225, 0.5)"
    //        }
    //            , {
    //            label: "My Second dataset", data: [28, 48, 40, 19, 86, 27, 90], borderColor: "rgba(196,88,80,0.09)", borderWidth: "0", backgroundColor: "rgba(196,88,80,0.07)"
    //        }
    //        ]
    //    }
    //    , options: {
    //        scales: {
    //            yAxes: [{
    //                ticks: {
    //                    beginAtZero: true
    //                }
    //            }
    //            ]
    //        }
    //    }
    //}
    //);
    //line chart
    var ctx = document.getElementById("lineChart");
    ctx.height = 150;
    var myChart = new Chart(ctx, {
        type: 'line', data: {
            labels: ["January", "February", "March", "April", "May", "June", "July"], datasets: [{
                label: "My First dataset", borderColor: "rgba(0,0,0,.09)", borderWidth: "1", backgroundColor: "rgba(22,205,250,0.07)", data: [22, 44, 67, 43, 76, 45, 12]
            }
                , {
                label: "My Second dataset", borderColor: "rgba(30, 50,100, 0.9)", borderWidth: "1", backgroundColor: "rgba(245, 114, 161, 0.5)", pointHighlightStroke: "rgba(26,179,148,1)", data: [16, 32, 18, 26, 42, 33, 44]
            }
            ]
        }
        , options: {
            responsive: true, tooltips: {
                mode: 'index', intersect: false
            }
            , hover: {
                mode: 'nearest', intersect: true
            }
        }
    }
    );
    //pie chart
    //var ctx = document.getElementById("pieChart");
    //ctx.height = 150;
    //var myChart = new Chart(ctx, {
    //    type: 'pie', data: {
    //        datasets: [{
    //            data: [25, 20, 15], backgroundColor: ["#254080", "#b59bc9", "#cccc99"], hoverBackgroundColor: ["#254080", "#b59bc9", "#cccc99"]
    //        }
    //        ], labels: ["navy blue", "purple", "yellow"]
    //    }
    //    , options: {
    //        responsive: true
    //    }
    //}
    //);
    //doughut chart





    //var ctx = document.getElementById("doughutChart");
    //ctx.height = 150;
    //var myChart = new Chart(ctx, {
    //    type: 'doughnut', data: {
    //        datasets: [{
    //            data: [55, 40, 30, 20, 10, 5], backgroundColor: ["#242440", "#8596a7", "#a7cccc", "#E9B9D3", "#FF9F32", "#27006A"], hoverBackgroundColor: ["#242440", "#8596a7", "#a7cccc", "#E9B9D3", "#FF9F32", "#27006A"]
    //        }
    //        ], labels: ["ASP", "AddI SP", "SP", "AddI DIG", "DIG", "AddI IGP"]
    //    }
    //    , options: {
    //        responsive: true
    //    }
    //}
    //);

    //polar chart
    var ctx = document.getElementById("polarChart");
    ctx.height = 150;
    var myChart = new Chart(ctx, {
        type: 'polarArea', data: {
            datasets: [{
                data: [15, 18, 9, 19], backgroundColor: ["#242440", "#8596a7", "#a7cccc", "#eeeedb"]
            }
            ], labels: ["blue", "grey", "green", "yellow"]
        }
        , options: {
            responsive: true
        }
    }
    );
    // single bar chart
    var ctx = document.getElementById("singelBarChart");
    ctx.height = 150;
    var myChart = new Chart(ctx, {
        type: 'bar', data: {
            labels: ["ASP", "AddI SP", "SP", "AddI DIG", "DIG", "AddI IGP"], datasets: [{
                label: "", data: [80, 70, 20, 30, 10, 5], borderColor: "rgba(60,186,159,1)", borderWidth: "0", backgroundColor: "rgba(60,186,159,0.2)"
            }
            ]
        }
        , options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }
                ]
            }
        }
    }
    );

}

)(jQuery);