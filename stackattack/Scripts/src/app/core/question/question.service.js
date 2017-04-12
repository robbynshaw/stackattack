'use strict';

angular.
  module('core.question').
  factory('Question', ['$resource',
    function($resource) {
      return $resource('phones/:phoneId.json', {}, {
        query: {
          method: 'GET',
          params: {phoneId: 'phones'},
          isArray: true
        }
      });
    }
  ]);
