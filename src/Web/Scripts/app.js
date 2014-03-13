"use strict";

var app = angular.module("App", ["ng", "ngRoute", "App.Controllers", "App.Services"]);

angular.module("App.Controllers", []);
angular.module("App.Services", []);

var routeProvider = function ($routeProvider) {
    $routeProvider.
      when("/application", { templateUrl: "partialView/get/application", controller: "ApplicationIndexCtrl" }).
      when("/application/create", { templateUrl: "partialView/get/application-create", controller: "ApplicationCreateCtrl" }).
      when("/application/edit/:id", { templateUrl: "partialView/get/application-edit", controller: "ApplicationEditCtrl" }).
      when("/application/delete/:id", { templateUrl: "partialView/get/application", controller: "ApplicationIndexCtrl" }).
      when("/configuration/byApp/:appId", { templateUrl: "partialView/get/configuration", controller: "ConfigurationIndexCtrl" }).
      when("/configuration/create/:appId", { templateUrl: "partialView/get/configuration-create", controller: "ConfigurationCreateCtrl" }).
      when("/configuration/edit/:id/app/:appId", { templateUrl: "partialView/get/configuration-edit", controller: "ConfigurationEditCtrl" }).
      when("/configuration/preview/:id", { templateUrl: "partialView/get/configuration", controller: "ConfigurationIndexCtrl" }).
      when("/globalsetting", { templateUrl: "partialView/get/globalsetting", controller: "GlobalSettingIndexCtrl" }).
      when("/globalsetting/create", { templateUrl: "partialView/get/globalsetting-create", controller: "GlobalSettingCreateCtrl" }).
      when("/globalsetting/edit/:id", { templateUrl: "partialView/get/globalsetting-edit", controller: "GlobalSettingEditCtrl" }).
      when("/globalsetting/delete/:id", { templateUrl: "partialView/get/globalsetting", controller: "GlobalSettingIndexCtrl" }).
      otherwise({ redirectTo: "/application" });
};

app.config(["$routeProvider", routeProvider]);