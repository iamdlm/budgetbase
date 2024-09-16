function getChart(handler) {
    // Construct FormData
    const formData = new FormData();
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const period = document.querySelector('#timeperiod').value;

    // Append the RequestVerificationToken to the FormData
    formData.append('__RequestVerificationToken', token);
    formData.append('period', period);

    // Make a POST request to the Razor page's action
    return fetch(handler, {
        method: 'POST',
        body: formData
    })
        .then(response => response.text())
        .then(data => {
            return data;
        })
        .catch(error => {
            console.error(error);
        });
}

let chartInstance = null;
async function getPLData() {
    try {
        const response = await getChart('dashboard?handler=GetPLData');
        data = JSON.parse(response);

        var options = {
            series: data.series,
            chart: {
                type: 'bar',
                height: 350,
                stacked: true,
            },
            stroke: {
                width: 1,
                colors: ['#fff']
            },
            dataLabels: {
                formatter: (val) => {
                    return Math.round((val / 1000) * 100) / 100 + 'K'
                }
            },
            plotOptions: {
                bar: {
                    horizontal: false
                }
            },
            xaxis: data.xaxis,
            fill: {
                opacity: 1
            },
            yaxis: {
                labels: {
                    formatter: (val) => {
                        return Math.round((val / 1000) * 100) / 100 + 'K'
                    }
                }
            },
            legend: {
                position: 'top',
                horizontalAlign: 'left'
            }
        };

        if (chartInstance) {
            chartInstance.destroy();
        }
        chartInstance = new ApexCharts(document.querySelector("#pl-chart"), options);
        chartInstance.render();
    } catch (error) {
        console.error(error);
        // Handle any errors, e.g., update the UI to show an error message
    }
}