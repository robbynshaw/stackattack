// Imports
const $ = require('jquery');
const simpleevent = require('../core/simple-event');
const questionWrapperTemplate = require('./question-wrapper.handlebars');
const questionDetailTemplate = require('./question-detail.handlebars');
const answerDetailTemplate = require('./answer-detail.handlebars');
const successMessageTemplate = require('./success-message.handlebars');

function Question(opts) {
    // Store options
    this.baseUrl = opts.baseUrl;
    this.userId = opts.userId;
    this.$container = opts.$container;

    // Properties
    this.data = {};

    // Events
    this.onRefreshed = new simpleevent(this);
    this.onAnswerClick = new simpleevent(this);
    this.onAnswerReceived = new simpleevent(this);

    this.onAnswerReceived.subscribe(function (obj, args) {
        var wrapperHtml = questionWrapperTemplate();
        console.log(args);
        var msg = successMessageTemplate(args);
        obj.$container.hide();
        obj.$container.html(wrapperHtml);
        obj.$container.find('.question-detail').html(msg);

        obj.$container.fadeIn(500);
    });

    this.onAnswerClick.subscribe(function (obj, args) {
        console.log(args);
        checkScore(obj, args.answer.id);
    });

    function checkScore($this, id) {
        $.ajax({
            method: 'GET',
            url: $this.baseUrl + 'api/User/CheckScore',
            data: {
                userID: $this.userId,
                answerID: id
            }
        }).done(function (data) {
            $this.onAnswerReceived.notify(data);
        });
    }

    function addAnswers(obj, args, $container) {
        if (args.answers) {
            for (var i = 0; i < args.answers.length; i++) {
                var answer = args.answers[i];
                // Add number for display
                answer.number = i + 1;
                
                // Add event
                answer.onClick = new simpleevent(answer);
                answer.onClick.subscribe(function (o, e) {
                    obj.onAnswerClick.notify({
                        answer: o,
                        element: e
                    });
                })

                // Fill it up
                var html = answerDetailTemplate(answer);
                var $el = $(document.createElement('div'));

                $container.append($el);
                $el.on('click', function () {
                    answer.onClick.notify($el);
                })

                // Make it all slide down fancy
                $el.hide();
                $el.html(html);
                setTimeout(function (e) {
                    e.slideDown(500);
                }, i * 250, $el);
            }
        }
    }

    this.onRefreshed.subscribe(function (obj, args) {
        obj.data = args;
        var wrapperHtml = questionWrapperTemplate();
        var detalHtml = questionDetailTemplate(args);

        // Re-reveal
        obj.$container.hide();
        obj.$container.html(wrapperHtml);
        obj.$container.find('.question-detail').html(detalHtml);

        obj.$container.fadeIn(function () {
            addAnswers(obj, args, obj.$container.find('.answer-detail'));
        });
    });
}

Question.prototype.refresh = function (id) {
    var $this = this;

    $.ajax({
        method: 'GET',
        url: this.baseUrl + 'api/Question/',
        data: {
            id: id
        }
    }).done(function (data) {
        $this.onRefreshed.notify(data);
        });
    return this;
}

module.exports = Question;