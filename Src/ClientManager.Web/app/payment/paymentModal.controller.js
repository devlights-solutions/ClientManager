(function () {
    'use strict';
    var app = angular.module('app.payment');

    app.controller('PaymentModalCtrl', ['PaymentSvc', 'ModalSvc', '$uibModalInstance', PaymentModalCtrl]);

    function PaymentModalCtrl(paymentSvc, modalSvc, $modalInstance) {
        var vm = this;
        var $form;

       

        vm.init = function (payment) {
            vm.originalPayment = angular.extend({}, payment);
            vm.payment = payment;

            loadData();
        };

        
        vm.create = function (formId) {
            _save(formId, paymentSvc.create);
        };

        vm.edit = function (formId) {
            _save(formId, paymentSvc.edit);
        };

        vm.close = function (payment) {
            $modalInstance.close(payment);
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        function _save(formId, promise) {
            $form = $form || $(formId);
            if ($form.valid()) {
                promise(vm.payment)
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
