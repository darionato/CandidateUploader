(function() {

    'use strict';


    var app = angular.module('viewApp', []);


    app.controller('viewController', ['$scope', '$http', '$filter', function ($scope, $http, $filter) {


        var orderBy = $filter('orderBy');

        $scope.items = [];


        $http({ method: 'POST', url: '/View/Files' }).success(function (data) {

            $scope.items = data;

        }).error(function () {

            $scope.items = [];

        });

        $scope.order = function () {

            $scope.items = orderBy($scope.items, $scope.selectedOrder, false);

        };


    }]);


    app.directive('cuTemplateItem', function() {
        return {
            restrict: 'E',
            templateUrl: 'View/TemplateItem'
        };
    });


}());