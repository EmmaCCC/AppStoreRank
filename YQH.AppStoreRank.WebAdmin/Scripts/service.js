

function HttpInterceptor($q) {
    return {
        request: function (config) {
            console.log('request');
            return config;
        },
        requestError: function (err) {
            return $q.reject(err);
        },
        response: function (res) {
            console.log('response');
            console.log(res.data);
            return res;
        },
        responseError: function (err) {
            console.log('err',err);
            if (-1 === err.status) {
               
            } else if (500 === err.status) {
                alert('错误');
            } else if (501 === err.status) {
              
            }
            return $q.reject(err);
        }
    };
}

angular.module('Service', [])

.factory('HttpInterceptor', ['$q', HttpInterceptor])

.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(HttpInterceptor);
}])
.filter("time", function () {
    return function (input, format) {
        var date = new Date(input);
        return date.pattern(format);

    }
}).factory('TaskInfoService', function ($http) {
    return {
        save: function (data) {
            var http = $http.post('/api/TaskInfo', data);
            return http;
        },
        applist: function (querystring) {
            var http = $http.get('/api/TaskInfo' + querystring);
            return http;
        },
        detail: function (querystring) {
            var http = $http.get('/api/TaskInfo' + querystring);
            return http;
        },
        list: function (querystring) {
            var http = $http.get('/api/TaskInfo' + querystring);
            return http;
        },
        complete: function (querystring) {
            var http = $http.delete('/api/TaskInfo' + querystring);
            return http;
        }
    }
})
.factory('EnumService', function ($http) {
    return {
        getEnumList: function (name) {
            var http = $http.get('/api/Enum?name=' + name);
            return http;
        }
    };
})
.factory('AccountService', function ($http) {
    return {
        login: function (data) {
            var http = $http.post('/api/Account/Login', data);
            return http;
        },
        changePwd: function (data) {
            var http = $http.post('/api/Account/ChangePassword', data);
            return http;
        },
        save: function (data) {
            var http = $http.post('/api/Account', data);
            return http;
        },
        getAccountInfo: function (id) {
            var http = $http.get('/api/Account?id=' + id);
            return http;
        },
        getPageList: function (pageIndex, pageSize, type, name) {
            var http = $http.get('/api/Account?pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&type=' + type + '&name=' + name);
            return http;
        },
        setDisabled: function (data) {
            var http = $http.post('/api/Account/Disabled', data);
            return http;
        },
        editmoney: function (data) {
            var http = $http.post('/api/Account/EditMoney', data);
            return http;
        }
    };
})
.factory('OrderInfoService', function ($http) {
    return {
        list: function (querystring) {
            var http = $http.get('/api/OrderInfo' + querystring);
            return http;

        }
    };
})
.factory('OperationService', function ($http) {
    return {
        list: function (querystring) {
            var http = $http.get('/api/Operation' + querystring);
            return http;
        }
    };
})
.factory('WithdrawService', function ($http) {
    return {
        list: function (querystring) {
            var http = $http.get('/api/Withdraw' + querystring);
            return http;
        },
        editstatus: function (data) {
            var http = $http.put('/api/Withdraw', data);
            return http;
        }
    }
}).factory('ApplicationService', function ($http) {
    return {
        add: function (data) {
            var http = $http.post('/api/Application', data);
            return http;
        },
        update: function (data) {
            var http = $http.put('/api/Application', data);
            return http;
        },
        delete: function (id) {
            var http = $http.delete('/api/Application?id=' + id);
            return http;
        },
        getApplicationInfo: function (id, isFromDatabase) {
            isFromDatabase = isFromDatabase || false;
            var http = $http.get('/api/Application?id=' + id + '&isFromDatabase=' + isFromDatabase);
            return http;
        },
        getPageList: function (pageIndex, pageSize, startTime, endTime, name) {
            var http = $http.get('/api/Application?pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&startTime=' + startTime + '&endTime=' + endTime + '&name=' + name);
            return http;
        },
        recovery: function (id) {
            var http = $http.get('/api/Application/Recovery?id=' + id);
            return http;
        }
    };
}).factory('CommentService', function ($http) {
    return {

        getPageList: function (pageIndex, pageSize, startTime, endTime, appId) {
            var http = $http.get('/api/Comment?pageIndex=' + pageIndex + '&pageSize=' + pageSize + '&startTime=' + startTime + '&endTime=' + endTime + '&appId=' + appId);
            return http;
        },
        delete: function (id) {
            var http = $http.delete('/api/Comment?id=' + id);
            return http;
        },
        batchDelete: function (ids) {
            var http = $http.post('/api/Comment/Batch', ids);
            return http;
        },
        conditionDelete: function (startTime, endTime, appId) {
            var http = $http.get('/api/Comment/Condition?startTime=' + startTime + '&endTime=' + endTime + '&appId=' + appId);
            return http;
        }
    };
})