const $ = require('jquery');
const QuestionList = require('./question-list/question-list');
const Question = require('./question-wrapper/question');

$(document).ready(function () {
    console.log('Welcome to Stack Attack!');
    var config = document.config;
    console.log(config);

    var $app = $('.app');

    var question = new Question({
        baseUrl: config.baseUrl,
        userId: config.userId,
        $container: $app.find('.question-wrapper')
    });

    var recentQuestions = new QuestionList({
        baseUrl: config.baseUrl,
        userId: config.userId,
        title: 'Recently Guessed',
        onlyRecent: true,
        amountToFetch: 10,
        amountToShow: 10,
        $container: $app.find('.recent-questions')
    })
        .refresh()
        .onQuestionClick.subscribe(function (obj, id) {
            question.refresh(id);
        });

    var randomQuestions = new QuestionList({
        baseUrl: config.baseUrl,
        userId: config.userId,
        title: 'Random Questions',
        onlyRecent: false,
        amountToFetch: 10,
        amountToShow: 10,
        $container: $app.find('.random-questions')
    })
        .refresh()
        .onQuestionClick.subscribe(function (obj, id) {
            question.refresh(id);
        });

});