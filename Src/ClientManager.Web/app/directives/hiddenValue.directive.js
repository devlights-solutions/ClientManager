(function (ng) {
    var app = ng.module('app.directives');

    //configFactory.$inject = ['$http', 'logger'];
    app.directive('hiddenValue', [
        '$timeout',
        hiddenValueDirective
    ]);

    function hiddenValueDirective($timeout) {
        var directive = {
            require: 'ngModel',
            link: link
        };

        return directive;

        function link(scope, element, attrs, modelCtrl) {
            var directive = {
                scope: scope,
                element: element,
                attrs: attrs,
                modelCtrl: modelCtrl
            }

            if (attrs.valDate) {
                element.removeAttr('data-val-date');
            }

            $timeout(function() {
                if ($(element).is(':hidden') && attrs.validate) {
                    $(element).change(function() {
                        validElement(element, attrs);
                    });
                }
            });

            updateValue(element, modelCtrl.$modelValue, attrs);
            scope.$watch(function () {
                return directive.modelCtrl.$modelValue;
            }, function (newValue) {
                updateValue(directive.element, newValue, directive.attrs);
            }, true);


        }

        function updateValue(element, newValue, attrs) {
            var value = newValue;
            if (attrs.isDate == "True" && value) {
                value = moment(newValue).format();
            }
            if (!value && !((typeof value) == 'boolean')) {
                value = '';
            }
            //if (value == null || isNaN(value)) {
            //    value = '';
            //}
            element.val(value);
            if ($(element).is(':hidden') && attrs.validate) {
                $(element).change();
            }
        }

        function validElement(element, attrs) {
            if (!attrs.validate) return;

            var $form = $(element).closest('form');
            if ($form) {
                $form.validate().element($(element));
            }
        }
    }
})(angular)