(function () {

    var app = angular.module('app.util');

    app.factory('SmartTableSvc', [
        '$q',
        SmartTableSvc
    ]);

    function SmartTableSvc($modal, $templateCache, $timeout) {
        var SORT_DIRECTION = {
            ASC: 'ASC',
            DESC: 'DESC'
        };
        var service = {
            getGridParams: getGridParams,
            getSort: getSort,
            getPageNumber: getPageNumber,
            load: load
        };

        return service;

        function getGridParams(tableState) {
            return angular.extend({}, getSort(tableState), { page: getPageNumber(tableState) });
        }

        function getSort(tableState) {
            var sort = {};
            if (!tableState) return sort;

            if (tableState.sort.predicate) {
                sort.sortBy = tableState.sort.predicate;
            }
            if (tableState.sort.reverse) {
                sort.sortDirection = SORT_DIRECTION.DESC;
            } else if (tableState.sort.reverse === false) {
                sort.sortDirection = SORT_DIRECTION.ASC;
            }
            return sort;
        }

        function getPageNumber(tableState) {
            if (!tableState) return 1;

            var pagination = tableState.pagination;

            var start = pagination.start || 0;     // This is NOT the page number, but the index of item in the list that you want to use to display the table.
            var number = pagination.number || 10;  // Number of entries showed per page.

            return start > number ? Math.ceil(start / number) : 1;
        }

        function load(tableState, getFunction) {
            var deferred = $q.defer();

            getFunction({
                page: getPageNumber(tableState)
            }).then(function (data) {
                tableState.pagination.numberOfPages = data.pageCount;
                deferred.resolve({
                    data: data,
                    tableState: tableState
                });
            }).catch(function (response, statusCode) {
                deferred.reject(response, statusCode);
            });

            return deferred.promise;

        }

    };
})()