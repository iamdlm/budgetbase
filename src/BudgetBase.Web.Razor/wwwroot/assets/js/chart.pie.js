function getPieChart(handler) {
    // Construct FormData
    const formData = new FormData();
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const period = document.querySelector('#timeperiod').value;
    const type = document.querySelector('#pietype').value;

    // Append the RequestVerificationToken to the FormData
    formData.append('__RequestVerificationToken', token);
    formData.append('period', period);
    formData.append('type', type);

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

let chartPieInstance = null;

async function getPieData() {
    try {
        const response = await getPieChart('dashboard?handler=GetPieData');
        data = JSON.parse(response);
        
        var pieOptions = {
            series: data.series,
            colors: data.colors,
            chart: {
                width: 380,
                type: 'pie',
            },
            labels: data.labels,
            dataLabels: {
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

        if (chartPieInstance) {
            chartPieInstance.destroy();
        }
        chartPieInstance = new ApexCharts(document.querySelector("#pie-chart"), pieOptions);
        chartPieInstance.render();
    } catch (error) {
        console.error(error);
        // Handle any errors, e.g., update the UI to show an error message
    }
}