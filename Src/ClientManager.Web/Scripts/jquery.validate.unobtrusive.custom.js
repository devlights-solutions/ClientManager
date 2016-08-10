(function ($) {
    function getErrorElement(element) {

        var $element = $(element);
        var $formGroup = $element.closest('.form-group');

        //Buscar si es un Select2
        var uiSelect = $formGroup.find('.ui-select-container');
        if (uiSelect.length)
            return uiSelect;

        //Verificar si es un Datepicker (Editor Date )
        var datepicker = $formGroup.find('.form-group-date');
        if (datepicker.length)
            return $formGroup.find('.form-control');

        return $element;
    }

    $.validator.setDefaults({
        ignore: [],
        highlight: function (element, errorClass, validClass) {
            $(element).closest('.form-group').addClass('has-error').removeClass('has-success');
            $(element).trigger('highlated');
        },
        unhighlight: function (element, errorClass, validClass) {
            var $element = getErrorElement(element);
            $element.closest('.form-group').removeClass('has-error').addClass('has-success').find('.validation-icon').remove();
            $element.trigger('unhighlated');
        }
    });

    $.validator.addMethod("unique", function (value, element, params) {
        var prefix = params;
        var selector = jQuery.validator.format("[name!='{0}'][name$='{1}'][data-val-unique-uniquesufix='{1}']", element.name, prefix);
        var matches = new Array();
        $(selector, $(element).closest('form')).each(function (index, item) {
            if (value == $(item).val()) {
                matches.push(item);
            }
        });

        return matches.length == 0;
    });
    $.validator.unobtrusive.adapters.addSingleVal("unique", "uniquesufix");
    

    $.validator.unobtrusive.adapters.add(
        'notequalto', ['other'], function (options) {
            options.rules['notEqualTo'] = '#' + options.params.other;
            if (options.message) {
                options.messages['notEqualTo'] = options.message;
            }
        });

    $.validator.addMethod('notEqualTo', function (value, element, param) {
        return this.optional(element) || value != $(param).val();
    }, '');
    

    $.validator.unobtrusive.adapters.add(
        'notequaltovalue', ['value'], function (options) {
            options.rules['notEqualToValue'] = '#' + options.params.value;
            if (options.message) {
                options.messages['notEqualToValue'] = options.message;
            }
        });

    $.validator.addMethod('notEqualToValue', function (value, element, param) {
        return this.optional(element) || value != $(param).val();
    }, '');

    // definition for the isdateafter validation rule
    $.validator.addMethod('isdateafter', function (value, element, params) {
        value = Globalize.parseDate(value);
        var otherDate = Globalize.parseDate($(params.compareTo).val());
        
        if (!value || !otherDate)
            return true;

        return value > otherDate || (value.getTime() === otherDate.getTime() && params.allowEqualDates);
    });

    $.validator.unobtrusive.adapters.add('isdateafter', ['propertytested', 'allowequaldates'], function (options) {
        options.rules['isdateafter'] = {
            'allowEqualDates': options.params['allowequaldates'],
            'compareTo': '#' + options.params['propertytested']
        };
        options.messages['isdateafter'] = options.message;
    });

    $.validator.addMethod('requiredif', function (value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];
        // get the target value (as a string, 
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];
        targetvalue = (targetvalue == null ? '' : targetvalue).toString();

        var targetvaluearray = targetvalue.split('|');

        for (var i = 0; i < targetvaluearray.length; i++) {

            // get the actual value of the target control
            // note - this probably needs to cater for more 
            // control types, e.g. radios
            var control = $(id);
            var controltype = control.attr('type');
            var actualvalue =
                controltype === 'checkbox' ?
                    control.attr('checked') ? "true" : "false" :
                    control.val();

            // if the condition is true, reuse the existing 
            // required field validator functionality
            if (targetvaluearray[i] === actualvalue) {
                return $.validator.methods.required.call(this, value, element, parameters);
            }
        }

        return true;
    });

    $.validator.unobtrusive.adapters.add('requiredif', ['dependentproperty', 'targetvalue'], function (options) {
        options.rules['requiredif'] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue']
        };
        options.messages['requiredif'] = options.message;
    });

})(jQuery)
