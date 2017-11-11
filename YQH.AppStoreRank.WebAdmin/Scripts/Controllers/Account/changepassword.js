var app = angular.module('app', ['Service']);
app.controller('ctl', ['$scope', '$http', 'AccountService', function ($scope, $http, accountService) {
    $scope.oldpaassword = "";
    $scope.newpassword01 = "";
    $scope.newpassword02 = "";

    $scope.IsChange = function (id) {
        if (confirm("确定修改密码吗？")) {
            $scope.postData = {};
            $scope.postData.old =$scope.oldpaassword ;
            $scope.postData.new1 = $scope.newpassword01;
            $scope.postData.new2 = $scope.newpassword02; 
            accountService.changePwd($scope.postData).success(function (data) {
                if (data.status === 0) {
                    $scope.oldpaassword = "";
                    $scope.newpassword01 = "";
                    $scope.newpassword02 = "";
                    alert('修改成功');
                } else {
                    alert(data.message);
                }
                
            });
        }
    }
    $scope.add = function () {
       
       
        if (!$scope.oldpaassword) {
            alert("请填写原始密码！")
            return;
        }  

        if (!$scope.newpassword01) {
            alert("请填写新密码！")
            return;
        } 

        if (!$scope.newpassword02) {
            alert("请再次输入新密码！")
            return;
        }
        if ($scope.newpassword02!== $scope.newpassword01) {
            alert("两次密码不同，请重新输入")
            return;
        }
        $scope.IsChange();
    }
}]);
