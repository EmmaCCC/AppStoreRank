var app = angular.module('app', ['Service', 'UI.Paging', 'UI']);
app.controller('ctl', ['$scope', '$http', 'OperationService', function ($scope, $http, OperationService) {
    $scope.starttime = "";
    $scope.endtime = "";
    $scope.ResultList = {};

    $scope.pageIndex = 1;
    $scope.pageChange = function (i) {
        $scope.pageIndex = i;
        Init(i);
    };

    Init();
    function Init() {
        var querystring = "?pageindex=" + $scope.pageIndex + "&pagesize=10&starttime=" + $scope.starttime + "&endtime=" + $scope.endtime;
        OperationService.list(querystring).success(function (data) {
            if (data.status === 0) {
                $scope.ResultList = data;
            }
            else {
                alert(data.message);
            }
        });
    };

    $scope.search = function () {
        $scope.pageIndex = 1;
        Init();
    };

}]);