'use strict';

angular.module("controllers", []);
var app = angular.module('app', ['ngRoute', 'controllers']);

var routeProvider = function ($routeProvider) {
    $routeProvider.
      when('/application', { templateUrl: 'partialView/get/application', controller: 'applicationIndex' }).
      when('/application/create', { templateUrl: 'partialView/get/application-create', controller: 'applicationCreate' }).
      when('/application/edit/:id', { templateUrl: 'partialView/get/application-edit', controller: 'applicationEdit' }).
      when('/application/delete/:id', { templateUrl: 'partialView/get/application', controller: 'applicationIndex' }).
      when('/configuration/byApp/:appId', { templateUrl: 'partialView/get/configuration', controller: 'configurationIndex' }).
      when('/configuration/create/:appId', { templateUrl: 'partialView/get/configuration-create', controller: 'configurationCreate' }).
      when('/configuration/edit/:id/app/:appId', { templateUrl: 'partialView/get/configuration-edit', controller: 'configurationEdit' }).
      when('/configuration/preview/:id', { templateUrl: 'partialView/get/configuration', controller: 'configurationIndex' }).
      when('/globalsetting', { templateUrl: 'partialView/get/globalsetting', controller: 'globalSettingIndex' }).
      when('/globalsetting/create', { templateUrl: 'partialView/get/globalsetting-create', controller: 'globalSettingCreate' }).
      when('/globalsetting/edit/:id', { templateUrl: 'partialView/get/globalsetting-edit', controller: 'globalSettingEdit' }).
      when('/globalsetting/delete/:id', { templateUrl: 'partialView/get/globalsetting', controller: 'globalSettingIndex' }).
      otherwise({ redirectTo: '/application' });
};

app.config(['$routeProvider', routeProvider]);