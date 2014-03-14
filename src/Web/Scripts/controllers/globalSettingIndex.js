"use strinct;";

angular.module('app.controllers')
    .controller('globalSettingIndex', function ($scope, $http, apiService, $notifications) {
        $scope.globalSettings = [];
        $scope.delete = function (id) {
            if (notifications.confirm('Are you sure to delete setting?')) {
                apiService.globalSetting.delete(id).success(function () {
                    $scope.load();
                });
            }
        };
        $scope.load = function () {
            apiService.globalSetting.getAll().success(function (data) {
                $scope.globalSettings = data;
            });
        };
        $scope.load();
    });