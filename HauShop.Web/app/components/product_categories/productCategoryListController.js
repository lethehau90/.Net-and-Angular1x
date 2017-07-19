/// <reference path="\Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'commonService'];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter, commonService) {

        $scope.deleteProductCategory = deleteProductCategory;
        $scope.data = []
        $scope.parentCategories = []

        $scope.reLoad = reLoad;

        function reLoad() {
            loadParentCategories()
        }

        //deleteProductCategory
        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config, function (result) {
                    if (result.data == 'children') {
                        notificationService.displayError('Danh mục có danh sách con')
                    }
                    else if (result.data == 'product') {
                        notificationService.displayError('Sản phẩm có bài đăng liên quan danh mục này')
                    }
                    else {
                        notificationService.displaySuccess('Xóa thành công')
                        reLoad()
                    }
                }, function () {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        };

        $scope.toggle = function (scope) {
            scope.toggle();
        };

        $scope.moveLastToTheBeginning = function () {
            var a = $scope.parentCategories.pop();
            $scope.parentCategories.splice(0, 0, a);
        };

        $scope.collapseAll = function () {
            $scope.$broadcast('angular-ui-tree:collapse-all');
        };

        $scope.expandAll = function () {
            $scope.$broadcast('angular-ui-tree:expand-all');
        };

       

        //loadParentCategories
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");
               
            }, function (error) {
                console.log('cannot get list parent.');
            })
        }

        //load
        loadParentCategories();

    }
})(angular.module('haushop.productCategories'))