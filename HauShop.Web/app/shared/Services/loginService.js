﻿(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData',
    function ($http, $q, authenticationService, authData) {
        var userInfo;
        var deferred;

        this.login = function (userName, password) {
            deferred = $q.defer();
            var data = "grant_type=password&username=" + userName + "&password=" + password;
            $http.post('/oauth/token', data, {
                headers:
                   { 'Content-Type': 'application/x-www-form-urlencoded' }
            }).then(function (response) {
                userInfo = {
                    accessToken: response.data.access_token,
                    userName: userName,
                    fullName: response.data.fullName
                };
                authenticationService.setTokenInfo(userInfo);
                authData.authenticationData.IsAuthenticated = true;
                authData.authenticationData.userName = userName;
                authData.authenticationData.fullName = userInfo.fullName;
                authData.authenticationData.accessToken = userInfo.accessToken;
                deferred.resolve(null);
            }, function (err, status) {
                    authData.authenticationData.IsAuthenticated = false;
                    authData.authenticationData.userName = "";
                    deferred.resolve(err);
            })
     
            return deferred.promise;
        }

        this.logOut = function () {
            $http.post('/api/account/logout', null).then(function (response) {
                authenticationService.removeToken();
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                authData.authenticationData.fullName = "";
                authData.authenticationData.accessToken = "";
            });
        }
    }]);
})(angular.module('haushop.common'));