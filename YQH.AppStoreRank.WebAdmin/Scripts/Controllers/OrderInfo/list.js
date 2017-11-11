var app = angular.module('app', ['Service', 'UI.Paging', 'UI']);
app.controller('ctl', ['$scope', '$http', 'OrderInfoService', function ($scope, $http, OrderInfoService) {
    $scope.starttime = "";
    $scope.endtime = "";
    $scope.ResultList = {};
    $scope.username = "";
    $scope.status = "-1";
    $scope.appname = "";

    $scope.pageIndex = 1;
    $scope.pageChange = function (i) {
        $scope.pageIndex = i;
        Init(i);
    };

    Init();
    function Init() {
        var querystring = "?pageindex=" + $scope.pageIndex + "&pagesize=10&starttime=" + $scope.starttime + "&endtime=" + $scope.endtime + "&appname=" + $scope.appname + "&username=" + $scope.username + "&status=" + $scope.status;
        OrderInfoService.list(querystring).success(function (data) {
            console.log('myresponse');
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