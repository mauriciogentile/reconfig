"use strinct;";

angular.module('app.controllers')
    .controller('cpplicationIndex', function ($scope, $routeParams, $rootScope, apiService, $notifications) {
        $scope.applications = [];
        $scope.delete = function (id) {
            if (notifications.confirm('Are you sure to delete app?')) {
                apiService.application.delete(id).success(function () {
                    $scope.load();
                });
            }
        };
        $scope.selectApp = function (id) {
            var apps = $.grep($scope.applications, function (item) {
                return (item.Id == id);
            });
            if (apps.length) $rootScope.$broadcast('appSelected', apps[0]);
        };
        $scope.load = function () {
            apiService.application.getAll().success(function (data) {
                $scope.applications = data;
            });
        };
        $scope.load();
});