(function() {

    'use strict';

    var app = angular.module('approveApp', []);

    app.controller('approveController', ['$scope', '$http', '$filter', function($scope, $http, $filter) {

        $scope.isApproving = false;
        $scope.items = [];

        $http({ method: 'POST', url: '/Approve/Files' }).success(function(data) {

            angular.forEach(data, function(item) {
                item.FreeTags = eval(item.FreeTags).join();
            });

            $scope.items = data;

        }).error(function() {

            $scope.items = [];

        });

        $scope.approveClick = function(id, index) {


            $scope.isApproving = true;


            $http({ method: 'POST', url: '/Approve/Approve', data: { id: id } }).success(function (data) {

                $scope.items.splice(index, 1);

            }).error(function () {

                $scope.isApproving = false;
                alert('no');

            });

        };

    }]);


}());