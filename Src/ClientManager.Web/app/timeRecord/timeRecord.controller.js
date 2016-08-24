(function () {
    'use strict';
    var app = angular.module('app.timeRecord');

    app.controller('TimeRecordCtrl', ['TimeRecordSvc', 'SmartTableSvc', '$scope', TimeRecordCtrl]);

    function TimeRecordCtrl(timeRecordSvc, smartTableSvc, $scope) {
        var vm = this;
        vm.isLoading = true;
        vm.projectOptions = { key: 'projectIdFilter' };

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

            $scope.$broadcast('uiSelect.getList.' + vm.projectOptions.key, { callback: function(listProject){
                console.log(listProject);
            }});
            //vm.refresh();
        };

        vm.create = function () {
            timeRecordSvc.open.create(vm.refresh, vm.filter.projectId);
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
            
            vm.clientRazonSocial = 'Cliente: ' + project.clientRazonSocial;
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
