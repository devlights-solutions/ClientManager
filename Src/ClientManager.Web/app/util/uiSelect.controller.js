(function () {

    var app = angular.module('app.util');
    app.controller('UiSelectCtrl', [
        '$scope',
        '$timeout',
        UiSelectCtrl
    ]);

    function UiSelectCtrl($scope, $timeout) {
        var vm = this;
        var config = {
            createModalPromise: null,
            createCallback: null
        };
        
        
        vm.list =[];

        vm.init = init;
        vm.create = create;

        ///////////////////

        function init(model) {
            vm.list = model.list || [];

            $timeout(function () { $scope.$emit('uiSelect.init.' + config.key, angular.copy(vm.list)) });

            config = model.options || {};
            vm.hasButton = config.createModalPromise ? true : false;

            $scope.$on('uiSelect.getList.' + config.key, function (e, args) {
                if (args.callback) args.callback(vm.list);
            })
        }

        function create() {
            config.createModalPromise(_successCallback);
        }

        function _successCallback(item) {
            vm.list.push(item);
            if (config.createCallback) {
                config.createCallback(item);
            }
        }

    }
})();