angular.module('app')
	.service('apiService', function ($http) {
	    return {
	        application: {
	            create: function (appData) {
	                return $http.post('api/application/create', appData);
	            },
	            update: function (appData) {
	                return $http.post('api/application/create', appData);
	            },
	            getAll: function () {
	                return $http.get('api/application/getAll');
	            },
	            get: function (id) {
	                return $http.get('api/application/get/' + id);
	            },
	            delete: function (id) {
	                return $http.post('api/application/delete/' + id);
	            }
	        },
	        configuration: {
	            get: function (id) {
	                return $http.get('api/configuration/get/' + id);
	            },
	            getByApp: function (id) {
	                return $http.get('api/configuration/byApp/' + id);
	            },
	            clone: function () {
	                return $http.post('api/configuration/clone/' + id);
	            },
	            create: function (data) {
	                return $http.post('api/configuration/create', data);
	            },
	            update: function (data) {
	                return $http.post('api/configuration/update', data);
	            },
	            delete: function (id) {
	                return $http.post('api/configuration/delete/' + id);
	            }
	        },
	        globalSetting: {
	            get: function (id) {
	                return $http.get('api/globalsetting/get' + id);
	            },
	            create: function (data) {
	                return $http.get('api/globalsetting/create', data);
	            },
	            update: function (data) {
	                return $http.get('api/globalsetting/update', data);
	            },
	            delete: function (id) {
	                return $http.get('api/globalsetting/delete/' + id);
	            },
	            getAll: function () {
	                return $http.get('api/globalsetting/getAll');
	            }
	        }
	    };
	});