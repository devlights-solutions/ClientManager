(function () {
    var app = angular.module('app.directives');

    //configFactory.$inject = ['$http', 'logger'];
    app.directive('tableNavigation', [
        tableNavigation
    ]);

    function tableNavigation() {
        var optionsDefault = {
            keyDisabled: false,
            enterKeyNext: false
        };

        var inputs = 'input[type="text"],textarea';
        var directive = {
            restrict: 'A',
            link: link
        };
        var arrow = { left: 37, up: 38, right: 39, down: 40, enter: 13 };

        return directive;

        function moveLeft(input, td) {
            if (input.selectionStart !== 0) return null;
            var tr = td.closest('tr');
            var pos = td[0].cellIndex;

            var columns = $(tr).find('td');
            for (var i = pos - 1; i >= 0; i--) {
                var hasInput = $(columns[i]).find(inputs).length;
                if (hasInput)
                    return $(columns[i]);
            }

            return null;
        }

        function moveRight(input, td) {
            if (input.selectionEnd !== input.value.length) return null;
            var tr = td.closest('tr');
            var pos = td[0].cellIndex;

            var columns = $(tr).find('td');
            for (var i = pos + 1; i < columns.length; i++) {
                var hasInput = $(columns[i]).find(inputs).length;
                if (hasInput)
                    return $(columns[i]);
            }

            return null;
        }

        function moveDown(input, td) {
            var tr = td.closest('tr');
            var pos = td[0].cellIndex;
            var moveToRow = tr.next('tr');

            if (moveToRow.length) {
                return $(moveToRow[0].cells[pos]);
            }

            return null;
        }

        function moveUp(input, td) {
            var tr = td.closest('tr');
            var pos = td[0].cellIndex;
            var moveToRow = tr.prev('tr');

            if (moveToRow.length) {
                return $(moveToRow[0].cells[pos]);
            }

            return null;
        }

        function handleNavigation(e, options) {
            // shortcut for key other than arrow keys
            if ($.inArray(e.which, [arrow.left, arrow.up, arrow.right, arrow.down, arrow.enter]) < 0) {
                return;
            }

            var input = e.target;
            var td = $(e.target).closest('td');
            var moveTo = null;

            switch (e.which) {
                case arrow.left:
                    moveTo = moveLeft(input, td);
                    break;
                case arrow.right:
                    moveTo = moveRight(input, td);
                    break;
                case arrow.up:
                    moveTo = moveUp(input, td);
                    break;
                case arrow.down:
                    moveTo = moveDown(input, td);
                    break;
                case arrow.enter:
                    if (options.enterKeyNext) {
                        moveTo = moveUp(input, td);
                    } else {
                        moveTo = moveDown(input, td);
                    }
                    break;
            }

            if (moveTo && moveTo.length) {
                e.preventDefault();

                moveTo.find(inputs).each(function (i, input) {
                    input.focus();
                    input.select();
                });
            }
        }

        function link(scope, element, attrs, modelCtrl) {
            var options = angular.extend({}, optionsDefault, attrs);
            if (!options.keyDisabled) {
                element.on('keydown.tableNavigation', inputs, function (e) {
                    handleNavigation(e, options);
                });
            }
        }

    }
})()