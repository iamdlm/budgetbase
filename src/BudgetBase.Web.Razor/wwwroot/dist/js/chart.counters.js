function counter(elementId, value) {
    let obj = document.getElementById(elementId);
    if (!obj) return; // Exit if element not found

    let current = 0;
    value = Math.floor(value); // Ensure target value is an integer
    let range = Math.abs(value - current);
    let increment = value > 0 ? 1 : -1;
    let step = Math.max(Math.abs(Math.floor(500 / range)), 1); // Ensure step is at least 1ms

    let timer = setInterval(() => {
        current += increment;
        obj.textContent = current;
        if ((increment > 0 && current >= value) || (increment < 0 && current <= value)) {
            clearInterval(timer);
        }
    }, step);
}

function setMetricValue(elementId, value) {
    const el = document.getElementById(elementId);
    el.setAttribute('data-target', value);
    triggerCounterUpdate(el); // Trigger the update animation
}

function triggerCounterUpdate(counterElement) {
    const target = +counterElement.getAttribute('data-target');
    let duration = target < 1000 ? 1000 : target <= 9999 ? 2000 : 3000; // Duration based on target value

    const start = Date.now(); // Get the start time
    const from = +counterElement.innerText;
    const range = target - from;
    const minUpdateInterval = 20; // Minimum update interval in milliseconds

    const step = () => {
        const elapsed = Date.now() - start;
        const progress = Math.min(elapsed / duration, 1); // Ensure progress doesn't exceed 1

        counterElement.innerText = (from + range * progress).toFixed(0);

        if (progress < 1) {
            setTimeout(step, minUpdateInterval);
        } else {
            counterElement.innerText = target; // Ensure it exactly matches the target at the end
        }
    };

    step();
}

function getMetric(handler, elementId) {
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
            const numericData = parseFloat(data); // Convert data to a number
            setMetricValue(elementId, numericData);
            return numericData; // Return the numeric data for further processing
        })
        .catch(error => {
            console.error(error);
        });
}