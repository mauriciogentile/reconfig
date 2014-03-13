"use strinct;";
angular.module('App.Controllers')
    .controller('menu', function ($scope, $rootScope, $location) {
        $scope.selectedApp = {};
        $scope.$on("appSelected", function (event, app) {
            $scope.selectedApp = app;
            $rootScope.selectedApp = app;
            $location.path("#/configuration/byApp/" + app.Id);
        });
    });