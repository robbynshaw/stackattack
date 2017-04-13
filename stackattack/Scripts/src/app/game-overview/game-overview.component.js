'use strict';

// Register `gameOverview` component, along with its associated controller and template
angular.
    module('gameOverview').
    component('gameOverview', {
        templateUrl: 'game-overview/game-overview.html',
        controller: ['User',
            function GameOverviewController(User) {
                //this.phones = Phone.query();
                //this.orderProp = 'age';
            }
        ]
    });