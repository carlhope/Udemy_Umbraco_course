angular.module("umbraco").controller("bleImageController", function ($scope, mediaResource) {

    //your property is called image so the following will contain the udi:
    var imageUdi = $scope.block.data.image;
    //the mediaResource has a getById method:
    mediaResource.getById(imageUdi).then(function (media) {
        console.log(media);
        //set a property on the 'scope' called imageUrl for the returned media object's mediaLink
        $scope.imageUrl = media.mediaLink;
    });
});
    
