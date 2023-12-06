function ContactRequestsResource($http, umbRequestHelper) {

    return {
        getTotalNumber: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/backoffice/api/ContactRequestsApi/GetTotalNumber"), "failed to retrieve total number"
            );
        },
        getAll: function () {
            return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/backoffice/api/ContactRequestsApi/GetAll"), "failed to retrieve the contact requests"
            );
        },
    }
}

angular.module("umbraco.services").factory("ContactRequestResource", ContactRequestsResource);