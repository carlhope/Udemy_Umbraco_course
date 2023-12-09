﻿(function () {
    "use strict";

    function contactrequestsController($scope, contactRequestsService) {
        var vm = this;
        vm.contactRequestsNumber = 0;
        vm.contactRequests = [];
        $scope.model.badge = { count: 0 };

        getTotalNumber();
        getContentRequests();

        function getTotalNumber() {
            contactRequestsService.GetTotalNumber().then(function (number) {
                vm.contactRequestsNumber = number;
                $scope.model.badge = { count: number };
            })
        }

        function getContentRequests() {
            contactRequestsService.GetAll().then(function (data) {
                vm.contactRequests = data;
            })
        }



    }

    angular.module("umbraco").controller("contactrequestsController", contactrequestsController);

})();