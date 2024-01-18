'use strict';

(function () {
    const btns = document.querySelector('.btns');
    let prevInputs = calculator.querySelector('.inputs');
    let display = calculator.querySelector('.display');
    
    let currentValue = [];
    let totalValue = [];
    let currentOperator = '';
    let isOperatorActive = true;
    
    const updateUI = function () {
        display.textContent = currentValue.join('');
        
        // if (!currentOperator) return;
        // if (display.textContent.length > 0) prevInputs.textContent = `${display.textContent}${currentOperator}`;
        // else {
        //     prevInputs.textContent = '';
        //     // currentOperator = '';
        // }
    };
    
    // Reset back to zero state
    const clearDisplay = function () {
        currentValue = [];
        totalValue = [];
        currentOperator = '';
        isOperatorActive = true;
        display.textContent = '';
        prevInputs.textContent = '';
    };
    
    const calculate = function (operator) {
        totalValue = [totalValue.reduce((prev, cur) => {
            console.log(operator);
            switch (operator) {
                case '+':
                    return prev + cur;
                case '-':
                    return prev - cur;
                case '*':
                    return prev * cur;
                case '/':
                    return cur === 0 ? 0 : prev / cur;
                default:
                    clearDisplay();
                    break;
            }
        })];
        
        const totalValueStringToNumber = Number(totalValue.join(''));
    
        display.textContent = Number.isInteger(totalValueStringToNumber) ? totalValueStringToNumber : parseFloat(totalValueStringToNumber.toFixed(2));
        console.log(totalValue);
    };
    
    const removeLastCharacter = function () {
        currentValue.pop();
        updateUI();
    };
    
    const pushToCurrentValue = function (value) {
        currentValue.push(value);
        console.log(currentValue);
        updateUI();
    };
    
    const handleOperators = function (value) {
        if (isOperatorActive && currentValue.length !== 0) {
            totalValue.push(Number(currentValue.join('')));
            isOperatorActive = false;
            if (totalValue.length > 1) calculate(currentOperator);
        }
        
        if (value !== '=') {
            currentOperator = value;
            currentValue = [];
            isOperatorActive = true;
        }
    };
    
    btns.addEventListener('click', (e) => {
        e.preventDefault();
    
        if (e.target.value === 'clear') clearDisplay();
        else if (e.target.value === 'deleteOne') {
            if (isOperatorActive) removeLastCharacter();
        } else if (e.target.classList.contains('operationBtn')) handleOperators(e.target.value); 
        else pushToCurrentValue(e.target.value);
    });
    
    document.body.addEventListener('keydown', (e) => {
        switch (e.key) {
            case 'Escape':
                clearDisplay();
                break;
            case 'Backspace':
                if (isOperatorActive) removeLastCharacter();
                break;
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '.':
                pushToCurrentValue(e.key);
                break;
            case '+':
            case '-':
            case '*':
            case '/':
                handleOperators(e.key);
                break;
            case 'Enter':
            case '=':
                handleOperators('=');
        }
    });
})();

