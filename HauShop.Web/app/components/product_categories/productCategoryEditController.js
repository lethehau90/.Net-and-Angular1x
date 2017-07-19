(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];

    function productCategoryEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.productCategory = []

        $scope.GetSeoTitle = GetSeoTitle;

        $scope.parentCategories = [];

        $scope.flatFolders = [];

        //GetSeoTitle
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name)
        }

        $scope.UpdateProductCategory = UpdateProductCategory;

        //loadProductCategoryDetail
        function loadProductCategoryDetail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (resurt) {
                $scope.productCategory = resurt.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        //UpdateProductCategory
        function UpdateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('cập nhật không thành công');
            });
        }

        //loadParentCategories
        //function loadParentCategories() {
        //    apiService.get('/api/productcategory/getallparents', null, function (result) {
        //        $scope.parentCategories = result.data;
        //    }, function (error) {
        //        console.log('cannot get list parent.');
        //    })
        //}

        //loadParentCategories
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                //console.log(result);
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

        //load
        loadParentCategories();
        loadProductCategoryDetail();
    }
})(angular.module('haushop.productCategories'))