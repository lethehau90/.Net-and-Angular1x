/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('pageEditController', pageEditController);

    pageEditController.$inject = ['$scope', 'apiService', 'commonService', 'notificationService', '$state', '$filter','$stateParams']

    function pageEditController($scope, apiService, commonService, notificationService, $state, $filter, $stateParams) {
        $scope.page = []

        $scope.GetSeoTitle = GetSeoTitle;

        $scope.pageGetByID = pageGetByID

        function pageGetByID() {
            apiService.get('/api/page/getbyid/' + $stateParams.id, null, function (result) {
                $scope.page = result.data;
            }, function (err) {
                console.log('Lỗi')
            })
        }

        function GetSeoTitle() {
            $scope.page.Alias = commonService.getSeoTitle($scope.page.Name);
        }

        $scope.EditPage = EditPage

        function EditPage() {
            apiService.put('/api/page/update', $scope.page, function (result) {
                notificationService.displaySuccess('Cập nhật thành công ' + result.data.Name)
                $state.go('page')
            }, function () {
                console.log('lỗi')
            })
        }

        //ckeditor
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        pageGetByID();
    }
})(angular.module('haushop.pages'))