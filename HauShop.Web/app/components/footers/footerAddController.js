/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerAddController', footerAddController)

    footerAddController.$inject = ['$scope', 'apiService', 'commonService', 'notificationService', '$state', '$filter']

    function footerAddController($scope, apiService, commonService, notificationService, $state, $filter) {
        $scope.footer = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.footer.Alias = commonService.getSeoTitle($scope.footer.Name);
        }

        $scope.AddFooter = AddFooter

        function AddFooter() {
            apiService.post('/api/footer/create', $scope.footer, function (result) {
                if (result.data === 'doubleID') {
                    notificationService.displayWarning('Trang ' + $scope.footer.ID + ' đã có');
                }
                else {
                    notificationService.displaySuccess('Thêm thành công ' + result.data.Name)
                    $state.go('footer')
                }
            }, function () {
                console.log('lỗi')
            })
        }

        //ckeditor
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '400px'
        }
    }
})(angular.module('haushop.footers'))