const $ = require('jquery');
const ScoreBox = require('./score-box/score-box');
const QuestionList = require('./question-list/question-list');
const Question = require('./question-wrapper/question');

$(document).ready(function () {
    console.log('Welcome to Stack Attack!');
    var config = document.config;
    console.log(config);

    var $app = $('.app');

    var scoreBox = new ScoreBox({
        baseUrl: config.baseUrl,
        originalScore: config.originalScore,
        $container: $app.find('.score-box')
    });

    var question = new Question({
        baseUrl: config.baseUrl,
        userId: config.userId,
        $container: $app.find('.question-wrapper')
    });

    // Hook up score box
    question.onScoreChanged.subscribe(function (obj, data) {
        scoreBox.onScoreAdded.notify(data);
    });

    var recentQuestions = new QuestionList({
        baseUrl: config.baseUrl,
        userId: config.userId,
        title: 'Recently Guessed',
        onlyRecent: true,
        amountToFetch: 10,
        amountToShow: 10,
        $container: $app.find('.recent-questions')
    }).refresh();
    
    recentQuestions.onQuestionClick.subscribe(function (obj, id) {
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
    }).refresh();

    randomQuestions.onQuestionClick.subscribe(function (obj, id) {
            question.refresh(id);
        });

    question.onAnswerReceived.subscribe(function () {
        recentQuestions.refresh();
        randomQuestions.refresh();
    });

});