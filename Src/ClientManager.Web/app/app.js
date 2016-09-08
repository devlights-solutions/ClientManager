(function () {
    'use strict';

    var app = angular.module('app', [
        'app.core', 'app.client', 'app.project', 'app.payment', 'app.timeRecord', 'mwl.calendar'
    ])

    app.config(['$httpProvider', 'uiSelectConfig', 'usSpinnerConfigProvider',
        function ($httpProvider, uiSelectConfig, usSpinnerConfigProvider) {
            $httpProvider.interceptors.push('LoadingInterceptor');

            //ui-select
            uiSelectConfig.theme = 'select2';

            var fullpage = {
                lines: 11,
                length: 40,
                radius: 42,
                opacity: 0.6,
                width: 10,
                scale: 1.5,
                color: '#209e91'
            };

            usSpinnerConfigProvider.setTheme('fullpage', fullpage);
        }
    ]);
})();
