(function () {
    'use strict';
    var app = angular.module('app.services');

    app.factory('TimeRecordSvc', ['ModalSvc', 'StaticModalSvc', '$http', '$q', TimeRecordSvc]);

    function TimeRecordSvc(modalSvc, staticModalSvc, $http, $q) {
        var self = this;
        var urlApiBase = '/Api/TimeRecord/';
        var urlAppBase = '/TimeRecord/';

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
        function create(timeRecord) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: urlApiBase,
                data: JSON.stringify(timeRecord),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function edit(timeRecord) {
            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: urlApiBase,
                data: JSON.stringify(timeRecord),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function remove(timeRecordId) {
            var deferred = $q.defer();

            $http({
                method: 'DELETE',
                url: urlApiBase + timeRecordId,
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
                controller: 'TimeRecordModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openEdit(timeRecordId, successCallback) {
            modalSvc.open({
                templateUrl: service.urls.edit + timeRecordId,
                size: 'sm',
                controller: 'TimeRecordModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openDetail(timeRecordId) {
            modalSvc.open({
                templateUrl: service.urls.detail + timeRecordId,
                size: 'sm',
                controller: 'TimeRecordModalCtrl',
                controllerAs: 'vm'
            }, {
            });
        }

        function openRemove(timeRecordId, successCallback) {
            staticModalSvc.open(null, {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Confirmar',
                headerText: 'Eliminar TimeRecord',
                bodyText: 'Está seguro que desea eliminar el timeRecord?',
                successCallback: function () {
                    var promise = remove(timeRecordId);
                    if (successCallback) {
                        promise.then(successCallback);
                    }
                }
            });
        }

        return service;
    };
})();