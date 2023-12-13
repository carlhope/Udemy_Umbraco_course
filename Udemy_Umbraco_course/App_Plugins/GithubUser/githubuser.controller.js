(function () {
    "use strict";

    function githubuserController($scope) {
        if ($scope.model.value === null || $scope.model.value === "") {
            $scope.model.value = {
                githubUsername:"",
                githubPrefferedLanguage:""
            }
        }
    }

    angular.module("umbraco").controller("githubUserController", githubuserController);
})();