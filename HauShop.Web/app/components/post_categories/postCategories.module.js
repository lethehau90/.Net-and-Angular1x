(function () {
    angular.module('haushop.postcategories', ['haushop.common']).config(config)

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('postcategories', {
                url: '/postcategories',
                parent: 'base',
                templateUrl: '/app/components/post_categories/postCategoryListView.html',
                controller: 'postCategoryListController'
            })
            .state('postcategoryadd', {
                url: '/postcategoryadd',
                parent: 'base',
                templateUrl: '/app/components/post_categories/postCategoryAddView.html',
                controller: 'postCategoryAddController'
            })
            .state('postcategoryedit', {
                url: '/postcategoryedit/:id',
                parent: 'base',
                templateUrl: '/app/components/post_categories/postCategoryEditView.html',
                controller: 'postCategoryEditController'
            })
    }

})()