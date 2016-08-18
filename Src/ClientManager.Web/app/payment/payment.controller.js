(function () {
    'use strict';
    var app = angular.module('app.payment');

    app.controller('PaymentCtrl', ['PaymentSvc', 'SmartTableSvc', '$scope', PaymentCtrl]);

    function PaymentCtrl(paymentSvc, smartTableSvc, $scope) {
        var vm = this;

        vm.isLoading = true;
        vm.filter = {};
        //vm.payments = [];

        vm.pipeTable = function (tableState) {
            vm.isLoading = true;

            vm.getPayments(smartTableSvc.getGridParams(tableState))
                .then(function (result) {
                    vm.isLoading = false;
                    tableState.pagination.numberOfPages = result.pageCount;//set the number of pages so the pagination can update
                });
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

        vm.refresh = function () {
            var params = angular.extend({}, smartTableSvc.getGridParams(), vm.filter);
            vm.getPayments(params);
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

        vm.getPayments = function (params) {
            return paymentSvc.getAll(params)
                .then(function (result) {
                    vm.payments = result.list;
                    return result;
                });
        };
    };

})();
