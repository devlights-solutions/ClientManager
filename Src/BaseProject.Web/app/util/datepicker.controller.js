(function () {

    var app = angular.module('app.util');
    app.controller('DatepickerCtrl', ['$scope', '$rootScope', DatepickerController]);
    app.controller('DatepickerMonthYearCtrl', ['$scope', '$rootScope', DatepickerMonthYearCtrl]);


    function DatepickerController($scope, $rootScope) {
        var vm = this;
        vm.$rootScope = $rootScope;
        vm._configDefault = {
            onOpen: null,
            onChange: null,
            minDate: null,
            isOpen: false,
            dateOptions: {
                showWeeks: false
            }
        };

        ///////////////////
        vm.$rootScope.$on('date.closeAll', function (e, vmTrigger) {
            if (vm != vmTrigger) {
                vm.onOpen(null, false);
            }
        });

    }

    DatepickerController.prototype = {
        init: function (config) {
            this.config = angular.extend({}, this._configDefault, config);
        },
        onOpen: function ($event, isOpen) {
            var toggleOpen = !this.config.isOpen;
            if (typeof isOpen !== "undefined") {
                toggleOpen = isOpen;
            }
            if ($event) {
                $event.preventDefault();
                $event.stopPropagation();
            }
            if (toggleOpen) {
                this.$rootScope.$broadcast('date.closeAll', this);
            }
            this.config.isOpen = toggleOpen;

            if (this.config.onOpen)
                this.config.onOpen($event);
        },

        onChange: function (fecha) {
            if (this.config.onChange)
                this.config.onChange(fecha);
        }
    };


    function DatepickerMonthYearCtrl($scope, $rootScope) {
        DatepickerController.call(this, $scope, $rootScope);
        var vm = this;

        vm.init = function (config) {
            config = config || {};
            config.dateOptions = config.dateOptions || {};
            config.dateOptions.minMode = 'month';
            config.dateOptions.maxMode = 'year';

            DatepickerController.prototype.init.call(this, config);
        }
    }

    DatepickerMonthYearCtrl.prototype = Object.create(DatepickerController.prototype);


})();