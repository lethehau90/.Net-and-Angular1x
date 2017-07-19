/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('haushop.pages', ['haushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('page', {
                url: '/page',
                templateUrl: '/app/components/pages/pageListView.html',
                parent: 'base',
                controller: 'pageListController'
            })
            .state('pageadd', {
                url: '/pageadd',
                templateUrl: '/app/components/pages/pageAddView.html',
                parent: 'base',
                controller: 'pageAddController'
            })
             .state('pageedit', {
                 url: '/pageedit/:id',
                 templateUrl: '/app/components/pages/pageEditView.html',
                 parent: 'base',
                 controller: 'pageEditController'
             })
    }
})()