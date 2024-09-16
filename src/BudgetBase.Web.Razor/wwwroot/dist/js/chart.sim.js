let chartSimInstance = null;

function setSimChart(inc) {
    var series = [];

    if (inc != 0) {
        series = [inc * 0.5, inc * 0.3, inc * 0.2];
    };

    var simOptions = {
        series: series,
        chart: {
            width: 380,
            type: 'pie'
        },
        labels: ['Needs', 'Wants', 'Savings'],
        dataLabels: {
            enabled: true,
            formatter: function (val, opts) {
                return opts.w.config.series[opts.seriesIndex]
            },
            dropShadow: {
                enabled: false
            }
        },
        responsive: [{
            breakpoint: 480,
            options: {
                chart: {
                    width: 200
                },
                legend: {
                    position: 'bottom'
                }
            }
        }],
        noData: {
            text: "No data available in the selected period",
            align: 'center',
            verticalAlign: 'middle',
            offsetX: 0,
            offsetY: 0,
            style: {
                color: undefined,
                fontSize: '14px',
                fontFamily: undefined
            }
        }
    };

    if (chartSimInstance) {
        chartSimInstance.destroy();
    }

    chartSimInstance = new ApexCharts(document.querySelector("#sim-chart"), simOptions);
    chartSimInstance.render();
}