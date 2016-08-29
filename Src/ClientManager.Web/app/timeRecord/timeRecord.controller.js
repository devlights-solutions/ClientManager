(function () {
    'use strict';
    var app = angular.module('app.timeRecord');

    app.controller('TimeRecordCtrl', ['TimeRecordSvc', 'SmartTableSvc', '$scope', TimeRecordCtrl]);

    function TimeRecordCtrl(timeRecordSvc, smartTableSvc, $scope) {
        var vm = this;
        vm.isLoading = true;
        vm.projectOptions = { key: 'projectIdFilter' };
        vm.userOptions = { key: 'userIdFilter' };
        vm.areAllSelected = false;

        vm.pipeTable = function (tableState) {
            var params = buildFilter(tableState);
            loadTable(params).then(function (result) {
                tableState.pagination.numberOfPages = result.pageCount;//set the number of pages so the pagination can update
            });
        };

        vm.refresh = function () {
            var params = buildFilter();
    
            loadTable(params);
        };

        vm.selectAll = function () {
            return _.each(vm.timeRecords, function (tr) {
                 tr.checked=vm.areAllSelected;
            })
        }

        vm.mostrarBotonPago = function () {
            return _.some(vm.timeRecords, function (tr) {
                return tr.checked;
            })
        }
              

        vm.init = function (filters) {
            vm.filter = filters;

            $scope.$on('uiSelect.init.' + vm.projectOptions.key, function (e, args) {
                console.log(args);

                if (args) vm.listProject = args;
                var project = _.find(vm.listProject, function (p) {
                    return p.id == vm.filter.projectId;
                })
                vm.actualizarCliente(project)
            })
        };

        vm.pagar = function (timeRecord) {
            timeRecordSvc.open.confirmarPago([timeRecord.id], vm.refresh);
        }

        vm.pay = function () {
            var trs = _.filter(vm.timeRecords, function (tr) {
                return tr.checked;
            })
            trs = _.map(trs, function (tr) {
                return tr.id
            })
            timeRecordSvc.open.confirmarPago(trs, vm.refresh);
        };

        vm.create = function () {
            timeRecordSvc.open.create(vm.refresh, vm.filter.projectId, vm.filter.userId);
        };

        vm.edit = function (timeRecord) {
            timeRecordSvc.open.edit(timeRecord.id, vm.refresh);
        };

        vm.remove = function (timeRecord) {
            timeRecordSvc.open.remove(timeRecord.id, vm.refresh);
        };

        vm.detail = function (timeRecord) {
            timeRecordSvc.open.detail(timeRecord.id);
        };

 

        vm.actualizarCliente = function (project, model) {
            vm.clientRazonSocial = '';
            if(project){
                vm.clientRazonSocial = 'Cliente: ' + project.clientRazonSocial;
            }
            vm.refresh();
        }

        vm.actualizarUsuario = function () {
            vm.refresh();
        }


        function buildFilter(tableState) {
            return angular.extend({}, smartTableSvc.getGridParams(tableState), vm.filter);

        }

        function loadTable(params) {
            vm.isLoading = true;

            return timeRecordSvc.getAll(params)
                .then(function (result) {
                    vm.isLoading = false;
                    vm.timeRecords = result.list;
                    return result;
                });
        }
    };

})();
