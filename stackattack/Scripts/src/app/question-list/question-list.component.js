'use strict';

// Register `phoneDetail` component, along with its associated controller and template
angular.
  module('questionList').
  component('questionList', {
    templateUrl: 'question-list/question-list.html',
    controller: ['$routeParams', 'Question',
      function QuestionListController($routeParams, Phone) {
        var self = this;
        self.phone = Phone.get({phoneId: $routeParams.phoneId}, function(phone) {
          self.setImage(phone.images[0]);
        });

        self.setImage = function setImage(imageUrl) {
          self.mainImageUrl = imageUrl;
        };
      }
    ]
  });
