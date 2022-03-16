
/*
 Template Name: Stexo - Responsive Bootstrap 4 Admin Dashboard
 Author: Themesdesign
 Website: www.themesdesign.in
 File: Chart js 
 */


"use strict";


var randomScalingFactor = function () {
    return Math.round(Math.random() * 100);
};
  

    var config = {
        type: 'doughnut',
        data: {
            datasets: [{
                data: [
                   30,20,40
                ],
                backgroundColor: [
                    'pink',
                    'Yellow',
                    'Green'
                ],
                label: 'Dataset 1'
            }],
            labels: [
                'DETRACTORS',
                'PASSIVES',
                'PROMOTERS'
            ]
        },
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: ' Doughnut Chart'
            },
            animation: {
                animateScale: true,
                animateRotate: true
            }
        }
    };

    window.onload = function () {
        var ctx = document.getElementById('doughnut').getContext('2d');
        window.myDoughnut = new Chart(ctx, config);
    };



