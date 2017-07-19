/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerListController', footerListController);

    footerListController.$inject = ['apiService', '$scope', '$state', '$stateParams', 'notificationService', '$filter', '$ngBootbox']

    function footerListController(apiService, $scope, $state, $stateParams, notificationService, $filter, $ngBootbox) {

        $scope.footers = []
        $scope.selectAll = selectAll;
        $scope.deleteFooter = deleteFooter;
        $scope.deleteMultiple = deleteMultiple;

        $scope.getFooters = getFooters

        function getFooters() {
            apiService.get('/api/footer/getall', null, function (result) {
                if (result.data.length > 0) {
                    notificationService.displayWarning('Không có bản ghi nào')
                }
                else {
                    $scope.footers = result.data;
                }
            }, function (error) {
                console.log('Lỗi')
            })
        }

        //selectAll
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.footers, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.footers, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.isAll = false;

        //listening use watch
        $scope.$watch("footers", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function getFooter() {
            apiService.get('/api/footer/getall', null, function (result) {
                if (result.data.length <= 0) {
                    notificationService.displayWarning('Không có bản ghi nào')
                } else {
                    $scope.footers = result.data;
                }
            }, function (error) {
                console.log('lỗi');
            });
        }

        //deleteFooter
        function deleteFooter(id) {
            $ngBootbox.confirm('Bạn có chắc chắn muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/footer/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công')
                    getFooter()
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
                apiService.del('/api/footer/deletemulti', config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi')
                    getFooter()
                }, function (error) {
                    notificationService.displayError('Xóa không thành công')
                });
            });
        }

        getFooter()
    }
})(angular.module('haushop.footers'))