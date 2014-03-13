"use strinct;";

angular.module('App.Controllers')
    .controller('GlobalSettingCreateCtrl', function ($scope, $apiService, $notifications, $location) {
        $scope.sectionName = "";
        $scope.key = "";
        $scope.environment = "Development";
        $scope.value = "";
        $scope.save = function () {
            $apiService.globalSetting.create({
                    sectionName: $scope.sectionName,
                    key: $scope.key,
                    value: $scope.value,
                    environment: $scope.environment
                })
                .success(function () {
                    $location.path("#/globalsetting");
                })
                .error(function (err) {
                    $notifications.alert(err.Message || err.message);
                });
        };
    });