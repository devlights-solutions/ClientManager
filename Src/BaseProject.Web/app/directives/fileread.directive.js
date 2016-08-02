(function () {
    'use strict';

    var app = angular.module('app.directives');

    app.directive('appFileRead', ['$timeout', FileReadDirective]);

    function FileReadController($scope, $element) {
        var vm = this;

        vm.onClick = function ($event) {
            $element.find('input').click();

            //$($event.currentTarget).next().click();
        };

        vm.cleanFile = function($event) {
            $element.find('input').val('');
            vm.fileRead = null;
            vm.fileName = null;
        };
        //vm.fileChange = function (changeEvent) {
        //    var reader = new FileReader();
        //    reader.onload = function(loadEvent) {
        //        $scope.$apply(function () {
        //            $scope.fileread = loadEvent.target.result;
        //            $scope.filename = changeEvent.target.files[0].name;
        //        });
        //    }
        //    reader.readAsDataURL(changeEvent.target.files[0]);
        //};
    }

    function FileReadDirective($timeout) {
        // define constants and helpers used for the directive
        // ...
        return {
            restrict: 'EA', //E = element, A = attribute, C = class, M = comment      
            templateUrl: '/Scripts/app/directives/fileRead.html',
            controllerAs: 'uploadVm',
            controller: ['$scope', '$element', FileReadController],
            bindToController: true,
            scope: {
                //@ reads the attribute value, = provides two-way binding, & works with functions
                fileRead: "=",
                fileName: "=",
                urlView: '@',
                showViewButton: '&',
                accept: '@',
                ngDisabled: '&'
            },
            link: function (scope, element, attributes) {
                element.find('input').bind("change", function(changeEvent) {
                    var reader = new FileReader();
                    reader.onload = function(loadEvent) {
                        scope.$apply(function() {
                            scope.uploadVm.fileRead = loadEvent.target.result;
                            scope.uploadVm.fileName = changeEvent.target.files[0].name;
                        });
                    }
                    reader.readAsDataURL(changeEvent.target.files[0]);
                });
            }
        };
    }
})();

//(function () {
//    var app = angular.module('obras.util');

//    //configFactory.$inject = ['$http', 'logger'];
//    app.directive('fileread', [
//        fileReadDirective
//    ]);


//    function fileReadDirective() {
//        return {
//            restrict: 'E',
//            scope: {
//                fileread: "=",
//                filename: "="
//            },
//            controller: function() {
                
//            },
           
//            link: function (scope, element, attributes) {
//                element.bind("change", function (changeEvent) {
//                    var reader = new FileReader();
//                    reader.onload = function (loadEvent) {
//                        scope.$apply(function () {
//                            scope.fileread = loadEvent.target.result;
//                            scope.filename = changeEvent.target.files[0].name;
//                        });
//                    }
//                    reader.readAsDataURL(changeEvent.target.files[0]);
//                });
//            }
//        }
//    }

//    //app.directive('filename', [
//    //    fileNameDirective
//    //]);


//    //function fileNameDirective() {
//    //    return {
//    //        scope: {
//    //            filename: "="
//    //        },
//    //        link: function (scope, element, attributes) {
//    //            element.bind("change", function (changeEvent) {
//    //                //scope.$apply(function () {
//    //                    scope.filename = changeEvent.target.files[0].name;
//    //                //});
//    //            });
//    //        }
//    //    }
//    //}

//})();