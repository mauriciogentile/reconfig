angular.module('app')
	.service('notifications', function () {
	    return {
	        alert: bootbox.alert,
	        confirm: function (message) {
	            return confirm(message);
	        }
	    };
	});