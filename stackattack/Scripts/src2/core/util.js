// Utilities
module.exports = function () {

    var U = {};

    var GUIDCounter = function () {
        var count = 234;
        var next = function () {
            count += 1;
            return count;
        }
        return next;
    };
    U.NextGUID = GUIDCounter();

    U.PadWithZeros = function (num) {
        var s = num + "";
        while (s.length < 2) {
            s = "0" + s;
        }
        return s;
    };

    U.GetDurationString = function (durationInMinutes) {
        var days = Math.floor((durationInMinutes / 60) / 24);
        var hoursActual = (durationInMinutes - (days * 60 * 24)) / 60
        var hours = Math.floor(hoursActual);
        var minutes = Math.floor(durationInMinutes - ((days * 60 * 24) + (hours * 60)));
        return s = days + "d " + hours + "h " + minutes + "m ";
    };

    // Data Overrides
    Date.prototype.toNiceString = function () {
        var dat = new Date(this.valueOf());
        var hourNum = dat.getHours();
        var amPm = hourNum >= 12 ? " pm" : " am";
        var hours = amPm === " am" ? hourNum : hourNum - 12;
        var hours = hours === 0 ? 12 : hours;
        var s;
        if (dat > (new Date("01/01/2098"))
            || dat.getTime() == (new Date("01/01/2001 00:00:00")).getTime()
            || dat.getTime() == (new Date("01/01/1901 00:00:00")).getTime()) {
            s = "n/a";
        } else {
            s = U.PadWithZeros(dat.getFullYear()) + "-"
                + U.PadWithZeros(dat.getMonth() + 1)
                + "-" + U.PadWithZeros(dat.getDate())
                + " " + U.PadWithZeros(hours)
                + ":" + U.PadWithZeros(dat.getMinutes())
                + amPm;
        }
        return s;
    };

    Date.prototype.toNiceTimeString = function () {
        var dat = new Date(this.valueOf());
        var hourNum = dat.getHours();
        var amPm = hourNum >= 12 ? " pm" : " am";
        var hours = amPm === " am" ? hourNum : hourNum - 12;
        var hours = hours === 0 ? 12 : hours;
        var s;
        if (dat > (new Date("01/01/2098"))
            || dat.getTime() == (new Date("01/01/2001 00:00:00")).getTime()
            || dat.getTime() == (new Date("01/01/1901 00:00:00")).getTime()) {
            s = "n/a";
        } else {
            s = U.PadWithZeros(hours)
                + ":" + U.PadWithZeros(dat.getMinutes())
                + ":" + U.PadWithZeros(dat.getSeconds())
                + amPm;
        }
        return s;
    };

    return U;
}();