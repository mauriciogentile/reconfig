'use strict';

describe('ApplicationCreateCtrl', function() {
    var scope, rootScope, notifications, windowMock, controller;

    beforeEach(module('app'));

    beforeEach(inject(function($controller, $rootScope, $window) {
        controller = $controller;
        rootScope = $rootScope;
        scope = $rootScope.$new();
        windowMock = {
            alert: function () {},
            confirm: function () {}
        };
    }));

    function createController() {
        return controller('applicationCreate', {
            $scope: scope,
            $window: windowMock,
            $notifications: notifications
        });
    }

    it('should have a method to check if the path is active', function() {
        //console.log(app.name);
        //expect(app.VideosCtrl).equalTo(null);
    });

    it("should have App.Controllers as a dependency", function() {
        //expect(hasModule('App.Controllers')).to.equal(true);
    });

    it('should have a method to check if the path is active', function() {
        var applicationCreate = createController();
        console.log(applicationCreate);
        //expect(scope.name).equal("");
        expect(scope.name).toBe("");
        expect(scope.name).toBe("");
        //console.log(app.name);
        //console.log(angular.module("app.services"));
        //console.log(angular.module("app.controllers"));
        //console.log(app);
        //console.log(expect);
    });
});