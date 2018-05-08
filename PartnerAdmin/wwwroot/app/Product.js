(function () {

    var productApp = angular.module("productApp", [
        "ngAnimate",
        "angularUtils.directives.dirPagination",
        "blockUI"
    ]);

    //service
    productApp.service('productSearch', function ($http) {
        this.getProductTargetList = function (partnerId, goodId, currentPage, itemsPerPage) {
            var formdata = {
                partnerId: partnerId,
                goodId: goodId,
                currentPageNo: currentPage,
                limitPageNo: itemsPerPage
            }

            return $http({
                method: 'POST',
                url: '/Product/SearchProduct',
                data: JSON.stringify(formdata),
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(
                function successCallback(response) {
                    return response
                },
                function errorCallback(response) {
                    alert("error");
                }
            );
        };
    });

    //controller
    productApp.controller('productCtrl', ['blockUI', '$scope', 'productSearch', function (blockUI, $scope, productSearch) {
        $scope.partnerId = "AUCTION2";
        $scope.goodId = "";
        $scope.currentPageNo = 1;
        $scope.itemsPerPage = 20;
        $scope.totalCount = 0;
        $scope.productTargetList = [];

        $scope.searchProductTarget = function (currentPageNo) {
            // Assign new page number
            $scope.currentPageNo = currentPageNo;
            $scope.showLoading = false;

            var productlist = productSearch.getProductTargetList($scope.partnerId, $scope.goodId, $scope.currentPageNo, $scope.itemsPerPage);
            productlist.then(function (resp) {
                if (resp) {
                    var jsonResult = JSON.parse(resp.data);
                    if (jsonResult.Success) {
                        if (jsonResult.List.length > 0) {
                            $scope.productTargetList = jsonResult.List;
                            $scope.totalCount = jsonResult.List[0].TotalCnt;
                        }
                        else {
                            $scope.LoadingText = "데이터가 없습니다.";
                            $scope.showLoading = true;
                        }
                    }
                }
            }, function () {
                $scope.LoadingText = "Error occur, please try again.";
                $scope.showLoading = true;
                }).finally(function () {
            });
        };

        $scope.searchProductTarget(1);
    }]);
})();