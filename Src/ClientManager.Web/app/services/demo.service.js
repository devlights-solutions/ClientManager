(function () {
    'use strict';
    var app = angular.module('app.services');

    app.factory('DemoSvc', ['ModalSvc', 'StaticModalSvc', '$http', '$q', DemoSvc]);

    function DemoSvc(modalSvc, staticModalSvc, $http, $q) {
        var self = this;
        var urlApiBase = '/Api/Demo/';
        var urlAppBase = '/Demo/';

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
        function create(demo) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: urlApiBase,
                data: JSON.stringify(demo),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function edit(demo) {
            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: urlApiBase,
                data: JSON.stringify(demo),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function remove(demoId) {
            var deferred = $q.defer();

            $http({
                method: 'DELETE',
                url: urlApiBase + demoId,
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
                controller: 'DemoModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openEdit(demoId, successCallback) {
            modalSvc.open({
                templateUrl: service.urls.edit + demoId,
                size: 'sm',
                controller: 'DemoModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openDetail(demoId) {
            modalSvc.open({
                templateUrl: service.urls.detail + demoId,
                size: 'sm',
                controller: 'DemoModalCtrl',
                controllerAs: 'vm'
            }, {
            });
        }

        function openRemove(demoId, successCallback) {
            staticModalSvc.open(null, {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Confirmar',
                headerText: 'Eliminar Demo',
                bodyText: 'Está seguro que desea eliminar el demo?',
                successCallback: function () {
                    var promise = remove(demoId);
                    if (successCallback) {
                        promise.then(successCallback);
                    }
                }
            });
        }

        return service;
    };
})();