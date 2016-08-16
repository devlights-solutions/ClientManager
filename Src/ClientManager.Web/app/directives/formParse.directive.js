(function() {
    var app = angular.module('app.directives');

    //configFactory.$inject = ['$http', 'logger'];
    app.directive('formParse', [ '$timeout',
        formParseDirective
    ]);


    function formParseDirective($timeout) {
        var directive = {
            link: link
        };

        return directive;

        function parseForm(element) {
            var $form = $(element);
            $form.removeData("validator");
            $form.removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse($form);
            //controls.parse($form);

        }
        function link(scope, element, attrs, modelCtrl) {
            scope.$on('form.parse', function(e) {
                parseForm(element);
            });

            $timeout(function() {
                parseForm(element);

                element.on('submit', function (e) {
                    return false;
                });
            });
        }
    }


})();