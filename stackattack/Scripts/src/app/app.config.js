'use strict';

angular.
  module('stackattackApp').
  config(['$locationProvider' ,'$routeProvider',
    function config($locationProvider, $routeProvider) {
      $locationProvider.hashPrefix('!');

      $routeProvider.
        when('/questions', {
          templateUrl: 'game-overview/game-overview.html',
        }).
        when('/questions/:questionId', {
          templateUrl: 'question-detail/game-detail.html',
        }).
        otherwise('/questions');
    }
  ]);
