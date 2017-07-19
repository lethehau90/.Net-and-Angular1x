(function (app) {
    app.controller('revenueStatisticController', revenueStatisticController)

    revenueStatisticController.$inject = ['apiService','$scope','notificationService', '$filter']

    function revenueStatisticController(apiService, $scope, notificationService, $filter) {
        $scope.getStatistic = getStatistic;
        $scope.tableData = []

        $scope.labels = [];
        $scope.series = ['Doanh thu', 'Lợi nhuận'];

        $scope.chartData = [];
    
        $scope.search = search;

        $scope.fromDate = '01/01/2016'
        $scope.toDate = '01/01/2018'

        function search() {
            $scope.getStatistic($filter('date')($scope.fromDate, 'MM/dd/yyyy'), $filter('date')($scope.toDate, 'MM/dd/yyyy'))
        }

        function getStatistic(fromDate, toDate) {
         
            apiService.get('api/statistic/getrevenue?fromDate=' + fromDate + '&toDate=' + toDate, null, function (response) {
                $scope.tableData = response.data;
                var lables = []
                var chartData = []
                var revenues = []
                var benefits = []
                $.each(response.data, function (i, item) {
                    lables.push($filter('date')(item.Date,'dd/MM/yyyy'))
                    revenues.push(item.Revenues)
                    benefits.push(item.Benefit)
                })
                chartData.push(revenues);
                chartData.push(benefits);

                $scope.chartData = chartData;
                $scope.labels = lables;
            }, function (response) {
                notificationService.displayError('Không thể tải dữ liệu')
            })
    
        }

        $scope.getStatistic($scope.fromDate, $scope.toDate)
    }

})(angular.module('haushop.statistic'))