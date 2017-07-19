(function (app) {
    app.controller('postCategoryListController', postCategoryListController);

    postCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter', 'commonService'];

    function postCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter, commonService) {

        $scope.deletePostCategory = deletePostCategory;
        $scope.data = []
        $scope.parentCategories = []

        $scope.reLoad = reLoad;

        function reLoad() {
            loadParentCategories()
        }

        //deletePostCategory
        function deletePostCategory(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/postcategory/delete', config, function (result) {
                    if (result.data == 'children') {
                        notificationService.displayError('Danh mục có danh sách con')
                    }
                    else if (result.data == 'post') {
                        notificationService.displayError('Tin tức có bài đăng liên quan danh mục này')
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
            apiService.get('/api/postcategory/getall', null, function (result) {
                $scope.parentCategories = commonService.getTree(result.data, "ID", "ParentID");

            }, function (error) {
                console.log('cannot get list parent.');
            })
        }

        //load
        loadParentCategories();

    }
})(angular.module('haushop.postcategories'))