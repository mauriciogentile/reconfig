"use strinct;";

angular.module('app.controllers')
    .controller('configurationCreate', function ($scope, $http, $routeParams, apiService, $window, $location) {
        $scope.appId = $routeParams.appId;
        $scope.name = "";
        $scope.environment = "Development";
        $scope.url = "";
        $scope.save = function () {
            if ($scope.url && !isValidUrl($scope.url)) {
                notifications.alert("Invalid Url");
                return;
            }
            apiService.configuration.create('api/configuration/create', {
                    name: $scope.name,
                    environment: $scope.environment,
                    url: $scope.url,
                    applicationId: $scope.appId
                })
                .success(function () {
                    $location.path("#/configuration/byApp/" + $scope.appId);
                })
                .error(function (err) {
                    notifications.alert(err.Message || err.message);
                });
        };
});