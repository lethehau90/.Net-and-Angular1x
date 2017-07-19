/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('haushop.statistic', ['haushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('statistic', {
                url: '/statistic',
                templateUrl: '/app/components/statistic/revenueStatisticView.html',
                parent: 'base',
                controller: 'revenueStatisticController'
            })
            
    }
})()