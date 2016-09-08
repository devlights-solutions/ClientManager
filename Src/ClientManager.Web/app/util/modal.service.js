(function () {

    var app = angular.module('app.util');

    app.factory('ModalSvc', [
        '$uibModal',
        '$templateCache',
        'SpinnerSvc',
        '$timeout',
        ModalSvc
    ]);

    function ModalSvc($modal, $templateCache, spinnerSvc, $timeout) {
        var plugDefaults = {
            animation: true,
            backdrop: 'static',
            keyboard: true,
            modalFade: true,
            size: 'md'
        };

        var options = {
            cache: false,
            validateForm: null, 
            openCallback: null,
            renderCallback: null,
            successCallback: null,
            cancelCallback: null
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

            var instance = $modal.open(tempPlugDefaults);
            setOpened(instance, tempOptions);
            setRendered(instance, tempOptions);
            setResult(instance, tempOptions);
            spinnerSvc.showFullpage();
            return instance;
        }

        function setOpened(instance, tempOptions) {
            instance.opened.then(function () {

                if (tempOptions.openCallback) {
                    tempOptions.openCallback();
                }
            });
        }

        function setRendered(instance, tempOptions) {
            instance.rendered.then(function () {
                spinnerSvc.hideFullpage();
                if (tempOptions.renderCallback) {
                    tempOptions.renderCallback();
                }
            });
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