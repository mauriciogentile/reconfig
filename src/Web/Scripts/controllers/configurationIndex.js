"use strinct;";

angular.module('App.Controllers')
    .controller('ConfigurationIndexCtrl', function ($scope, $routeParams, $rootScope, $apiService, $notifications) {
        $scope.configurations = [];
        $scope.delete = function (id) {
            if (notifications.confirm('Are you sure to delete configuration?')) {
                apiService.configuration.delete(id).success(function () {
                    $scope.load();
                });
            }
        };
        $scope.clone = function (id) {
            apiService.configuration.clone(id).success(function () {
                $scope.load();
            });
        };
        $scope.selectApp = function (id) {
            var apps = $.grep($scope.applications, function (item) {
                return (item.Id == id);
            });
            if (apps.length) $rootScope.$broadcast('appSelected', apps[0]);
        };
        $scope.load = function () {
            apiService.configuration.getByApp($routeParams.appId).success(function (data) {
                $scope.appId = $routeParams.appId;
                $scope.configurations = data;
            });
        };
        $scope.load();
});