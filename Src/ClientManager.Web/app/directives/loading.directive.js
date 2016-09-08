(function () {
    'use strict';

    var app = angular.module('app.directives');

    app.directive('loading', ['$timeout', LoadingDirective]);

    function LoadingController($scope, $element) {
        var vm = this;

        vm.getSize = function() {
            return vm.size || 'fa-3x';
        };

    }

    function LoadingDirective($timeout) {
        // define constants and helpers used for the directive
        // ...
        return {
            restrict: 'EA', //E = element, A = attribute, C = class, M = comment      
            templateUrl: '/app/directives/loading.html',
            controllerAs: 'loadingVm',
            controller: ['$scope', '$element', LoadingController],
            bindToController: true,
            scope: {
                //@ reads the attribute value, = provides two-way binding, & works with functions
                size: "@"
            },
            link: function (scope, element, attributes) {
            }
        };
    }
})();
