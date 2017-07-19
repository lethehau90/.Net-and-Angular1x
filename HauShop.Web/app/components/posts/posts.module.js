(function () {
    angular.module('haushop.posts', ['haushop.common']).config(config)

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('posts', {
            url: '/posts',
            parent : 'base',
            templateUrl: '/app/components/posts/postListView.html',
            controller : 'postListController'
            })
            .state('postadd', {
                url: '/postadd',
                parent: 'base',
                templateUrl: '/app/components/posts/postAddView.html',
                controller: 'postAddController'
            })
            .state('postedit', {
                url: '/postedit',
                parent: 'base',
                templateUrl: '/app/components/posts/postEditView.html',
                controller: 'postEditController'
            })
    }
})()