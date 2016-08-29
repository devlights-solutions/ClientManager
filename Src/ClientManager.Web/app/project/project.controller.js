(function () {
    'use strict';
    var app = angular.module('app.project');

    app.controller('ProjectCtrl', ['ProjectSvc', 'SmartTableSvc', '$scope', ProjectCtrl]);

    function ProjectCtrl(projectSvc, smartTableSvc, $scope) {
        var vm = this;

        vm.isLoading = true;
        vm.filter = {};
        //vm.projects = [];

        vm.pipeTable = function (tableState) {
           
            vm.isLoading = true;

            vm.getProjects(smartTableSvc.getGridParams(tableState))
                .then(function (result) {
                    vm.isLoading = false;
                    tableState.pagination.numberOfPages = result.pageCount;//set the number of pages so the pagination can update
                });
        };

        vm.init = function (filters) {
            vm.filter = filters;
        }

        vm.create = function () {
            projectSvc.open.create(vm.refresh);
        };

        vm.edit = function (project) {
            projectSvc.open.edit(project.id, vm.refresh);
        };

        vm.remove = function (project) {
            projectSvc.open.remove(project.id, vm.refresh);
        };

        vm.detail = function (project) {
            projectSvc.open.detail(project.id);
        };

        vm.refresh = function () {
            var params = angular.extend({}, smartTableSvc.getGridParams(), vm.filter);
            vm.getProjects(params);
        };

   

        vm.actualizarCliente = function () {
            vm.refresh();
        }

        vm.groupFilter = function (item) {
            var array = [];
            var category = _.find(vm.categories, function (c) {
                return c.id === vm.filter.categoryId;
            });

            if (category) {
                return [category.name];
            }

            return null;
        };

        vm.getProjects = function (params) {
            return projectSvc.getAll(params)
                .then(function (result) {
                    vm.projects = result.list;
                    return result;
                });
        };
    };

})();
