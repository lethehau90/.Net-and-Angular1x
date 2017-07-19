/// <reference path="\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('haushop.footers', ['haushop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('footer', {
                url: '/footer',
                templateUrl: '/app/components/footers/footerListView.html',
                parent: 'base',
                controller: 'footerListController'
            })
            .state('footeredit', {
                url: '/footeredit/:id',
                templateUrl: '/app/components/footers/footerEditView.html',
                parent: 'base',
                controller: 'footerEditController'
            })
            .state('footeradd', {
                url: '/footeradd',
                templateUrl: '/app/components/footers/footerAddView.html',
                parent: 'base',
                controller: 'footerAddController'
            })
    }
})()