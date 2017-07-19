(function (app) {
    app.controller('logoEditController', logoEditController)

    logoEditController.$inject = ['$scope', '$state', 'notificationService', 'commonService', 'apiService', '$stateParams']

    function logoEditController($scope, $state, notificationService, commonService, apiService, $stateParams) {

        $scope.logo = []

        $scope.getByLogoId = getByLogoId

        $scope.Editlogo = Editlogo

        function Editlogo() {
            apiService.put('api/logo/update', $scope.logo, function (result) {
                notificationService.displaySuccess('Cập nhật thành công ' + result.data.Name)
                $state.go('logo')
            }, function (error) {
                notificationService.displayError('Lỗi không cập nhật được')
            })
        }

        function getByLogoId() {
            var config = {
                params: {
                    id: $stateParams.id
                }
            }

            apiService.get('api/logo/getbyid', config, function (result) {
                $scope.logo = result.data
            }, function (error) {
                notificationService.displayError('Lỗi không load được')
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

        getByLogoId()
    }

})(angular.module('haushop.logos'))