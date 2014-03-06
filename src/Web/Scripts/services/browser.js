angular.module('app')
	.service('browser', function () {
	    return {
	        setNewLocation: function (newLocation) {
	            window.location = newLocation;
	        }
	    };
	});