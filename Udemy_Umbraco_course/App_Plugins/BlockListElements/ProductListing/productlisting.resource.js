
function productListingResource($http, umbRequestHelper) {
    return {
        getProduct: function (number) {
             return umbRequestHelper.resourcePromise(
                $http.get("/umbraco/backoffice/api/ProductListing/GetProducts?number=" + number),
                "Failed to get product"
            );
        }
    }
}



angular.module("umbraco.services").factory("productListingResource", productListingResource);