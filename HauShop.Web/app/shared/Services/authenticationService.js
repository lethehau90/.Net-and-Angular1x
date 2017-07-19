(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', '$window', 'authData',
        function ($http, $q, $window, authData) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = data;
                $window.sessionStorage["TokenInfo"] = JSON.stringify(tokenInfo);
            }

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                $window.sessionStorage["TokenInfo"] = null;
            }

            this.init = function () {

                tokenInfo = $window.sessionStorage["TokenInfo"]

                if (tokenInfo === undefined || tokenInfo === "null") {
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    authData.authenticationData.fullName = "";
                    authData.authenticationData.accessToken = "";
                }
                else {
                    tokenInfo = JSON.parse($window.sessionStorage["TokenInfo"]);
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = tokenInfo.userName;
                    authData.authenticationData.fullName = tokenInfo.fullName
                    authData.authenticationData.accessToken = tokenInfo.accessToken;
                }
            }

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
                else {
                    return window.location.href = '/admin#/login'
                }
            }

            this.validateRequest = function () {
                this.setHeader();
                var url = '/api/home/testmethod';
                var deferred = $q.defer();
                $http.get(url).then(function () {
                    deferred.resolve(null);
                }, function (error) {
                    deferred.reject(error);
                });
                return deferred.promise;
            }

            this.init();
        }
    ]);
})(angular.module('haushop.common'));