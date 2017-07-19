/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerEditController', footerEditController)

    footerEditController.$inject = ['$scope', 'apiService', 'commonService', 'notificationService', '$state', '$filter', '$stateParams']

    function footerEditController($scope, apiService, commonService, notificationService, $state, $filter, $stateParams) {
        $scope.footer = []
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.footerGetByID = footerGetByID;

        function GetSeoTitle() {
            $scope.footer.Alias = commonService.getSeoTitle($scope.footer.Name);
        }

        $scope.EditFooter = EditFooter

        function EditFooter() {
            apiService.put('/api/footer/update', $scope.footer, function (result) {
                notificationService.displaySuccess('Cập nhật thành công ' + result.data.ID)
                $state.go('footer')
            }, function () {
                console.log('lỗi')
            })
        }

        function footerGetByID() {
            apiService.get('/api/footer/getbyid/' + $stateParams.id, null, function (result) {
                $scope.footer = result.data;
            }, function (err) {
                console.log('Lỗi')
            })
        }

        //ckeditor
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '400px'
        }

        footerGetByID();
    }
})(angular.module('haushop.footers'))