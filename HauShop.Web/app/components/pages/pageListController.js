/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('pageListController', pageListController);

    pageListController.$inject = ['$scope', 'apiService', 'commonService', 'notificationService', '$state', '$filter', '$ngBootbox']

    function pageListController($scope, apiService, commonService, notificationService, $state, $filter, $ngBootbox) {
        $scope.pages = []

        $scope.getPage = getPage();
        $scope.selectAll = selectAll;
        $scope.deletePage = deletePage;
        $scope.deleteMultiple = deleteMultiple;
        $scope.loading = true

        //selectAll
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.pages, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.pages, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.isAll = false;

        //listening use watch
        $scope.$watch("pages", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function getPage() {
            apiService.get('/api/page/getall', null, function (result) {
                if (result.data.length <= 0) 
                    notificationService.displayWarning('Không có bản ghi nào')
                $scope.pages = result.data;
                $scope.loading = false
            }, function (error) {
                console.log('lỗi');
            });
        }

        //deletePage
        function deletePage(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/page/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công')
                    getPage()
                }, function () {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        };
        //deleteMultiple
        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc chắc muốn xóa bản ghi đã chọn ?').then(function () {
                var listId = [];
                $.each($scope.selected, function (i, item) {
                    listId.push(item.ID);
                })
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.del('/api/page/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi')
                    getPage()
                }, function (error) {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        }

        getPage();
    }
})(angular.module('haushop.pages'))