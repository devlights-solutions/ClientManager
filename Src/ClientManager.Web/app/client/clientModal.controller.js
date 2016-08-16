(function () {
    'use strict';
    var app = angular.module('app.client');

    app.controller('ClientModalCtrl', ['ClientSvc', 'ModalSvc', '$uibModalInstance', ClientModalCtrl]);

    function ClientModalCtrl(clientSvc, modalSvc, $modalInstance) {
        var vm = this;
        var $form;

       

        vm.init = function (client) {
            vm.originalClient = angular.extend({}, client);
            vm.client = client;

            loadData();
        };

        
        vm.create = function (formId) {
            _save(formId, clientSvc.create);
        };

        vm.edit = function (formId) {
            _save(formId, clientSvc.edit);
        };

        vm.close = function (client) {
            $modalInstance.close(client);
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        function _save(formId, promise) {
            $form = $form || $(formId);
            if ($form.valid()) {
                promise(vm.client)
                    .then(vm.close)
                    .catch(function (result) {
                        vm.error = result.message;
                    });
            }
        }


        function loadData() {
            
        }

       

        
    };

})();
