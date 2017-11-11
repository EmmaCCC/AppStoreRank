/**
 * Created by hyl on 2015/9/24.
 */
angular.module('UI',[]).directive('ngCalendar',function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            $(element).datepicker({
                language: 'cn',
                changeYear:true,
                yearRange: "1900:2010",
                dateFormat: "yyyy-mm-dd",
                onSelect: function (date) {
                    scope.$apply(function () {
                        ngModel.$setViewValue(date);
                    });
                }
            });
        }
    };
});
