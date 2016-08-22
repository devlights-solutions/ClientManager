(function () {

    var app = angular.module('app.util');
    app.controller('UiSelectCtrl', [
        '$scope',
        UiSelectCtrl
    ]);

    function UiSelectCtrl($scope) {
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
            config = model.options || {};
            vm.hasButton = config.createModalPromise ? true : false;

            $scope.$on('uiSelect.getList.' + config.key, function (e, callback) {
                if(callback) callback(list);
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