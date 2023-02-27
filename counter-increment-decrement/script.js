const counterNum = document.querySelector('#counterNum');
const lowerCounterBtn = document.querySelector('#lowerCounterBtn');
const addCounterBtn = document.querySelector('#addCounterBtn');

counterNum.innerHTML = 0;

lowerCounterBtn.addEventListener('click', function() {
    counterNum.innerHTML = parseInt(counterNum.innerHTML) - 1;
    counterColorChange();
});

addCounterBtn.addEventListener('click', function() {
    counterNum.innerHTML = parseInt(counterNum.innerHTML) + 1;
    counterColorChange();
});

function counterColorChange() {
    counterNum.innerHTML < 0 ? counterNum.style.color = 'red' : counterNum.innerHTML > 0 ? counterNum.style.color = 'green' : counterNum.style.color = 'white';
};