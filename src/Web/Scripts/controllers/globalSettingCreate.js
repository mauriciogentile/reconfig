"use strinct;";

angular.module('controllers').controller('globalSettingCreate', function ($scope, apiService, notifications, browser) {
    $scope.sectionName = "";
    $scope.key = "";
    $scope.environment = "Development";
    $scope.value = "";
    $scope.save = function () {
        apiService.globalSetting.create({
                sectionName: $scope.sectionName,
                key: $scope.key,
                value: $scope.value,
                environment: $scope.environment
            })
            .success(function () {
                browser.setNewLocation("#/globalsetting");
            })
            .error(function (err) {
                notifications.alert(err.Message || err.message);
            });
    };
});