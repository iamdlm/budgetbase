var bgOptions = {
    series: [{
        name: 'Budget',
        data: [300, 150, 200, 100],
    }, {
        name: 'Total',
        data: [280, 180.60, 180.50, 80],
    }],
    chart: {
        height: 350,
        type: 'radar',
        dropShadow: {
            enabled: true,
            blur: 1,
            left: 1,
            top: 1
        }
    },
    xaxis: {
        categories: ['Travel', 'Clothing', 'Food', 'Going Out']
    }
};
var bgChart = new ApexCharts(document.querySelector("#bg-chart"), bgOptions);
bgChart.render();