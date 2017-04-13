'use strict';

// Register `questionList` component, along with its associated controller and template
angular.
    module('questionList').
    component('questionList', {
        templateUrl: 'question-list/question-list.html',
        controller: ['User',
            function QuestionListController(User) {
                //this.phones = Phone.query();
                //this.orderProp = 'age';
            }
        ]
    });