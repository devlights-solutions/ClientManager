(function() {

    var app = angular.module('app.util');

    app.factory('SpinnerSvc', [
        'usSpinnerService',
        '$rootScope',
        SpinnerSvc
    ]);

    function SpinnerSvc(usSpinnerService, $rootScope) {
        $rootScope.$$showOverlay = false;

        var service = {
            show: show,
            showFullpage: showFullpage,
            hide: hide,
            hideFullpage: hideFullpage

        };

        return service;

        function show(key) {
            usSpinnerService.spin(key);
        }

        function showFullpage() {
            $rootScope.$$showOverlay = true;
            show('spinner-fullpage');
        }

        function hide(key) {
            usSpinnerService.stop(key);
        }

        function hideFullpage() {
            $rootScope.$$showOverlay = false;
            hide('spinner-fullpage');
        }
    };
})()