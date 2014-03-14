angular.module('app.services')
	.factory('$notifications', function () {
	    return {
	        alert: bootbox.alert,
	        confirm: function (message) {
	            return confirm(message);
	        }
	    };
	});