"use strinct;";

angular.module('controllers').controller('globalSettingEdit', function ($scope, $http, $routeParams, apiService, notifications, browser) {
    $http.get('api/globalsetting/get/' + $routeParams.id).success(function (data) {
        $scope.id = data.Id;
        $scope.sectionName = data.SectionName;
        $scope.key = data.Key;
        $scope.environment = data.Environment;
        $scope.value = data.Value;
    });
    $scope.save = function () {
        apiService.globalSetting.update({
                id: $scope.id,
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