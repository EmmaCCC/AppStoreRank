
angular.module('app', ['Service', 'UI.Paging', 'UI'])


.controller('ctrl', ['$scope', 'AccountService', 'EnumService', function ($scope, as, es) {

    $scope.accountType = [];
    $scope.startTime = "";
    $scope.endTime = "";
    $scope.type = -1;
    $scope.name = "";

    $scope.pageIndex = 1;
    $scope.pageSize = 10;
    $scope.totalCount = 0;
    $scope.pageCount = 0;

    $scope.resultList = [];

    $scope.model = {
        id: '',
        status: '1',
        remark: ''
    }


    init();

    $scope.search = function () {
        $scope.pageIndex = 1;
        init();

    }

    $scope.pageChange = function (pageIndex) {
        $scope.pageIndex = pageIndex;
        init();
    }


    es.getEnumList('AccountType').success(function (data) {
        $scope.accountType = data.result;

    });

    $scope.setDisabled = function (id, isDisabled, type) {
        if (type === 0 && isDisabled) {
            if (!confirm('该账号是管理员账号，确定禁用?')) {
                return;
            }
        }
        var data = {
            id: id,
            isDisabled: isDisabled
        }
        as.setDisabled(data).success(function (data) {
            if (data.status === 0) {
                init();
            } else {
                alert(data.message);
            }
        });
    }

    function init() {
        as.getPageList($scope.pageIndex, $scope.pageSize, $scope.type, $scope.name)
           .success(function (data) {
               if (data.status === 0) {
                   $scope.resultList = data.result.list;
                   $scope.totalCount = data.result.totalCount;
                   $scope.pageCount = data.result.pageCount;
               } else {
                   alert(data.message);
               }
           });
    }

    $scope.ChangeMoney = function (id) {
        $scope.currentId = id;
        $scope.temptype = "true";
        $scope.tempmoney = "";
        $("#modaldialog").trigger("click");
    }


    $scope.confirmEdit = function () {
        if (!$scope.tempmoney || isNaN($scope.tempmoney)) {
            alert("金额输入有误！");
            return;
        }

        if (confirm("确定要提交更改吗？")) {
            $scope.postdata = {};
            $scope.postdata.money = $scope.tempmoney;
            $scope.postdata.type = $scope.temptype;
            $scope.postdata.id = $scope.currentId;
            as.editmoney($scope.postdata).success(function (data) {
                if (data.status === 0) {
                    $("#closeBtn").trigger("click");
                    init();
                }
                alert(data.message);
            });
        }
    }

}]);


