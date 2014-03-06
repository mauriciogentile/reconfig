"use strinct;";
angular.module('controllers')
    .controller('applicationCreate', function ($scope, apiService, notifications, browser) {
        $scope.name = "";
        $scope.owner = "";
        $scope.accessKey = Guid();
        $scope.save = function () {
            apiService.application.create({
                name: $scope.name,
                owner: $scope.owner,
                accessKey: $scope.accessKey
            })
            .success(function (data) {
                browser.setNewLocation("#/application");
            })
            .error(function (err) {
                notifications.alert(err.Message || err.message);
            });
        };
});