/**
 * Created by hyl on 2015/10/27.
 */

/**
 * 用于处理http请求响应
 * @param http angularjs的 $http的respose对像
 * @constructor
 */
function HttpHandler(http) {
    var _http = http;
    var _errorList = [{ status: 404, handler: error404 }, { status: 401, handler: error401 }, { status: 500, handler: error500 }, { status: -1, handler: errorDefautl }];
    var _success = null;
    var init = function (self) {
        self.$$state = _http.$$state;
        _http.error(onerror);
        _http.success(onsuccess);
    };

    this.then = function (onFulfilled, onRejected, progressBack) {
        _http.then(onFulfilled, onRejected, progressBack);
        return this;
    }
    this.catch = function (callback) {
        _http.catch(callback);
        return this;
    };

    this.finally = function (callback, progressBack) {
        _http.finally(callback, progressBack);
        return this;
    };

    /**
     * 处理一个http成功响应
     * @param fn  处理事件的handler
     * @returns {HttpHandler}
     */
    this.success = function (fn) {
        _success = fn;
        return this;
    };

    /**
     * 错误事件
     * @param fn 用于处理错误误的handler
     * @param status 所要拦截处理的错误代码
     * @returns {HttpHandler}
     */
    this.error = function (fn, status) {
        if (!status) {
            _errorList = [];
            _errorList.push({ status: -1, handler: fn });
        }
        else {
            removeHandler(_errorList, status);
            _errorList.push({ status: status, handler: fn });
        }
        return this;
    };

    function removeHandler(array, status) {
        for (var i = 0; i < array.length; i++) {
            if (status === array[i].status) {
                array.splice(i, 1);
            }
        }
    };

    /**
     * 接管success事件 如果status代码不为0和1用error事件替换 如果没有 status代码不处理
     * @param data
     * @param status
     * @param headers
     * @param config
     */
    function onsuccess(data, status, headers, config) {
        if (data.status) {
            if (data.status === 0 || data.status === 1)
                _success(data, status, headers, config);
            else
                onerror(data, data.status, headers, config);
        }
        else {
            _success(data, status, headers, config);
        }
    }

    function error404(data, status, headers, config) {
        alert('找不到该页!'); 
        //location.href = "/Home/Error";
    };

    function error401(data, status, headers, config) {
        alert('没有权限'); 
        //location.href = "/User/Login?url="+encodeURIComponent(window.location.href);
    };

    function error500(data, status, headers, config) {
        alert('网络异常，请刷新后重试！');
        //location.href = "/Home/Error";
    };

    function errorDefautl(data, status, headers, config) {
        //location.href = "/Home/Error";
    };

    function onerror(data, status, headers, config) {
        var defErr;
        for (var i = 0; i < _errorList.length; i++) {
            if (status === _errorList[i].status) {
                _errorList[i].handler(data, status, headers, config);
                return;
            }
            else if (_errorList[i].status === -1)
                defErr = _errorList[i];
        }
        if (defErr)
            defErr.handler(data, status, headers, config);
    };
    init(this);
}