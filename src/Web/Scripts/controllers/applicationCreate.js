"use strinct;";
angular.module('App.Controllers')
    .controller('ApplicationCreateCtrl', function ($scope, $location, $apiService, $window) {
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
                $location.path("#/application");
            })
            .error(function (err) {
                $window.alert(err.Message || err.message);
            });
        };
});