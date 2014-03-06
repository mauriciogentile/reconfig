"use strinct;";
angular.module('controllers')
    .controller('menu', function ($scope, $rootScope, browser) {
        $scope.selectedApp = {};
        $scope.$on("appSelected", function (event, app) {
            $scope.selectedApp = app;
            $rootScope.selectedApp = app;
            browser.setNewLocation("#/configuration/byApp/" + app.Id);
        });
    });