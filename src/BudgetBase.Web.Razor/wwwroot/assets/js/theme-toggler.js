document.addEventListener('DOMContentLoaded', function () {
    const lightBootstrapLink = document.getElementById('bootstrap-light');
    const darkBootstrapLink = document.getElementById('bootstrap-dark');
    const lightColorLink = document.getElementById('style-light');
    const darkColorLink = document.getElementById('style-dark');

    const themeToggle = document.getElementById('theme-toggle');
    let currentTheme = localStorage.getItem('theme') || (new Date().getHours() > 18 || new Date().getHours() < 6 ? 'dark' : 'light');

    // Function to fetch theme preference from server
    function fetchThemePreference() {
        if (themeToggle && themeToggle.dataset.authenticated === 'true') {
            fetch("/index?handler=GetThemePreference")
                .then(response => response.json())
                .then(data => {
                    currentTheme = data.theme;
                    localStorage.setItem('theme', currentTheme);
                    setTheme(currentTheme);
                    updateThemeButton(currentTheme);
                })
                .catch(error => {
                    console.error('Error fetching theme preference:', error);
                    setDefaultTheme();
                });
        } else {
            setDefaultTheme();
        }
    }

    // Function to set default theme
    function setDefaultTheme() {
        currentTheme = localStorage.getItem('theme') || (new Date().getHours() > 18 || new Date().getHours() < 6 ? 'dark' : 'light');
        setTheme(currentTheme);
        updateThemeButton(currentTheme);
    }
    if (themeToggle) {
        // Toggle theme on button click
        themeToggle.addEventListener('click', function () {
            currentTheme = currentTheme === 'light' ? 'dark' : 'light';

            // Save to local storage
            localStorage.setItem('theme', currentTheme);

            // If user is authenticated, save preference server-side
            if (themeToggle.dataset.authenticated === 'true') {
                updateThemePreference(currentTheme);
            }

            setTheme(currentTheme);
            updateThemeButton(currentTheme);
        });
    }

    // Fetch theme preference on page load
    fetchThemePreference();

    // Toggle theme function
    function setTheme(theme) {
        if (theme === 'dark') {
            darkBootstrapLink.removeAttribute('disabled');
            darkColorLink.removeAttribute('disabled');
            lightBootstrapLink.setAttribute('disabled', 'true');
            lightColorLink.setAttribute('disabled', 'true');
        } else {
            lightBootstrapLink.removeAttribute('disabled');
            lightColorLink.removeAttribute('disabled');
            darkBootstrapLink.setAttribute('disabled', 'true');
            darkColorLink.setAttribute('disabled', 'true');
        }
    }

    // Toggle button icon
    function updateThemeButton(theme) {
        if (theme === 'light') {
            themeToggle.innerHTML = `<div class="btn btn-icon btn-soft-light">
                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6" height="20">
                                            <path stroke-linecap="round" stroke-linejoin="round" d="M21.752 15.002A9.72 9.72 0 0 1 18 15.75c-5.385 0-9.75-4.365-9.75-9.75 0-1.33.266-2.597.748-3.752A9.753 9.753 0 0 0 3 11.25C3 16.635 7.365 21 12.75 21a9.753 9.753 0 0 0 9.002-5.998Z"/>
                                        </svg>
                                     </div>`;
        } else {
            themeToggle.innerHTML = `<div class="btn btn-icon btn-soft-light">
                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" class="size-6" height="20">
                                            <path stroke-linecap="round" stroke-linejoin="round" d="M12 3v2.25m6.364.386-1.591 1.591M21 12h-2.25m-.386 6.364-1.591-1.591M12 18.75V21m-4.773-4.227-1.591 1.591M5.25 12H3m4.227-4.773L5.636 5.636M15.75 12a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0Z"/>
                                        </svg>
                                     </div>`;
        }
    }

    function updateThemePreference(theme) {
        // Construct FormData
        const formData = new FormData();
        const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

        // Append the RequestVerificationToken to the FormData
        formData.append('__RequestVerificationToken', token);
        formData.append('theme', theme);

        // Submit the form to update theme server-side
        fetch("/index?handler=UpdateThemePreference", {
            method: "POST",
            body: formData
        }).then(response => {
            if (!response.ok) {
                console.error('Failed to update theme on server');
            }
        }).catch(error => {
            console.error('Error:', error);
        });
    }
});