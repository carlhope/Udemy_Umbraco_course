function productListingService(productListingResource) {
    return {
        getProduct: function (number) {
            return productListingResource.getProduct(number).then(function (data) {
                return data;
            }, function () { });
            
        }
    }
}



angular.module("umbraco.services").factory("productListingService", productListingService);