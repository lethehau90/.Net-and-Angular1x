(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService'];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.flatFolders = [];

        $scope.GetSeoTitle = GetSeoTitle;

        $scope.AddProductCategory = AddProductCategory;

        //GetSeoTitle
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name)
        }

        //AddProductCategory
        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được thêm mới');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('thêm mới không thành công');
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
                console.log(result);
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
    }
})(angular.module('haushop.productCategories'))