angular.module('UI.Paging', []).directive('paging', function () {
    return {
        restrict: 'EA',
        replace: true,
        scope: {
            pageCount: '=',
            pageChange: '='
        },
        template: '<div> \
                            <a href="javascript:void(0)" ng-class="{Disable : page <= 1 }" ng-click="SelectPage(page-1)" >&lt;</a> \
                            <a href="javascript:void(0)" ng-if="before > 0" ng-click="SelectPage(before - 1)" >...</a> \
                            <a href="javascript:void(0)" ng-repeat="m in pages" ng-class="{Active : m.IsActive}" ng-click="SelectPage(m.Index)">{{m.Index}}</a> \
                            <a href="javascript:void(0)" ng-if="pageCount > (before + 10)" ng-click="SelectPage(before + 11)" >...</a> \
                            <a href="javascript:void(0)" ng-class="{Disable : page >= pageCount }" ng-click="SelectPage(page+1)" >&gt;</a> \
                            <div/>',
        link: function (scope, iElement, iAttrs) {
            scope.page = 1;
            scope.$watch('page', function () {
                scope.Init();
            });
            scope.$watch('pageCount', function () {
                scope.Init();
            });
            scope.Init = function ()
            {
                scope.pages = [];
                if (scope.pageCount < 10) {
                    for (var i = 1; i <= scope.pageCount; i++) {
                        var p = {};
                        p.Index = i;
                        p.IsActive = i == scope.page ? true : false;
                        scope.pages.push(p);
                    }
                }
                else {
                    scope.before = scope.page - (scope.page % 10);
                    scope.after = scope.pageCount > (scope.before + 10)
                    for (var i = scope.before + 1; i <= scope.before + 10; i++) {
                        if (i > scope.pageCount) {
                            break;
                        }
                        var p = {};
                        p.Index = i;
                        p.IsActive = i == scope.page ? true : false;
                        scope.pages.push(p);
                    }
                }
            }

            scope.SelectPage = function (index) {
                //scope.$apply(function () {
                if (index > 0 && index <= scope.pageCount) {
                    scope.page = index;
                    scope.pageChange(index);
                }
                //});
            };


        }
    };
});