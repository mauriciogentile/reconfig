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
        _apiService = { application: { } };
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

    it("should call save when save", function() {
        var applicationCreate = createController();

        var deferred = q.defer();
        var promise = deferred.promise;

        _apiService.application.create = sinon.stub().returns(promise);
        var path = sinon.spy(location, "path");

        scope.save({});

        deferred.resolve();

        sinon.assert.calledOnce(_apiService.application.create);
        sinon.assert.calledOnce(path);
    });

    it("should call save and show alert when error", function() {
        var applicationCreate = createController();

        var deferred = q.defer();
        var promise = deferred.promise;

        _apiService.application.create = sinon.stub().returns(promise);
        _window.alert = sinon.spy();

        scope.save({});

        deferred.reject();

        sinon.assert.calledOnce(_apiService.application.create);
        sinon.assert.calledOnce(_window.alert);
    });
});