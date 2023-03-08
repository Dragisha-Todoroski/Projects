const quizzes = document.querySelector('#quiz');
const cssQuiz = document.querySelector('#cssQuiz');
const cssQuizAll = cssQuiz.querySelectorAll('button');
const startBtn = document.querySelector('#startBtn');
const jsQuiz = document.querySelector('#jsQuiz');
const jsQuizAll = jsQuiz.querySelectorAll('button');
const quizChoiceMenu = document.querySelector('#quizChoiceMenu');
const quizChoiceMenuAll = quizChoiceMenu.querySelectorAll('button');

quizChoiceMenuAll.forEach(btn => {
    btn.addEventListener('click', () => {
        if(btn === quizChoiceMenuAll[0]) {
            playQuiz(quizzes, cssQuiz, cssQuizAll, 1, 3, 8);
        }
        else if(btn === quizChoiceMenuAll[1]) {
            playQuiz(quizzes, jsQuiz, jsQuizAll, 2, 3, 6);
            
        }
        else {
            document.body.children[0].innerHTML = 'Sorry, this one doesn\'t work 😞';
        };
    });
});

const playQuiz = (overallQuizDiv, quiz, quizAll, correct1, correct2, correct3) => {
    quizChoiceMenu.classList.add('displayOnOff');
    document.body.children[0].classList.toggle('displayOnOff');
    startBtn.classList.toggle('displayOnOff');

    startBtn.addEventListener('click', () => {
        startBtn.classList.toggle('displayOnOff');
        quiz.children[0].classList.toggle('displayOnOff');

        const timerInitial = Date.now(); // time at start of quiz
        
        let correctAnswers = 0;
        
        quizAll.forEach(btn => {
            btn.addEventListener('click', () => {
                if(btn === quizAll[0] || btn === quizAll[1] || btn === quizAll[2]) {
                    if(btn === quizAll[correct1]) {
                        correctAnswers++;
                    }
                    quiz.children[0].classList.toggle('displayOnOff');
                    quiz.children[1].classList.toggle('displayOnOff');
                }
        
                if(btn === quizAll[3] || btn === quizAll[4] || btn === quizAll[5]) {
                    if(btn === quizAll[correct2]) {
                        correctAnswers++;
                    }
                    quiz.children[1].classList.toggle('displayOnOff');
                    quiz.children[2].classList.toggle('displayOnOff');
                }
        
                if(btn === quizAll[6] || btn === quizAll[7] || btn === quizAll[8]) {
                    const timerFinal = Date.now(); // time at end of quiz
                    const timeForCompletionInMilliseconds = timerFinal - timerInitial; // time spent in quiz in milliseconds

                    const timeForCompletionInSeconds = timeForCompletionInMilliseconds / 1000; // converted to seconds (with decimal spaces)
                    const seconds = Math.floor(timeForCompletionInSeconds % 60); // extract seconds under a minute and convert to integer

                    const timeForCompletionInMinutes = timeForCompletionInSeconds / 60;
                    const minutes = Math.floor(timeForCompletionInMinutes % 60); // same for minutes

                    const timeForCompletionInHours = timeForCompletionInMinutes / 60;
                    const hours = Math.floor(timeForCompletionInHours % 24); // same for hours

                    if(btn === quizAll[correct3]) {
                        correctAnswers++;
                    }
                    quiz.children[2].classList.toggle('displayOnOff');

                    overallQuizDiv.lastElementChild.innerHTML += `
                        <p>Time: ${hours < 10 ? '0' + hours : hours}:${minutes < 10 ? '0' + minutes : minutes}:${seconds < 10 ? '0' + seconds : seconds}</p>
                        <p>You got ${correctAnswers}/3 correct!</p>
                        <p>You ${correctAnswers < 2 ? '<span class="quizFail">failed</span> the quiz! Go back to studying, dude! 😭' : '<span class="quizPass">passed</span> the quiz! Congratulations!!! 😊'}</p>
                    `;

                    overallQuizDiv.lastElementChild.classList.toggle('displayOnOff');
                }
            });
        });
    });
};