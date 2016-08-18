(function () {
    'use strict';
    var app = angular.module('app.services');

    app.factory('PaymentSvc', ['ModalSvc', 'StaticModalSvc', '$http', '$q', PaymentSvc]);

    function PaymentSvc(modalSvc, staticModalSvc, $http, $q) {
        var self = this;
        var urlApiBase = '/Api/Payment/';
        var urlAppBase = '/Payment/';

        var service = {
            urls: {
                create: urlAppBase + 'Create',
                edit: urlAppBase + 'Edit/',
                detail: urlAppBase + 'Detail/'
            },
            create: create,
            edit: edit,
            remove: remove,
            getAll: getAll,

            open: {
                create: openCreate,
                edit: openEdit,
                remove: openRemove,
                detail: openDetail
            }
        };

        /** 
         * REST
         */
        function create(payment) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: urlApiBase,
                data: JSON.stringify(payment),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function edit(payment) {
            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: urlApiBase,
                data: JSON.stringify(payment),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function remove(paymentId) {
            var deferred = $q.defer();

            $http({
                method: 'DELETE',
                url: urlApiBase + paymentId,
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function getAll(gridFilters) {
            var deferred = $q.defer();

            $http({
                method: 'GET',
                url: urlApiBase,
                params: gridFilters,
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        /** 
         * Modals
         */
        function openCreate(successCallback, projectId) {
            modalSvc.open({
                templateUrl: service.urls.create + "?projectId=" + projectId,
                size: 'sm',
                controller: 'PaymentModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openEdit(paymentId, successCallback) {
            modalSvc.open({
                templateUrl: service.urls.edit + paymentId,
                size: 'sm',
                controller: 'PaymentModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openDetail(paymentId) {
            modalSvc.open({
                templateUrl: service.urls.detail + paymentId,
                size: 'sm',
                controller: 'PaymentModalCtrl',
                controllerAs: 'vm'
            }, {
            });
        }

        function openRemove(paymentId, successCallback) {
            staticModalSvc.open(null, {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Confirmar',
                headerText: 'Eliminar Payment',
                bodyText: 'Está seguro que desea eliminar el payment?',
                successCallback: function () {
                    var promise = remove(paymentId);
                    if (successCallback) {
                        promise.then(successCallback);
                    }
                }
            });
        }

        return service;
    };
})();