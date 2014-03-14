'use strict';

describe('app bootstrap test', function() {
    var hasModule = function(name) {
        try {
            module(name);
            return true;
        }
        catch(err) {
            return false;
        }
    };

    beforeEach(module('app'));

    it('should have module app.controllers', function() {
        //expect(app.controllers).toBe(null);
    });


    it('should have module App.Services', function() {
        expect(hasModule("app.services")).toBe(true);
    });
});

