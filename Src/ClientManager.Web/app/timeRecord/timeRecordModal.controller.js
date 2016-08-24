(function () {
    'use strict';
    var app = angular.module('app.timeRecord');

    app.controller('TimeRecordModalCtrl', ['TimeRecordSvc', 'ModalSvc', '$uibModalInstance', TimeRecordModalCtrl]);

    function TimeRecordModalCtrl(timeRecordSvc, modalSvc, $modalInstance) {
        var vm = this;
        var $form;

       

        vm.init = function (timeRecord) {
            vm.originalTimeRecord = angular.extend({}, timeRecord);
            vm.timeRecord = timeRecord;

            loadData();
        };

        
        vm.create = function (formId) {
            _save(formId, timeRecordSvc.create);
        };

        vm.edit = function (formId) {
            _save(formId, timeRecordSvc.edit);
        };

        vm.close = function (timeRecord) {
            $modalInstance.close(timeRecord);
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        function _save(formId, promise) {
            $form = $form || $(formId);
            if ($form.valid()) {
                promise(vm.timeRecord)
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
