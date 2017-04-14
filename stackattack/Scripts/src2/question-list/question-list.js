// Imports
const $ = require('jquery');
const simpleevent = require('../core/simple-event');
const listTemplate = require('./question-list.handlebars');

function QuestionList(opts) {
    // Store options
    this.baseUrl = opts.baseUrl;
    this.userId = opts.userId;
    this.title = opts.title;
    this.onlyRecent = opts.onlyRecent;
    this.amountToShow = opts.amountToShow;
    this.amountToFetch = opts.amountToFetch;
    this.$container = opts.$container;

    // Properties
    this.data = {};

    // Events
    this.onRefreshed = new simpleevent(this);
    this.onQuestionClick = new simpleevent(this);

    // Private functions
    function hookUpItemEvents(obj, $container) {
        $container.find('li').each(function () {
            var id = $(this).attr('question-id');
            $(this).on('click', function () {
                obj.onQuestionClick.notify(id);
            });
        });
    }

    this.onRefreshed.subscribe(function (obj, args) {
        obj.data = args;
        var html = listTemplate({
            questions: obj.data,
            title: obj.title
        });

        // Re-reveal
        obj.$container.hide();
        obj.$container.html(html);
        obj.$container.fadeIn(function () {
            hookUpItemEvents(obj, obj.$container);
        });
    });
}


QuestionList.prototype.getQuestions = function getQuestions(endPoint) {
    var $this = this;

    $.ajax({
        method: 'GET',
        url: this.baseUrl + 'api/Question/' + endPoint,
        data: {
            count: this.amountToFetch
        }
    }).done(function (data) {
        $this.onRefreshed.notify(data);
    })
}

QuestionList.prototype.refresh = function() {
    if (this.onlyRecent) {
        this.getQuestions('GetRecent');
    } else {
        this.getQuestions('GetRandom');
    }
    return this;
}

module.exports = QuestionList;