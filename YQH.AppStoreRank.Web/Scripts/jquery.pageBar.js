; (function ($, undefined) {

    $.fn.pagebar = function (options) {
        var $pageList=[],
            pageIndex,
            totalPages,
            dispPageCounts,
            func;
        var $container = this;
        options = $.extend({}, $.fn.pagebar.options, options);//可以保证默认选项不被修改，且可以暴漏给外部使用者做全局修改。
        pageIndex = parseInt(options.pageIndex);
        totalPages = parseInt(options.totalPages);
        dispPageCounts = options.dispPageCounts;
        func = options.callback;
        this.empty();



        var start = pageIndex - parseInt(dispPageCounts / 2);
        if (start <= 0)
            start = 1;
        var end = start + dispPageCounts - 1;
        if (end > totalPages) {
            end = totalPages;
            start = totalPages - dispPageCounts + 1 > 0 ? totalPages - dispPageCounts + 1 : 1;
        }


        this.append($('<a class="pagebar_first" href="javascript:void(0);" pageindex=1 >首页</a>'));

        if (pageIndex > 1) {

            this.append($('<a class="pagebar_pre" href="javascript:void(0);" pageindex=' + (pageIndex - 1) + '>上一页</a>'));

        }
     
        for (i = start; i <= end; i++) {
            if (i == pageIndex) {
                var $page1 = $('<a  class="pagebar_raw" href="javascript:void(0);" pageindex =' + i + ' >' + i + '</a>');
                this.append($page1);
                $pageList[$pageList.length] = $page1;
            }
            else {
                var $page2 = $('<a  class="pagebar_pages" href="javascript:void(0);" pageindex=' + i + '>' + i + '</a>');
                this.append($page2);
                $pageList[$pageList.length] = $page2;
            }

        }
        if (pageIndex < totalPages) {
            this.append($('<a  class="pagebar_next" href="javascript:void(0);" pageindex=' + (pageIndex + 1) + '>下一页</a>'));
        }
        this.append($('<a  class="pagebar_last" href="javascript:void(0);" pageindex=' + totalPages + '>尾页</a>'));


        this.append($('<span class="nth-current">当前第' + pageIndex + '/' + totalPages + '页</span>'));

        for (var i = 0; i < $pageList.length; i++) {
            $pageList[i].click(function () {
                var index = parseInt($(this).attr('pageindex'));
				
                func(index);
                options.pageIndex = index;
                options = $.extend({}, $.fn.pagebar.options, options);
                $container.pagebar(options);
            })
        }
        $('.pagebar_pre,.pagebar_next,.pagebar_last,.pagebar_first').click(function () {
            var index = parseInt($(this).attr('pageindex'));
            func(index);
            options.pageIndex = index;
            options = $.extend({}, $.fn.pagebar.options, options);
            $container.pagebar(options);

        })
        return this;

    }


    $.fn.pagebar.options = {
        pageIndex: 1,//当前页码
        totalPages: 20, //总共有多少也
        dispPageCounts: 10, //显示多少个页面
        callback: function () { } //点击当前页面回调函数，传入当前index
    }

})(jQuery);