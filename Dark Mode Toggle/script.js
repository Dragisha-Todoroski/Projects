const darkModeBtn = document.querySelector('#darkModeBtn');

darkModeBtn.addEventListener('click', darkLightModeSwitch);

function darkLightModeSwitch() {
    document.body.classList.toggle('darkMode');
    darkModeBtn.classList.toggle('lightMode');
}
