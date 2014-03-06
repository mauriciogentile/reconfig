"use strinct;";
angular.module('controllers')
    .controller('configurationEdit', function ($scope, $routeParams, $rootScope, $filter, apiService, notifications, browser) {
        $scope.appId = $routeParams.appId;
        $scope.name = "";
        $scope.environment = "";
        $scope.url = "";
        $scope.sections = [];
        $scope.newSettings = [];
        $scope.editSectionVisible = false;
        $scope.editSectionName = "";
        $scope.editSectionIndex = "";
        $scope.addNewSection = function () {
            var sections = $scope.sections || [];
            sections.push({ Name: "New Section " + sections.length, Settings: [] });
            $scope.sections = sections;
        };
        $scope.editSection = function (index) {
            var section = $scope.sections[index];
            $scope.editSectionIndex = index;
            $scope.editSectionVisible = true;
            $scope.editSectionName = section.Name;
            section.Settings = section.Settings || [];
            if (!section.Settings.length) {
                section.Settings.push({ Key: "Key0", Value: "Value0" });
            }
            //clone
            angular.copy(section.Settings, $scope.newSettings);
        };
        $scope.enlargeEdit = function (obj, event) {
            $(event.target).attr("rows", 3);
        };
        $scope.reduceEdit = function (obj, event) {
            $(event.target).attr("rows", 1);
        };
        $scope.removeSection = function (index) {
            if (notifications.confirm('Are you sure to delete section?')) $scope.sections.splice(index, 1);
        };
        $scope.addSectionValue = function () {
            var index = $scope.newSettings.length;
            $scope.newSettings.push({ Key: "Key" + index, Value: "Value" + index });
        };
        $scope.removeSectionValue = function (index) {
            if (notifications.confirm('Are you sure to delete value?')) $scope.newSettings.splice(index, 1);
        };
        $scope.cancelSectionEdition = function () {
            $scope.editSectionVisible = false;
        };
        $scope.okSectionEdition = function () {
            angular.copy($scope.newSettings, $scope.sections[$scope.editSectionIndex].Settings);
            $scope.editSectionVisible = false;
        };
        $scope.byApp = function () {
            return function (item) {
                return item.Id != $routeParams.id;
            };
        };
        $scope.load = function () {
            apiService.configuration.get($routeParams.id).success(function (data) {
                $scope.id = data.Id;
                $scope.environment = data.Environment;
                $scope.name = data.Name;
                $scope.createdBy = data.Version.CreatedBy;
                $scope.lastUpdatedOn = data.Version.LastUpdatedOn;
                $scope.lastUpdatedBy = data.Version.LastUpdatedBy;
                $scope.url = data.Url;
                $scope.parentId = data.ParentId;
                $scope.sections = data.Sections;
                if (!$scope.sections.length) {
                    $scope.sections.push({ Name: "appSettings" });
                    $scope.sections.push({ Name: "connectionStrings" });
                }
            }).then(apiService.configuration.getByApp($routeParams.appId).success(function (data) {
                $scope.parentConfigurations = data;
                var filtered = $filter('filter')(data, { Id: $scope.parentId }, true);
                if (filtered.length) $scope.parentConfiguration = filtered[0];
            }));
        };
        $scope.save = function () {
            var configId = $scope.parentConfiguration || {};
            if (!configId.Id) configId.Id = null;

            if ($scope.url && !isValidUrl($scope.url)) {
                notifications.alert("Invalid Url");
                return;
            }
            apiService.configuration.update({
                    id: $scope.id,
                    name: $scope.name,
                    environment: $scope.environment,
                    url: $scope.url,
                    applicationId: $scope.appId,
                    parentId: configId.Id,
                    sections: $scope.sections
                })
                .success(function () {
                    browser.setNewLocation("#/configuration/byApp/" + $scope.appId);
                })
                .error(function (err) {
                    notifications.alert(err.Message || err.message);
                });
        };
        $scope.load();
});