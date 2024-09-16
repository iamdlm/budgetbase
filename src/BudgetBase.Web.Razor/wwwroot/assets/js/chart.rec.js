function getRecurringTable(handler) {
    // Construct FormData
    const formData = new FormData();
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;
    const type = document.querySelector('#rectype').value;

    // Append the RequestVerificationToken to the FormData
    formData.append('__RequestVerificationToken', token);
    formData.append('type', type);

    // Make a POST request to the Razor page's action
    return fetch(handler, {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            return data;
        })
        .catch(error => {
            console.error(error);
        });
}

async function getRecurringData() {
    try {
        const data = await getRecurringTable('dashboard?handler=GetRecurringData');

        const tableBody = document.querySelector('.table tbody');
        tableBody.innerHTML = ''; // Clear existing table rows

        Object.entries(data).forEach(([key, value]) => {
            const row = document.createElement('tr');
            const cellFrequency = document.createElement('td');
            const cellTotal = document.createElement('td');

            cellFrequency.textContent = key;
            cellTotal.textContent = value;

            row.appendChild(cellFrequency);
            row.appendChild(cellTotal);

            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error(error);
        // Handle any errors, e.g., update the UI to show an error message
    }
}