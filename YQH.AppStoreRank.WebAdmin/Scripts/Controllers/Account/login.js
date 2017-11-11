
angular.module('app', ['Service'])

.controller('loginCtrl', ['$scope', 'AccountService', function (sp, accountService) {

    sp.errMsg = '';
    sp.isLogining = false;
    sp.model = {
        username: '',
        password: ''
    }
    sp.login = function () {
        sp.isLogining = true;
        accountService.login(sp.model).success(function(data) {
            if (data.status === 0) {
                var returnUrl = getUrlParam('returnUrl');
                if (!returnUrl) {
                    returnUrl = '/Home/Index';
                }
                window.location.href = returnUrl;
            } else {
                sp.errMsg = data.message;
                sp.isLogining = false;
            }
        });
    }

}]);


