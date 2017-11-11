
angular.module('app', ['Service', 'w5c.validator', 'UI', 'ngFileUpload'])
.config(["w5cValidatorProvider", function (w5cValidatorProvider) {

    // 全局配置
    w5cValidatorProvider.config({
        blurTrig: false,
        showError: true,
        removeError: true

    });
    w5cValidatorProvider.setRules({
        userName: {
            maxlength: "用户名长度不能大于{maxlength}个字符",
            minlength: "用户名长度不能小于{minlength}个字符",
        },
        password: {
            maxlength: "用户名长度不能大于{maxlength}个字符",
            minlength: "用户名长度不能小于{minlength}个字符",
        },
        phone: {
            pattern: "请输入正确的手机号"
        },
        withdrawPwd: {

            maxlength: "用户名长度不能大于{maxlength}个字符",
            minlength: "用户名长度不能小于{minlength}个字符",
        }


    });
}])

.controller('ctrl', ['$scope', 'AccountService', 'EnumService', 'Upload', function (sp, as, es) {

    var id = getUrlParam('id');

    sp.isUpdate = !!id;

    sp.accountType = [];

    sp.curUser = {

    }



    sp.defaultTour = {
        deadlineTime: '',
        marketPrice: '',
        number: '',
        pic: [],
        content: '',
        routeName: '',
        tripTime: '',
        regionTag: ''
    }


    sp.save = function () {
        var data = sp.curUser;
        if (id) {
            delete data.password;
        }
        console.log(data);

        as.save(data).success(function (data) {
            if (data.status === 0) {
                alert('操作成功');
                window.location.href = '/Account/List';
            } else {
                alert(data.message);
            }
        });


    }

    sp.setRandPwd = function() {
        sp.curUser.password = Math.random().toString(36).substr(2).slice(0, 6);
    }

    es.getEnumList('AccountType').success(function (data) {
        sp.accountType = data.result;
        sp.accountType.splice(0, 1);
    });

    init();
    function init() {
        if (id) {
            as.getAccountInfo(id).success(function(data) {
                if (data.status === 0) {
                    sp.curUser = data.result;
                } else {
                    alert(data.message);
                }
            });
        }
    }

}]);


