(function () {
    'use strict';
    var app = angular.module('app.demo');

    app.controller('DemoCtrl', ['DemoSvc', 'SmartTableSvc', '$scope', DemoCtrl]);

    function DemoCtrl(demoSvc, smartTableSvc, $scope) {
        var vm = this;

        vm.isLoading = true;
        vm.filter = {};
        vm.inscripcionesAsesor = [];

        vm.pipeTable = function (tableState) {
            vm.isLoading = true;

            vm.getInscripcionesAsesor(smartTableSvc.getGridParams(tableState))
                .then(function (result) {
                    vm.isLoading = false;
                    tableState.pagination.numberOfPages = result.pageCount;//set the number of pages so the pagination can update
                });
        };

        vm.create = function () {
            demoSvc.open.create(vm.refresh);
        };

        vm.edit = function (demo) {
            demoSvc.open.edit(demo.id, vm.refresh);
        };

        vm.remove = function (demo) {
            demoSvc.open.remove(demo.id, vm.refresh);
        };

        vm.detail = function (demo) {
            demoSvc.open.detail(demo.id);
        };

        vm.refresh = function () {
            var params = angular.extend({}, smartTableSvc.getGridParams(), vm.filter);
            vm.getInscripcionesAsesor(params);
        };

        vm.groupFilter = function (item) {
            var array = [];
            var category = _.find(vm.categories, function(c) {
                return c.id === vm.filter.categoryId;
            });

            if (category) {
                return [category.name];
            }

            return null;
        };

        vm.getInscripcionesAsesor = function(params) {
            return demoSvc.getAll(params)
                .then(function(result) {
                    vm.inscripcionesAsesor = result.list;
                    return result;
                });
        };
    };

})();
