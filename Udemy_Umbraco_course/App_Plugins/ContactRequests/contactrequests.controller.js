(function () {
    "use strict";

    function contactrequestsController($scope, contactRequestsService) {
        var vm = this;
        vm.contactRequestsNumber = 0;
        vm.contactRequests = [];
        $scope.model.badge = { count: 0 };

        //getTotalNumber();
       // getContentRequests();

        function getTotalNumber() {
            contactRequestsService.getTotalNumber().then(function (number) {
                vm.contactRequestsNumber = number;
                $scope.model.badge = { count: number };
            })
        }

        function getContentRequests() {
            contactRequestsService.getAll().then(function (data) {
                vm.contactRequests = data;
            })
        }

      

    }

    angular.module("umbraco").controller("contactrequestsController", contactrequestsController);

})();