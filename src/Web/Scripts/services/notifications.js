angular.module('App.Services')
	.factory('$notifications', function () {
	    return {
	        alert: bootbox.alert,
	        confirm: function (message) {
	            return confirm(message);
	        }
	    };
	});