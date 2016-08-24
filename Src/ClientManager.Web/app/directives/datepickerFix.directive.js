(function (ng) {
    var app = ng.module('app.directives');

    app.directive('datepickerFix', [
        DatepickerFixDirective
    ]);

    function DatepickerFixDirective(ngModel) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: link
        };

        function link(scope, element, attr, ngModelCtrl) {
            ngModelCtrl.$formatters.push(function (value) {
                if (value) {
                    return moment(value).toDate();
                }
                return value;
            })
        }
    }
})(angular)