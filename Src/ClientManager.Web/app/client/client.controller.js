(function () {
    'use strict';
    var app = angular.module('app.client');

    app.controller('ClientCtrl', ['ClientSvc', 'SmartTableSvc', '$scope', ClientCtrl]);

    function ClientCtrl(clientSvc, smartTableSvc, $scope) {
        var vm = this;

        vm.isLoading = true;
        vm.filter = {};
        //vm.clients = [];

        vm.pipeTable = function (tableState) {
            vm.isLoading = true;

            vm.getClients(smartTableSvc.getGridParams(tableState))
                .then(function (result) {
                    vm.isLoading = false;
                    tableState.pagination.numberOfPages = result.pageCount;//set the number of pages so the pagination can update
                });
        };

        vm.create = function () {
            clientSvc.open.create(vm.refresh);
        };

        vm.edit = function (client) {
            clientSvc.open.edit(client.id, vm.refresh);
        };

        vm.remove = function (client) {
            clientSvc.open.remove(client.id, vm.refresh);
        };

        vm.detail = function (client) {
            clientSvc.open.detail(client.id);
        };

        vm.refresh = function () {
            var params = angular.extend({}, smartTableSvc.getGridParams(), vm.filter);
            vm.getClients(params);
        };

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

        vm.getClients = function (params) {
            return clientSvc.getAll(params)
                .then(function (result) {
                    vm.clients = result.list;
                    return result;
                });
        };
    };

})();
