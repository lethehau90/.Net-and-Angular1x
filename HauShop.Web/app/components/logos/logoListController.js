(function (app) {
    app.controller('logoListController', logoListController)

    logoListController.$inject = ['$scope', 'notificationService', 'apiService', '$filter', '$ngBootbox']

    function logoListController($scope, notificationService, apiService, $filter, $ngBootbox) {

        $scope.logos = []
        $scope.loading = true

        $scope.deletelogo = deletelogo

        function deletelogo(Id) {
            config = {
                params: {
                    Id: Id
                }
            }
            apiService.del('api/logo/delete', config, function (result) {
                notificationService.displaySuccess('Xóa thành công');
                getAllLogo()
            }, function (error) {
                notificationService.displayError('Xóa  không thành công');
            })
        }

        function getAllLogo() {
            apiService.get('api/logo/getall', null, function (result) {
                if (result.data.length == 0) {
                    notificationService.displayWarning('Không có bản ghi nào');
                }
                $scope.logos = result.data
                $scope.loading = false

            }, function (error) {

            })
        }

        getAllLogo()

    }

})(angular.module('haushop.logos'))