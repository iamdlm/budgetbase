function applyRules() {
    // Construct FormData
    const formData = new FormData();
    const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

    // Append the RequestVerificationToken to the FormData
    formData.append('__RequestVerificationToken', token);

    // Make a POST request to the Razor page's action
    return fetch('transactions?handler=ApplyRules', {
        method: 'POST',
        body: formData
    })
        .then(response => response.text())
        .then(data => {
            if (data == true) {
                rulesModalInstance.hide();
                table.setData();

                document.getElementById('rules-text').classList.add('show');
                document.getElementById('rules-text').classList.remove('hide');

                document.getElementById('rules-status').classList.add('hide');
                document.getElementById('rules-status').classList.remove('show');
            }
            else {
                document.getElementById('rules-text').classList.add('hide');
                document.getElementById('rules-text').classList.remove('show');

                document.getElementById('rules-status').classList.add('show');
                document.getElementById('rules-status').classList.remove('hide');
            }
        })
        .catch(error => {
            console.error(error);
        });
}

var rulesModalInstance = null;
var rulesModelElem = document.querySelector('#applyRules');
rulesModelElem.addEventListener('shown.bs.modal', function () {
    rulesModalInstance = bootstrap.Modal.getInstance(rulesModelElem);
});