generateBtn = document.querySelector('#generateBtn');
quote = document.querySelector('#quote');
character = document.querySelector('#character');

const quoteGenerator = () => {
    let quotesList = [
        {quote: 'Winter is Coming', character: 'Eddard Stark'},
        {quote: 'When you play a game of thrones you win or you die', character: 'Cersei Lannister'},
        {quote: 'If you think this has a happy ending, you haven\'t been paying attention', character: 'Ramsay Bolton'},
        {quote: 'Bran thought about it. \'Can a man still be brave if he\'s afraid?\' \'That is the only time a man can be brave,\', his father told him', character: 'Conversation between Bran and his father, Lord Eddard Stark'},
    ];
    
    let randQuote = Math.floor(Math.random() * quotesList.length);

    quote.innerHTML = quotesList[randQuote].quote;
    character.innerHTML = `- ${quotesList[randQuote].character}`;

    quotesList.splice(randQuote, 1);
};

generateBtn.addEventListener('click', () => {
    quoteGenerator();
});