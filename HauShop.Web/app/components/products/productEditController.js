(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];

    function productEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.product = []

        $scope.productCategories = [];

        $scope.flatFolders = [];

        $scope.moreImages = []

        $scope.GetSeoTitle = GetSeoTitle;

        $scope.UpdateProduct = UpdateProduct;

        $scope.Promotionpercent = []

        $scope.deleteFile = deleteFile


        //GetSeoTitle
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name)
        }

        function productLoadDetail()
        {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                console.log('cannot get list parent.');
            })
        }

        function Promotionpercent() {
            var price = $scope.product.Price
            var promotionPrice = $scope.product.promotionPrice
            $scope.Promotionpercent = (price - promotionPrice) / price * 100
        }

        //AddProduct
        function UpdateProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật');
                $state.go('products');
            }, function (error) {
                notificationService.displayError('cập nhật không thành công');
            });
        }

        //loadParentCategories
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.parentCategories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                })
            }, function (error) {
                console.log('cannot get list parent.');
            })
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        //ckeditor
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        //finder
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }
        
        //moreimages
        $scope.chooseMoreImage = function () {
            if ($scope.moreImages === null) $scope.moreImages = [];
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }

        function deleteFile(img) {
            var listImage = $scope.moreImages
            for (var i = 0; i < listImage.length ; i++) {
                if(listImage[i] === img )
                {
                    listImage.splice(i, 1);
                    break;
                }
            }
            $scope.moreImages = listImage;
        }

        //load
        loadParentCategories();
        productLoadDetail();
        Promotionpercent()
    }
})(angular.module('haushop.products'))