(function () {
    'use strict';
    var app = angular.module('app.demo');

    app.controller('DemoModalCtrl', ['DemoSvc', 'InscripcionEmpresaSvc', 'EmpresaSvc', 'LocalidadSvc', 'TipoSucursalSvc', 'TipoResponsableSvc', 'TipoAsesorSvc', 'EmpresaSvc', 'ModalSvc', '$uibModalInstance', DemoModalCtrl]);

    function DemoModalCtrl(demoSvc, InscripcionEmpresaSvc, EmpresaSvc, localidadSvc, tipoSucursalSvc, tipoResponsableSvc, tipoAsesorSvc, empresaSvc, modalSvc, $modalInstance) {
        var vm = this;
        var $form;

        var sucursalModel = {
            nombre: '', tipoSucursalId: '', telefono: '', email: '', domicilio: {
                    calle: '', numero: '', codigoPostal: '', localidadId:''
                }
        }

        vm.sucursal = angular.copy(sucursalModel);
        vm.empresas = [];
        vm.selectedEmpresa = null;

        // Tipos, 
        vm.tiposSucursales = [];
        vm.tiposAsesores = [];
        vm.tiposResponsables = [];
        vm.localidades = [];

        vm.init = function (demo) {
            vm.originalProduct = angular.extend({}, demo);
            vm.demo = demo;

            loadData();

            //TODO: mover al jquery validate.extension o algo asi
            $.validator.setDefaults({
                ignore: ".ignore"
            });
        };

        vm.getEmpresas = function (searchText) {
            if (!searchText) return;

            _getEmpresas(searchText).then(function (empresas) {
                vm.empresas = empresas;
            });
        };

        vm.empresaSelect = function (empresa, model) {
            vm.selectedEmpresa = empresa;
        }

        vm.create = function (formId) {
            _save(formId, demoSvc.create);
        };

        vm.edit = function (formId) {
            _save(formId, demoSvc.edit);
        };

        vm.close = function (demo) {
            $modalInstance.close(demo);
        };

        vm.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        function _save(formId, promise) {
            $form = $form || $(formId);
            if ($form.valid()) {
                promise(vm.demo)
                    .then(vm.close)
                    .catch(function (result) {
                        vm.error = result.message;
                    });
            }
        }

        function _getEmpresas(searchText) {
            return empresaSvc.getAll({ criteria: searchText })
                .then(function (response) {
                    return response;
                });
        };

        function loadData(){
            tipoSucursalSvc.getAll().then(function (response) {
                vm.tiposSucursales = response.list;
            });
            tipoAsesorSvc.getAll().then(function (response) {
                vm.tiposAsesores = response.list;
            });
            tipoResponsableSvc.getAll().then(function (response) {
                vm.tiposResponsables = response.list;
            });
            localidadSvc.getAll().then(function (response) {
                vm.localidades = response.list;
            });
        }

        vm.removeFromCollection = function (collectionName, element) {
            var idx = _.findIndex(vm.demo[collectionName], element);
            if (idx > -1) {
                vm.demo[collectionName].splice(idx, 1);
            }
        }

        function getById(collection, id) {
            return _.find(vm[collection], function(elem){
                return elem.id == id;
            });
        }

        vm.esFitosanitario = function () {
            if (vm.demo.tipoAsesorId == null || vm.demo.tipoAsesorId == '')
                return false;
            
            var tipoAsesor = _.find(vm.tiposAsesores,
                                        function (item) {
                                            if (item.id == vm.demo.tipoAsesorId) {
                                                return item;
                                            }
                                        });

            if (tipoAsesor.nombre == 'Fitosanitario') {
                return true;
            }

            return false;
        }

        vm.cargaHorarios = function () {
            if (vm.selectedEmpresa) {
                return _.find(vm.selectedEmpresa.categorias,
                                function (cat) {
                                    if (cat.numeroCategoria == '01' || cat.numeroCategoria == '08') {
                                        return true;
                                    }
                                    return false;
                                });
            }
            return false;
        }
    };

})();
