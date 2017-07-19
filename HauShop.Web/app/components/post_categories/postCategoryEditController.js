(function (app) {
    app.controller('postCategoryEditController', postCategoryEditController);

    postCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService', '$stateParams'];

    function postCategoryEditController($scope, apiService, notificationService, $state, commonService, $stateParams) {
        $scope.postCategory = []

        $scope.GetSeoTitle = GetSeoTitle;

        $scope.parentCategories = [];

        $scope.flatFolders = [];

        //GetSeoTitle
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSeoTitle($scope.postCategory.Name)
        }

        $scope.UpdatePostCategory = UpdatePostCategory;

        //loadPostCategoryDetail
        function loadPostCategoryDetail() {
            apiService.get('/api/postcategory/getbyid/' + $stateParams.id, null, function (resurt) {
                $scope.postCategory = resurt.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        //UpdatePostCategory
        function UpdatePostCategory() {
            apiService.put('/api/postcategory/update', $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + ' đã được cập nhật thành công');
                $state.go('postcategories');
            }, function (error) {
                notificationService.displayError('cập nhật không thành công');
            });
        }

        //loadParentCategories
        //function loadParentCategories() {
        //    apiService.get('/api/postcategory/getallparents', null, function (result) {
        //        $scope.parentCategories = result.data;
        //    }, function (error) {
        //        console.log('cannot get list parent.');
        //    })
        //}

        //loadParentCategories
        function loadParentCategories() {
            apiService.get('/api/postcategory/getall', null, function (result) {
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
        loadPostCategoryDetail();
    }
})(angular.module('haushop.postcategories'))