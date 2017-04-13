//'use strict';

//describe('gameOverview', function () {

//    // Load the module that contains the `gameOverview` component before each test
//    beforeEach(module('gameOverview'));

//    // Test the controller
//    describe('GameOverviewController', function () {
//        var $httpBackend, ctrl;
//        var xyzPhoneData = {
//            name: 'phone xyz',
//            images: ['image/url1.png', 'image/url2.png']
//        };

//        beforeEach(inject(function ($componentController, _$httpBackend_, $routeParams) {
//            $httpBackend = _$httpBackend_;
//            $httpBackend.expectGET('phones/xyz.json').respond(xyzPhoneData);

//            $routeParams.phoneId = 'xyz';

//            ctrl = $componentController('gameOverview');
//        }));

//        it('should fetch the phone details', function () {
//            jasmine.addCustomEqualityTester(angular.equals);

//            expect(ctrl.phone).toEqual({});

//            $httpBackend.flush();
//            expect(ctrl.phone).toEqual(xyzPhoneData);
//        });

//    });

//});
