/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('pageAddController', pageAddController);

    pageAddController.$inject = ['$scope', 'apiService', 'commonService', 'notificationService', '$state', '$filter']

    function pageAddController($scope, apiService, commonService, notificationService, $state, $filter) {
        $scope.page = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

        $scope.AddPage = AddPage

        function AddPage() {
            apiService.post('/api/page/create', $scope.page, function (result) {
                if (result.data === 'doubleAlias') {
                    notificationService.displayWarning('Trang ' + $scope.page.Name + ' đã có');
                }
                else {
                    notificationService.displaySuccess('Thêm thành công ' + result.data.Name)
                    $state.go('page')
                }
            }, function () {
                console.log('lỗi')
            })
        }

        //ckeditor
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
    }
})(angular.module('haushop.pages'))