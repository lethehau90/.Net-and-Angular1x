(function (app) {

    app.controller('logoAddController', logoAddController)

    logoAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state', 'commonService']

    function logoAddController($scope, apiService, notificationService, $state, commonService) {


        $scope.logo = {}

        $scope.getCode = getCode

        $scope.Addlogo = Addlogo

        function getCode() {
            $scope.logo.Code = commonService.getSeoTitle($scope.logo.Name)
        }

        function Addlogo() {
            apiService.post('api/logo/create', $scope.logo, function (result) {
                if (result.data === 'doubleCode') {
                    notificationService.displayWarning('Code ' + $scope.logo.Code + ' đã có');
                }
                else {
                    notificationService.displaySuccess('Thêm thành công ' + result.data.Name)
                    $state.go('logo')
                }
            }, function (error) {
                notificationService.displayError('Lỗi không thêm được')
            })
        }

        //finder
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.logo.Image = fileUrl;
                })
            }
            finder.popup();
        }

    }

})(angular.module('haushop.logos'))