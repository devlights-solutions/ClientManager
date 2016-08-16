(function () {
    'use strict';
    var app = angular.module('app.services');

    app.factory('ClientSvc', ['ModalSvc', 'StaticModalSvc', '$http', '$q', ClientSvc]);

    function ClientSvc(modalSvc, staticModalSvc, $http, $q) {
        var self = this;
        var urlApiBase = '/Api/Client/';
        var urlAppBase = '/Client/';

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
        function create(client) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: urlApiBase,
                data: JSON.stringify(client),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function edit(client) {
            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: urlApiBase,
                data: JSON.stringify(client),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function remove(clientId) {
            var deferred = $q.defer();

            $http({
                method: 'DELETE',
                url: urlApiBase + clientId,
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
        function openCreate(successCallback) {
            modalSvc.open({
                templateUrl: service.urls.create,
                size: 'sm',
                controller: 'ClientModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openEdit(clientId, successCallback) {
            modalSvc.open({
                templateUrl: service.urls.edit + clientId,
                size: 'sm',
                controller: 'ClientModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openDetail(clientId) {
            modalSvc.open({
                templateUrl: service.urls.detail + clientId,
                size: 'sm',
                controller: 'ClientModalCtrl',
                controllerAs: 'vm'
            }, {
            });
        }

        function openRemove(clientId, successCallback) {
            staticModalSvc.open(null, {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Confirmar',
                headerText: 'Eliminar Client',
                bodyText: 'Está seguro que desea eliminar el client?',
                successCallback: function () {
                    var promise = remove(clientId);
                    if (successCallback) {
                        promise.then(successCallback);
                    }
                }
            });
        }

        return service;
    };
})();