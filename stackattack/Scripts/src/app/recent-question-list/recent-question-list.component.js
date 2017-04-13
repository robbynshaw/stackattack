'use strict';

// Register `recentQuestionList` component, along with its associated controller and template
angular.
    module('recentQuestionList').
    component('recentQuestionList', {
        templateUrl: 'recent-question-list/recent-question-list.html',
        controller: ['User',
            function RecentQuestionListController(User) {
                //this.phones = Phone.query();
                //this.orderProp = 'age';
            }
        ]
    });