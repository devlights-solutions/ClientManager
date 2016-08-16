(function () {
    'use strict';
    var app = angular.module('app.services');

    app.factory('ProjectSvc', ['ModalSvc', 'StaticModalSvc', '$http', '$q', ProjectSvc]);

    function ProjectSvc(modalSvc, staticModalSvc, $http, $q) {
        var self = this;
        var urlApiBase = '/Api/Project/';
        var urlAppBase = '/Project/';

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
        function create(project) {
            var deferred = $q.defer();

            $http({
                method: 'POST',
                url: urlApiBase,
                data: JSON.stringify(project),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function edit(project) {
            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: urlApiBase,
                data: JSON.stringify(project),
                contentType: 'application/json'
            }).success(function (data) {
                deferred.resolve(data);
            }).error(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;
        }

        function remove(projectId) {
            var deferred = $q.defer();

            $http({
                method: 'DELETE',
                url: urlApiBase + projectId,
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
                controller: 'ProjectModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openEdit(projectId, successCallback) {
            modalSvc.open({
                templateUrl: service.urls.edit + projectId,
                size: 'sm',
                controller: 'ProjectModalCtrl',
                controllerAs: 'vm'
            }, {
                successCallback: successCallback
            });
        }

        function openDetail(projectId) {
            modalSvc.open({
                templateUrl: service.urls.detail + projectId,
                size: 'sm',
                controller: 'ProjectModalCtrl',
                controllerAs: 'vm'
            }, {
            });
        }

        function openRemove(projectId, successCallback) {
            staticModalSvc.open(null, {
                closeButtonText: 'Cancelar',
                actionButtonText: 'Confirmar',
                headerText: 'Eliminar Project',
                bodyText: 'Está seguro que desea eliminar el project?',
                successCallback: function () {
                    var promise = remove(projectId);
                    if (successCallback) {
                        promise.then(successCallback);
                    }
                }
            });
        }

        return service;
    };
})();