"use strict";

var app = angular.module("app", ["ng", "ngRoute", "app.controllers", "app.services"]);

angular.module("app.services", []);

angular.module("app.controllers", ["app.services"]);

var routeProvider = function ($routeProvider) {
    $routeProvider.
      when("/application", { templateUrl: "partialView/get/application", controller: "applicationIndex" }).
      when("/application/create", { templateUrl: "partialView/get/application-create", controller: "applicationController" }).
      when("/application/edit/:id", { templateUrl: "partialView/get/application-edit", controller: "applicationController" }).
      when("/application/delete/:id", { templateUrl: "partialView/get/application", controller: "cpplicationIndex" }).
      when("/configuration/byApp/:appId", { templateUrl: "partialView/get/configuration", controller: "configurationIndex" }).
      when("/configuration/create/:appId", { templateUrl: "partialView/get/configuration-create", controller: "configurationCreate" }).
      when("/configuration/edit/:id/app/:appId", { templateUrl: "partialView/get/configuration-edit", controller: "configurationEdit" }).
      when("/configuration/preview/:id", { templateUrl: "partialView/get/configuration", controller: "configurationIndex" }).
      when("/globalsetting", { templateUrl: "partialView/get/globalsetting", controller: "globalSettingIndex" }).
      when("/globalsetting/create", { templateUrl: "partialView/get/globalsetting-create", controller: "globalSettingCreate" }).
      when("/globalsetting/edit/:id", { templateUrl: "partialView/get/globalsetting-edit", controller: "globalSettingEdit" }).
      when("/globalsetting/delete/:id", { templateUrl: "partialView/get/globalsetting", controller: "globalSettingIndex" }).
      otherwise({ redirectTo: "/application" });
};

app.config(["$routeProvider", routeProvider]);