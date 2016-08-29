(function () {
    'use strict';
    var app = angular.module('app.payment');

    app.controller('PaymentCtrl', ['PaymentSvc', 'SmartTableSvc', '$scope', PaymentCtrl]);

    function PaymentCtrl(paymentSvc, smartTableSvc, $scope) {
        var vm = this;
        vm.isLoading = true;
        vm.projectOptions = { key: 'projectIdFilter' };
        vm.listProject = [];

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

        vm.create = function () {
            paymentSvc.open.create(vm.refresh, vm.filter.projectId);
        };

        vm.edit = function (payment) {
            paymentSvc.open.edit(payment.id, vm.refresh);
        };

        vm.remove = function (payment) {
            paymentSvc.open.remove(payment.id, vm.refresh);
        };

        vm.detail = function (payment) {
            paymentSvc.open.detail(payment.id);
        };

 
        vm.actualizarCliente = function (project, model) {
            vm.clientRazonSocial = '';
            if(project){
                vm.clientRazonSocial = 'Cliente: ' + project.clientRazonSocial;
                vm.costoTotal = 'Costo Total: $' + project.costoTotal;
            }

            $scope.$broadcast('uiSelect.getList.' + vm.projectOptions.key, {
                callback: function (listProject) {
                    console.log(listProject);
                }
            });

            vm.refresh();
        }        


        function buildFilter(tableState) {
            return angular.extend({}, smartTableSvc.getGridParams(tableState), vm.filter);

        }

        function loadTable(params) {
            vm.isLoading = true;

            return paymentSvc.getAll(params)
                .then(function (result) {
                    vm.isLoading = false;
                    vm.payments = result.list;
                    return result;
                });
        }
    };

})();
