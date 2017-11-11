function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return '';
}

function getUrlParamNative(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURIComponent(r[2]); return '';
}


function navBack() {
    
    history.back(-1);

}

function sourceRedirect(url) {
    var urlPrefix = getUrlParam("sourceId");
    if (urlPrefix != "")
    {
        if (url.indexOf("?") > 0)
            url += "&sourceId=" + urlPrefix;
        else
            url += "?sourceId=" + urlPrefix;
    }

    location.href = url;
}


function getRouteId() {

    return getUrlParam('id');
    //var url = window.location.href
    //, id = '';
    //var index = url.lastIndexOf('/');

    //if (index != -1) {
    //    id = url.substring(index + 1, url.length);
    //}
    //var end = id.lastIndexOf('#');
    //if (end != -1) {
    //    id = id.substring(0, end);
    //}
    //return id;
}

function processErrMsg(data) {
    if (data.status) {
        if (data.status === 1 )
            return data.message;
        return "系统繁忙";
    } else {
        return "系统繁忙";
    }
}




function setItem(key, data) {
    if (window.localStorage) {
        window.localStorage.setItem(key, data);
    } else {
        alert('您的浏览器版本过于老旧，请更换新的浏览器');
    }
}
function getItem(key) {
    if (window.localStorage) {
        return window.localStorage.getItem(key);
    } else {
        alert('您的浏览器版本过于老旧，请更换新的浏览器');
    }
}




function updateNewReply() {
    var count = 1;

    var obj = getItem('newReply');
    if (obj != null) {
        count = parseInt(obj) + 1;
    }
    setItem('newReply', count);

}

function setNewReply() {
    var count = 0;

    var obj = getItem('newReply');
    if (obj != null) {
        count = parseInt(obj);
    }
    if (count !== 0) {

        $('.new-comment-tip').text(count).show();

    } else {

        $('.new-comment-tip').hide();
    }
}

function updateCountMsg(key) {
    var count = 1;
    var obj = getItem(key);
    if (obj != null) {
        count = parseInt(obj) + 1;
    }
    setItem(key, count);
}

function getCountMsg(key) {
    var count = 0;

    var obj = getItem(key);
    if (obj != null) {
        count = parseInt(obj);
    }
    return count;
}



function removeMeetingItem(key, meetingId) {
    var meetingList = getItem(key);

    if (meetingList != null) {
        var list = JSON.parse(meetingList);
        for (var i = 0; i < list.length; i++) {
            if (list[i].meetingid === meetingId) {
                list.splice(i, 1);
                break;
            }
        }

        setItem(key, JSON.stringify(list));

    }
}

//data为一个meeting对象
function setMeetingItem(key, data) {
    if (data.content.length > 45) {
        data.content = data.content.substr(0, 45) + '...';
    }
    
    data.created = new Date();
    var list = [];
    var meetingList = getItem(key);

    if (meetingList != null) {
        list = JSON.parse(meetingList);
    }
    list.unshift(data);
    setItem(key, JSON.stringify(list));
}


function getMeetingItem(key, expireMiniute, pageIndex, pageSize) {
    expireMiniute = 15;
    var meetingList = getItem(key);
    if (meetingList == null) return null;

    var list = JSON.parse(meetingList);

    var valid = $.map(list, function (ele) {

        var milliseconds = new Date().getTime() - new Date(ele.created).getTime();

        var diffMiniutes = Math.floor(milliseconds / (1000 * 60));
        if (diffMiniutes < expireMiniute) {
            return ele;
        }
    });


    //去除重复的
    var result = [], hash = {};
    for (var j = 0;j<valid.length; j++) {
        if (!hash[valid[j].meetingid]) {
            result.push(valid[j]);
            hash[valid[j].meetingid] = true;
        }
    }

    valid = result;
    //去除重复的

    var length = valid.length;

    var start = (pageIndex - 1) * pageSize;
    var end = start + pageSize;
    if (start > length) {
        start = length - pageSize;

    }
    if (start < 0) {
        start = 0;
    }
    if (end > length) {
        end = length;
    }

    var pageCount = Math.ceil(length / pageSize);
    var tmp = [];

    for (var i = start; i < end; i++) {
        tmp.push(valid[i]);
    }

    setItem(key, JSON.stringify(valid));
    return {
        list: tmp,
        pageCount: pageCount,
        pageIndex: pageIndex,
        pageSize: pageSize,
        totalCount: valid.length
    };

}
