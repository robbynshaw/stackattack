'use strict';

// Register `questionDetail` component, along with its associated controller and template
angular.
    module('questionDetail').
    component('questionDetail', {
        templateUrl: 'question-detail/question-detail.html',
        controller: ['User',
            function QuestionDetailController(User) {
                //this.phones = Phone.query();
                //this.orderProp = 'age';
            }
        ]
    });