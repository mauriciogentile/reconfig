'use strict';

describe('applicationCreate', function() {
    var scope, _utils, location, controller, _apiService, _window, q;

    beforeEach(module('app'));

    beforeEach(inject(function($controller, $location, $rootScope, $q, apiService, utils) {
        controller = $controller;
        scope = $rootScope.$new();
        location = $location;
        q = $q;
        _utils = utils;
        _apiService = apiService;
        _window = {};
    }));

    var createController = function() {
        return controller('applicationCreate', {
            $scope: scope,
            $location: location,
            $window: _window,
            apiService: _apiService,
            utils: _utils
        });
    };

    it('should have a method to check if the path is active', function() {
    });

    it('should have scope initialized', function() {
        var applicationCreate = createController();
        expect(scope.name).toBe("");
        expect(scope.owner).toBe("");
        expect(scope.guid).not.toBe("");
        expect(scope.save).not.toBeUndefined();
    });

    it("should call save when save", function(done) {
        var applicationCreate = createController();

        debugger;

        var defer = q.defer();

        sinon.stub(_apiService.application, "create").returns(defer.promise);
        _window.alert = sinon.spy();
        sinon.spy(location, "path");

        scope.save({});
        
        defer.resolve({});

        scope.$apply();

        sinon.assert.calledOnce(_apiService.application.create);
        sinon.assert.calledOnce(location.path);
        expect(_window.alert.callCount).toBe(0);
    });

    it("should call save and show alert when error", function() {

        var applicationCreate = createController();

        var defer = q.defer();

        sinon.stub(_apiService.application, "create").returns(defer.promise);
        _window.alert = sinon.spy();
        sinon.spy(location, "path");

        scope.save({});

        defer.reject(new Error("Error"));

        scope.$apply();

        sinon.assert.calledOnce(_apiService.application.create);
        sinon.assert.calledOnce(_window.alert);
        expect(location.path.callCount).toBe(0);
    });
});