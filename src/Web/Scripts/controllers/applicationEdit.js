"use strinct;";

angular.module('App.Controllers')
    .controller('ApplicationEditCtrl', function ($scope, $routeParams, $apiService, $location, $notifications) {
        apiService.application.get($routeParams.id)
        .success(function (data) {
            $scope.id = data.Id;
            $scope.name = data.Name;
            $scope.owner = data.Owner;
            $scope.accessKey = data.AccessKey;
        });
        $scope.save = function () {
            apiService.application.update({
                id: $scope.id,
                name: $scope.name,
                owner: $scope.owner,
                accessKey: $scope.accessKey
            })
                .success(function () {
                    browser.setNewLocation("#/application");
                })
                .error(function (err) {
                    notifications.alert(err.Message || err.message);
                });
        };
});