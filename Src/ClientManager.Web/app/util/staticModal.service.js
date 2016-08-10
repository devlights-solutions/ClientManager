(function () {

    var app = angular.module('app.util');

    app.factory('StaticModalSvc', [
        '$uibModal',
        '$templateCache',
        '$timeout',
        StaticModalSvc
    ]);

    function StaticModalSvc($modal, $templateCache, $timeout) {
        var plugDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            controller:  ['$uibModalInstance', 'options', controller],
            controllerAs: 'staticModalVm',
            templateUrl: '/Scripts/app/util/staticModal.html'
        };

        var options = {
            cache: false,
            closeButtonText: 'Close',
            actionButtonText: 'OK',
            headerText: 'Proceed?',
            bodyText: 'Perform this action?',
            successCallback: null,
            cancelCallback: null,
            type: 'info'
        };

        var service = {
            open: open
        };

        return service;

        function open(customPlugDefaults, customOptions) {
            //Create temp objects to work with since we're in a singleton service
            var tempPlugDefaults = {};
            var tempOptions = {};
            //Map angular-ui modal custom defaults to modal defaults defined in service
            angular.extend(tempPlugDefaults, plugDefaults, customPlugDefaults);

            //Map modal.html $scope custom properties to defaults defined in service
            angular.extend(tempOptions, options, customOptions);

            if (!tempOptions.cache) {
                $templateCache.remove(tempPlugDefaults.templateUrl);
            }

            tempPlugDefaults.resolve = {
                options: function () {
                    return tempOptions;
                }
            };

            var instance = $modal.open(tempPlugDefaults);
            setResult(instance, tempOptions);

            return instance;
        }

        function controller($modalInstance, options) {
            var vm = this;

            vm.options = options;

            vm.ok = function () {
                $modalInstance.close();
            };

            vm.close = function (result) {
                $modalInstance.dismiss('cancel');
            };
        }

        function setResult(instance, tempOptions) {
            instance.result.then(function (result) {
                // CLOSE FUNCION QUE SE DA AL CERRAR CON OK EL MODAL
                if (tempOptions.successCallback) {
                    tempOptions.successCallback(result);
                }
            }, function () {
                //CLOSE FUNCION QUE SE DA AL CANCELAR EL MODAL
                if (tempOptions.cancelCallback) {
                    tempOptions.cancelCallback(result);
                }
            });
        }


    };
})()