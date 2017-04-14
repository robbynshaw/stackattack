// Setup simple events
module.exports = (function () {
    // Imports
    const Util = require('./util.js');

    var e = function Event(obj) {
        this._sender = obj;
        this.subscribers = {};
        this.tempSubscribers = [];
    };
    e.prototype = {
        subscribe: function (name, subscriber) {
            if (!subscriber) {
                subscriber = name;
                name = Util.NextGUID();
            }

            this.subscribers[name] = subscriber;
            return name;
        },
        subscribeOnce: function (subscriber) {
            this.tempSubscribers.push(subscriber);
        },
        unSubscribe: function (name) {
            delete this.subscribers[name];
        },
        notify: function (args, type, msg) {
            for (var property in this.subscribers) {
                if (this.subscribers.hasOwnProperty(property)) {
                    this.subscribers[property](this._sender, args, type, msg);
                }
            }

            // clone to avoid infinite loops
            var tsubs = [];
            for (var i = 0; i < this.tempSubscribers.length; i++) {
                tsubs.push(this.tempSubscribers[i]);
            }
            this.tempSubscribers = [];
            for (var i = 0; i < tsubs.length; i++) {
                tsubs[i](this._sender, args, type, msg);
            }
        }
    };
    return e;
})();