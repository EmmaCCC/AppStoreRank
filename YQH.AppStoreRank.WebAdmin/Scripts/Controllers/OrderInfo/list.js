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
    $scope.del = function() {
        layer.confirm('您是如何看待前端开发？',function(index) {
            layer.close(index);
        },function() {
            console.log(2);
        });
    }

    Init();
    function Init() {
        var querystring = "?pageindex=" + $scope.pageIndex + "&pagesize=10&starttime=" + $scope.starttime + "&endtime=" + $scope.endtime + "&appname=" + $scope.appname + "&username=" + $scope.username + "&status=" + $scope.status;
        OrderInfoService.list(querystring).success(function (data) {
            layer.msg('网络异常，请稍后再试', { icon: $scope.pageIndex });
         
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