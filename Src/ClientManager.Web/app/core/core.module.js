(function () {
    'use strict';

    var app = angular.module('app.core', [
        'ui.select',
        'ngSanitize', 
        'ui.bootstrap',
        'smart-table',
        'mwl.calendar',
        'angularSpinner',

        'app.interceptors',
        'app.services',
        'app.util',
        'app.directives'
    ]);

    
})();
