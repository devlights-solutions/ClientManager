(function () {

    var app = angular.module('app.util');
    app.controller('UiSelectCtrl', [
        UiSelectCtrl
    ]);

    function UiSelectCtrl() {
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