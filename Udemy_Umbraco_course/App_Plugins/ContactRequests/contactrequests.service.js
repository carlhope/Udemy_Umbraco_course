function ContactRequestsService(ContactRequestsResource) {
    return {

        getTotalNumber: function () {
            return ContactRequestsResource.getTotalNumber().then(function (data) {
                return data;
            }, function () { });
        },
        getAll: function () {
            return ContactRequestsResource.getAll().then(function (data) {
                return data;
            }, function () { });
        }
    }
}
    angular.module("umbraco.services").factory("contactRequestsService", ContactRequestsService);