(function () {
    var app = angular.module('app.interceptors');

    //configFactory.$inject = ['$http', 'logger'];
    app.factory('LoadingInterceptor', [
        '$q',
        'SpinnerSvc',
        LoadingInterceptor
    ]);

    function LoadingInterceptor($q, spinnerSvc) {
        var service = {
            request: request,
            requestError: requestError,
            response: response,
            responseError: responseError
        };
        return service;

        ////////////
        // optional method
        function request(config) {
            if (config && config.hideLoading) return config;

            switch (config.method) {
                case 'POST':
                case 'PUT':
                case 'DELETE':
                    spinnerSvc.showFullpage();
            default:
            }
            
            return config;
        }

        function requestError(rejection) {
            var config = rejection ? rejection.config : {};
            if (config.hideLoading) return $q.reject(rejection);

            switch (config.method) {
                case 'POST':
                case 'PUT':
                case 'DELETE':
                    spinnerSvc.hideFullpage();
                default:
            }

            return $q.reject(rejection);
        }

        function response(response) {
            var config = response ? response.config : {};
            if (config.hideLoading) return response;

            switch (config.method) {
                case 'POST':
                case 'PUT':
                case 'DELETE':
                    spinnerSvc.hideFullpage();
                default:
            }

            return response;
        }

        function responseError(rejection) {
            var config = rejection ? rejection.config : {};
            if (config.hideLoading) return $q.reject(rejection);

            switch (config.method) {
                case 'POST':
                case 'PUT':
                case 'DELETE':
                    spinnerSvc.hideFullpage();
                default:
            }

            return $q.reject(rejection);
        }
    }
})()