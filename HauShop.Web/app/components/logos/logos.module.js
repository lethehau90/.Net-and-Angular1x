(function () {

    angular.module("haushop.logos", ["haushop.common"]).config(config)

    config.$inject = ["$stateProvider", "$urlRouterProvider"]

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('logo', {
                url: '/logo',
                parent: 'base',
                templateUrl: '/app/components/logos/logoListView.html',
                controller: 'logoListController'
            })
            .state('addlogo',{
                url: '/addlogo',
                parent: 'base',
                templateUrl: '/app/components/logos/logoAddView.html',
                controller: 'logoAddController'
            })
            .state('editlogo',{
                url: '/editlogo/:id',
                parent: 'base',
                templateUrl: '/app/components/logos/logoEditView.html',
                controller: 'logoEditController'
            })
    }
})()