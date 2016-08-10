(function(ng) {
    var app = angular.module('app.util');

    //configFactory.$inject = ['$http', 'logger'];
    app.directive('integerOnly', [
        integerDirective
    ]);

    app.directive('number', [
        '$locale',
        numberDirective
    ]);

    app.directive('decimalsOnly', [
        '$locale',
        decimalDirective
    ]);

    function integerDirective() {
        var directive = {
            require: 'ngModel',
            link: link
        };

        return directive;

        function link(scope, element, attrs, modelCtrl) {

            element.bind('focus', onFocus); // Event handler for the focus event.

            modelCtrl.$parsers.push(function(inputValue) {
                // this next if is necessary for when using ng-required on your input. 
                // In such cases, when a letter is typed first, this parser will be called
                // again, and the 2nd time, the value will be undefined
                if (inputValue == undefined) return '';

                var transformedInput;
                if (attrs.unsigned) {
                    transformedInput = inputValue.replace(/[^0-9]/g, '');
                } else {
                    transformedInput = inputValue.replace(/[^-?0-9]/g, '');
                }
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return parseInt(transformedInput, 10);
            });

            function onFocus() {
                var e = this;
                e.select();
            }
        }
    }

    function numberDirective($locale, undefined) {

        function compile(tElement, tAttrs) {

            if (tElement[0].nodeName !== 'INPUT') {
                throw ('Error. Debe utilizarse en un elemento <input>');
            }

            return function(scope, element, attrs, ngModelCtrl, undefined) {

                if (!ngModelCtrl) {
                    return;
                }

                var step, newValue;

                //Atributos
                var maxAttr = (attrs.hasOwnProperty('max') && attrs.max !== '') ? parseInt(attrs.max, 10) : false,
                    minAttr = (attrs.hasOwnProperty('min') && attrs.min !== '') ? parseInt(attrs.min, 10) : false,
                    stepAttr = (attrs.hasOwnProperty('step') && attrs.step !== '') ? parseInt(attrs.step, 10) : 1,
                    decimalsAttr = (attrs.hasOwnProperty('decimals') && attrs.decimals !== '') ? parseInt(attrs.decimals, 10) : 0;

                element.bind('focus', function() {
                    element.select();
                });

                function validateMinMax(newValue) {
                    if (maxAttr !== false && newValue > maxAttr) {
                        newValue = maxAttr;
                    } else if (minAttr !== false && newValue < minAttr) {
                        newValue = minAttr;
                    }
                    return newValue;
                }

                function formatValue(newValue) {

                    //Formateando decimales
                    if (decimalsAttr != null && decimalsAttr >= 0) {
                        var out = parseFloat(newValue, 10);
                        if (!isNaN(out)) {
                            newValue = out.toFixed(decimalsAttr);
                        }
                    }

                    newValue = String(newValue);
                    if ($locale.NUMBER_FORMATS.DECIMAL_SEP === ',') {
                        newValue = newValue.replace(/\.(\d*)$/, ',$1');
                    } else {
                        newValue = newValue.replace(/,(\d*)$/, '.$1');
                    }

                    ngModelCtrl.$setViewValue(newValue);
                    ngModelCtrl.$render();
                }

                element.on('keydown', function(event) {

                    // Arrow key incrementation
                    if (event.keyCode === 38 || event.keyCode === 40) {
                        event.preventDefault();
                        step = (event.shiftKey) ? (stepAttr * 10) : stepAttr;
                        if (event.keyCode === 40) // Arrow down
                        {
                            step *= -1;
                        }

                        newValue = (isNaN(ngModelCtrl.$modelValue)) ? step : ngModelCtrl.$modelValue + step;

                        newValue = validateMinMax(newValue);

                        formatValue(newValue);

                        element.select();
                        event.preventDefault();
                    }

                    newValue = (isNaN(ngModelCtrl.$modelValue)) ? 0 : ngModelCtrl.$modelValue;
                    newValue = String(newValue);

                    var findsDot = new RegExp(/\./g);
                    var containsDot = newValue.match(findsDot);
                    var findsComma = new RegExp(/\,/g);
                    var containsComma = newValue.match(findsComma);

                    if ((containsDot != null || containsComma != null) && ([46, 110, 188, 190].indexOf(event.which) > -1)) {
                        event.preventDefault();
                        //return false;
                    }


                }); // end on keydown

                ngModelCtrl.$parsers.push(function(value) {
                    //convert data from view format to model format
                    var out = parseFloat(value, 10);
                    if (isNaN(out)) {
                        return null;
                    }
                    return out;
                });

                ngModelCtrl.$parsers.unshift(function(value) {
                    //value = value.replace(/\./g, '').replace(',', '.');

                    if (typeof value !== 'string' || value === '') {
                        return null;
                    }
                    value = String(value);
                    value = value.replace(/,(\d*)$/, '.$1');
                    var out = parseFloat(value, 10);

                    if (isNaN(out)) {
                        return undefined;
                    }

                    //Formateando decimales
                    if (decimalsAttr != null && decimalsAttr >= 0) {
                        out = out.toFixed(decimalsAttr);
                    }

                    out = validateMinMax(out);

                    if (decimalsAttr != null && decimalsAttr >= 0) {
                        out = parseFloat(value, 10);
                        out = out.toFixed(decimalsAttr);
                    }
                    //formatValue(out);
                    return out;
                }); // end $parser

                ngModelCtrl.$formatters.unshift(function(value) {
                    //mensaje('$formatters 1 value = ' + value);

                    if (typeof value !== 'string') {
                        return value;
                    }

                    if (isNaN(parseFloat(value, 10))) {
                        return '';
                    }

                    if ($locale.NUMBER_FORMATS.DECIMAL_SEP === ',') {
                        return value.replace(/\.(\d*)$/, ',$1');
                    }
                    var valor = value.replace(/,(\d*)$/, '.$1');

                    mensaje('$formatters 2 valor = ' + valor);

                    return 0;

                }); // end $formatter

                ngModelCtrl.$formatters.push(function(value) {
                    //convert data from model format to view format
                    return formatValue(value);
                });

                //ngModelCtrl.$validators.number = function (modelValue, viewValue) {
                //    mensaje('$validators modelValue = ' + modelValue);
                //    mensaje('$validators viewValue = ' + viewValue);
                //    if (modelValue === undefined || modelValue === null || modelValue === '') {
                //        return true;
                //    }
                //    if (isNaN(modelValue)) {
                //        return false;
                //    }
                //    return true;
                //}; // end $validator number


                //ngModelCtrl.$validators.range = function (modelValue, viewValue) {
                //    mensaje('$validators range');
                //    if ((maxAttr && modelValue > maxAttr) || (minAttr && modelValue < minAttr)) {
                //        return false;
                //    }
                //    return true;
                //}; // end $validator range


            }; // end link function


        }

        function link(scope, element, attrs, ngModel) {
            scope.$watch(attrs.ngModel, function(newValue, oldValue) {
                var spiltArray = String(newValue).split('');

                //if (attrs.allowNegative == "false") {
                //    if (spiltArray[0] == '-') {
                //        newValue = newValue.replace("-", "");
                //        ngModel.$setViewValue(newValue);
                //        ngModel.$render();
                //    }
                //}

                if (attrs.allowDecimal == "false") {
                    newValue = parseInt(newValue);
                    ngModel.$setViewValue(newValue);
                    ngModel.$render();
                }


                if (attrs.allowDecimal != "false") {
                    if (attrs.decimalUpto) {
                        var n = String(newValue).split(".");
                        if (n[1]) {
                            var n2 = n[1].slice(0, attrs.decimalUpto);
                            newValue = [n[0], n2].join(".");
                            ngModel.$setViewValue(newValue);
                            ngModel.$render();
                        }
                    }
                }


                if (spiltArray.length === 0) return;
                if (spiltArray.length === 1 && (spiltArray[0] == '-' || spiltArray[0] === '.')) return;
                if (spiltArray.length === 2 && newValue === '-.') return;

                /*Check it is number or not.*/
                if (isNaN(newValue)) {
                    ngModel.$setViewValue(oldValue);
                    ngModel.$render();
                }
            });


        }

        var directive = {
            restrict: 'A',
            require: 'ngModel',
            compile: compile
        };

        return directive;

    }

    function decimalDirective($locale, undefined) {
        // Usage:
        //     <input type="text" decimals="3" decimal-min="-20" decimal-max="40" formatting="false" ></input>
        // Creates:
        // 
        var directive = {
            link: link,
            require: 'ngModel',
            restrict: 'A'
        };
        return directive;


        function link(scope, el, attrs, ngModelCtrl) {
            var decimalSeparator = $locale.NUMBER_FORMATS.DECIMAL_SEP;
            var groupSeparator = $locale.NUMBER_FORMATS.GROUP_SEP;

            // Create new regular expression with current decimal separator.
            var NUMBER_REGEXP = "^\\s*(\\-|\\+)?(\\d+|(\\d*(\\.\\d*)))\\s*$";
            var regex = new RegExp(NUMBER_REGEXP);

            var formatting = true;
            var maxInputLength = 16; // Maximum input length. Default max ECMA script.
            var max; // Maximum value. Default undefined.
            var min; // Minimum value. Default undefined.
            var decimals = 2; // Number of decimals. Default 2.
            var lastValidValue; // Last valid value.

            // Create parsers and formatters.
            ngModelCtrl.$parsers.push(parseViewValue);
            ngModelCtrl.$parsers.push(minValidator);
            ngModelCtrl.$parsers.push(maxValidator);
            ngModelCtrl.$formatters.push(formatViewValue);

            el.bind('blur', onBlur); // Event handler for the leave event.
            el.bind('focus', onFocus); // Event handler for the focus event.

            // Put a watch on the min, max and decimal value changes in the attribute.
            scope.$watch(attrs.decimalMin, onMinChanged);
            scope.$watch(attrs.decimalMax, onMaxChanged);
            scope.$watch(attrs.decimals, onDecimalsChanged);
            //scope.$watch(attrs.formatting, onFormattingChanged);

            //Default value decimals
            if (!angular.isUndefined(attrs.decimals)) {
                decimals = parseFloat(attrs.decimals);
            }

            // Setup decimal formatting.
            if (decimals > -1) {
                ngModelCtrl.$parsers.push(function(value) {
                    return (value) ? round(value) : value;
                });
                ngModelCtrl.$formatters.push(function(value) {
                    return (value) ? formatPrecision(value) : value;
                });
            }

            function onMinChanged(value) {
                if (!angular.isUndefined(value)) {
                    min = parseFloat(value);
                    lastValidValue = minValidator(ngModelCtrl.$modelValue);
                    ngModelCtrl.$setViewValue(formatPrecision(lastValidValue));
                    ngModelCtrl.$render();
                }
            }

            function onMaxChanged(value) {
                if (!angular.isUndefined(value)) {
                    max = parseFloat(value);
                    maxInputLength = calculateMaxLength(max);
                    lastValidValue = maxValidator(ngModelCtrl.$modelValue);
                    ngModelCtrl.$setViewValue(formatPrecision(lastValidValue));
                    ngModelCtrl.$render();
                }
            }

            function onDecimalsChanged(value) {
                if (!angular.isUndefined(value)) {
                    decimals = parseFloat(value);
                    maxInputLength = calculateMaxLength(max);
                    if (lastValidValue !== undefined) {
                        ngModelCtrl.$setViewValue(formatPrecision(lastValidValue));
                        ngModelCtrl.$render();
                    }
                }
            }

            function onFormattingChanged(value) {
                if (!angular.isUndefined(value)) {
                    formatting = (value !== false);
                    ngModelCtrl.$setViewValue(formatPrecision(lastValidValue));
                    ngModelCtrl.$render();
                }
            }

            /**
             * Round the value to the closest decimal.
             */
            function round(value) {
                var d = Math.pow(10, decimals);
                return Math.round(value * d) / d;
            }

            /**
             * Format a number with the thousand group separator.
             */
            function numberWithCommas(value) {
                if (formatting) {
                    var parts = value.toString().split(decimalSeparator);
                    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, groupSeparator);
                    return parts.join(decimalSeparator);
                } else {
                    // No formatting applies.
                    return value;
                }
            }

            /**
             * Format a value with thousand group separator and correct decimal char.
             */
            function formatPrecision(value) {
                if (!(value || value === 0)) {
                    return '';
                }
                var formattedValue = parseFloat(value).toFixed(decimals);
                formattedValue = formattedValue.replace('.', decimalSeparator);
                return numberWithCommas(formattedValue);
            }

            function formatViewValue(value) {
                //console.log('formatViewValue value=' + value);
                return ngModelCtrl.$isEmpty(value) ? '' : '' + value;
            }

            /**
             * Parse the view value.
             */
            function parseViewValue(value) {
                if (angular.isUndefined(value)) {
                    value = '';
                }
                value = value.toString().replace(decimalSeparator, '.');

                // Handle leading decimal point, like ".5"
                if (value.indexOf('.') === 0) {
                    value = '0' + value;
                }

                // Allow "-" inputs only when min < 0
                if (value.indexOf('-') === 0) {
                    if (min >= 0) {
                        value = null;
                        ngModelCtrl.$setViewValue(formatViewValue(lastValidValue));
                        ngModelCtrl.$render();
                    } else if (value === '-') {
                        value = '';
                    }
                }

                var empty = ngModelCtrl.$isEmpty(value);
                if (empty) {
                    lastValidValue = '';
                    //ngModelCtrl.$modelValue = undefined;
                } else {
                    if (regex.test(value) && (value.length <= maxInputLength)) {
                        if (value > max) {
                            lastValidValue = max;
                        } else if (value < min) {
                            lastValidValue = min;
                        } else {
                            lastValidValue = (value === '') ? null : parseFloat(value);
                        }
                    } else {
                        // Render the last valid input in the field
                        ngModelCtrl.$setViewValue(formatViewValue(lastValidValue));
                        ngModelCtrl.$render();
                    }
                }

                return lastValidValue;
            }

            /**
             * Calculate the maximum input length in characters.
             * If no maximum the input will be limited to 16; the maximum ECMA script int.
             */
            function calculateMaxLength(value) {
                var length = 16;
                if (!angular.isUndefined(value)) {
                    length = Math.floor(value).toString().length;
                }
                if (decimals > 0) {
                    // Add extra length for the decimals plus one for the decimal separator.
                    length += decimals + 1;
                }
                if (min < 0) {
                    // Add extra length for the - sign.
                    length++;
                }
                return length;
            }

            /**
             * Minimum value validator.
             */
            function minValidator(value) {
                if (!angular.isUndefined(min)) {
                    if (!ngModelCtrl.$isEmpty(value) && (value < min)) {
                        return min;
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }

            /**
             * Maximum value validator.
             */
            function maxValidator(value) {
                if (!angular.isUndefined(max)) {
                    if (!ngModelCtrl.$isEmpty(value) && (value > max)) {
                        return max;
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }


            /**
             * Function for handeling the blur (leave) event on the control.
             */
            function onBlur() {
                var value = ngModelCtrl.$modelValue;
                if (!angular.isUndefined(value)) {
                    // Format the model value.
                    ngModelCtrl.$viewValue = formatPrecision(value);
                    ngModelCtrl.$render();
                }
            }


            /**
             * Function for handeling the focus (enter) event on the control.
             * On focus show the value without the group separators.
             */
            function onFocus() {
                var e = this;
                var value = ngModelCtrl.$modelValue;
                if (!angular.isUndefined(value) && value != null) {
                    ngModelCtrl.$viewValue = value.toString().replace(".", decimalSeparator);
                    ngModelCtrl.$render();
                }
                e.select();
            }
        }
    }


})(angular)