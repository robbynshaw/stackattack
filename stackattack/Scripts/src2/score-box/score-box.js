// Imports
const $ = require('jquery');
const simpleevent = require('../core/simple-event');
const scoreBoxTemplate = require('./score-box.handlebars');

function ScoreBox(opts) {
    // Store options
    this.baseUrl = opts.baseUrl;
    this.originalScore = opts.originalScore;
    this.$container = opts.$container;

    // Properties
    this.data = 0;

    // Events
    this.onScoreAdded = new simpleevent(this);

    function changeScore(obj, newScore) {
        var html = scoreBoxTemplate({ score: newScore });

        // Re-reveal
        obj.$container.hide();
        obj.$container.html(html);
        obj.$container.fadeIn(500);
    }

    this.onScoreAdded.subscribe(function (obj, data) {
        obj.data += data;
        changeScore(obj, obj.data);
    });

    // Load original view
    this.data = this.originalScore;
    changeScore(this, this.originalScore);
}

module.exports = ScoreBox;