'use strict';

describe('app.controllers bootstrap test', function() {
    
    var hasController = function(name) {
        try {
            module(name);
            return true;
        }
        catch(err) {
            return false;
        }
    };

    it('should have module app.controllers', function() {
        expect(hasController("applicationCreate")).toBe(true);
    });

    it('should have module app.controllers', function() {
        expect(hasController("applicationIndex")).toBe(true);
    });
});

