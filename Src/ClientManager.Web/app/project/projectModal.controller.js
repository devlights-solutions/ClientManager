(function () {
    'use strict';
    var app = angular.module('app.project');

    app.controller('ProjectModalCtrl', ['ProjectSvc', 'ModalSvc', '$uibModalInstance', ProjectModalCtrl]);

    function ProjectModalCtrl(projectSvc, modalSvc, $modalInstance) {
        var vm = this;
        var $form;

       

        vm.init = function (project) {
            vm.originalProject = angular.extend({}, project);
            vm.project = project;

            loadData();
        };

        
        vm.create = function (formId) {
            _save(formId, projectSvc.create);
        };

        vm.edit = function (formId) {
            _save(formId, projectSvc.edit);
        };

        vm.close = function (project) {
            $modalInstance.close(project);
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        function _save(formId, promise) {
            $form = $form || $(formId);
            if ($form.valid()) {
                promise(vm.project)
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
