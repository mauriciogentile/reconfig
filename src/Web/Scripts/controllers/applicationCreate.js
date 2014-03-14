"use strinct";

angular.module('app.controllers')
    .controller('applicationCreate', ["$scope", "$location", "$window", "apiService", "utils", function ($scope, $location, $window, apiService, utils) {
        $scope.name = "";
        $scope.owner = "";
        $scope.accessKey = utils.guid();
        $scope.save = function () {
            apiService.application.create({
                name: $scope.name,
                owner: $scope.owner,
                accessKey: $scope.accessKey
            })
            .then(function (data) {
                $location.path("#/application");
            })
            .catch(function (err) {
                $window.alert(err.Message || err.message);
            });
        };
}]);