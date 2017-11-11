//author:Emma
//date:201709.24
//name:scrollLoad 滚动加载插件
//version:1.3.0


; (function ($, window, document, undefined) {
    //构造函数
    var PageBar = function (ele, opts) {
        this._init(ele, opts);
    };

    PageBar.prototype = {
        //私有方法
        _init: function (ele, opts) {

            var defaults = {
                url: '/data.json?pageIndex={{pageIndex}}',
                param: {},
                pageIndex: 1,
                totalPages: 20,
                isWithSkipBtn: false,
                getTotalPages: function (data) {
                    return 30;
                },
                displayPageCount: 10,
                nextPage: function (data, currentPage) {

                }
            };
            this.$element = ele;
            this.settings = $.extend({}, defaults, opts);
            var _this = this;

            $.ajax({
                type: "GET",
                url: this.settings.url.replace('{{pageIndex}}', this.settings.pageIndex),
                data: this.settings.param,
                dataType: "json",
                success: function (data) {
                    _this.settings.totalPages = _this.settings.getTotalPages(data);
                    _this._render();
                    _this.settings.nextPage(data, _this.settings.pageIndex);
                }
            });


        },
        _render: function () {
            this.$element.empty();
            var $pageList = [];
            var $container = $('<div class="pagebar-wrapper"></div>');


            var pageIndex = parseInt(this.settings.pageIndex);

            var totalPages = parseInt(this.settings.totalPages);
            var displayPageCount = parseInt(this.settings.displayPageCount);
            var _this = this;



            var start = pageIndex - parseInt(displayPageCount / 2);

            if (start <= 0)
                start = 1;


            var end = start + displayPageCount;
            if (end > totalPages) {
                end = totalPages;
                start = totalPages - displayPageCount + 1 > 0 ? totalPages - displayPageCount + 1 : 1;
            }


            $container.append($('<a class="pagebar-first" href="javascript:void(0);"  data-page-index="1" >首页</a>'));

            if (pageIndex > 1) {
                $container.append($('<a class="pagebar-pre" href="javascript:void(0);" data-page-index=' + (pageIndex - 1) + '>上一页</a>'));
            }

            if (start > displayPageCount) {
                var prevPage = (start - displayPageCount) <= 0 ? 1 : (start - displayPageCount);
                $container.append($('<a class="pagebar-dot" href="javascript:void(0);"  data-page-index="' + prevPage + '" >. . .</a>'));
            }

            for (var i = start; i <= end; i++) {
                if (i === pageIndex) {
                    var $page1 = $('<a  class="pagebar-raw" href="javascript:void(0);"  data-page-index="' + i + '">' + i + '</a>');
                    $container.append($page1);
                }
                else {
                    var $page2 = $('<a  class="pagebar-pages" href="javascript:void(0);" data-page-index="' + i + '">' + i + '</a>');
                    $container.append($page2);
                    $pageList[$pageList.length] = $page2;
                }

            }

            if (end < totalPages) {
                var nextPage = end + 10 > totalPages ? totalPages : end + displayPageCount;
                $container.append($('<a class="pagebar-dot" href="javascript:void(0);"  data-page-index="' + nextPage + '" >. . .</a>'));
            }

            if (pageIndex < totalPages) {
                $container.append($('<a  class="pagebar-next" href="javascript:void(0);" data-page-index="' + (pageIndex + 1) + '">下一页</a>'));
            }
            $container.append($('<a  class="pagebar-last" href="javascript:void(0);" data-page-index="' + totalPages + '">尾页</a>'));


            $container.append($('<span class="pagebar-nth-current">当前第' + pageIndex + '/' + totalPages + '页</span>'));

            if (_this.settings.isWithSkipBtn) {

                $container.append($('<span class="pagebar-skip-page">跳到第<input class="input-page-index" type="text" />页 <input class="input-btn-go" type="button" value="Go" /></span>'));
            }

            this.$element.append($container);

            $('.input-btn-go').click(function() {
                var $this = $(this);
                var $input = $this.siblings('.input-page-index');
                var index = $input.val();
                if (index <= 1) {
                    $input.val(1);
                }
                if (index >= totalPages) {
                    $input.val(totalPages);
                }

                $.ajax({
                    type: "GET",
                    url: _this.settings.url.replace('{{pageIndex}}', index),
                    data: _this.settings.param,
                    dataType: "json",
                    success: function (data) {
                        _this.settings.totalPages = _this.settings.getTotalPages(data);
                        _this.settings.pageIndex = index;
                        _this._render();
                        _this.settings.nextPage(data, index);

                    }
                });

            });

            $.each($pageList, function (index, $page) {
                $page.click(function () {
                    var index = $(this).data('pageIndex');
                    $.ajax({
                        type: "GET",
                        url: _this.settings.url.replace('{{pageIndex}}', index),
                        data: _this.settings.param,
                        dataType: "json",
                        success: function (data) {
                            _this.settings.totalPages = _this.settings.getTotalPages(data);
                            _this.settings.pageIndex = index;
                            _this._render();
                            _this.settings.nextPage(data, index);

                        }
                    });

                });
            });


            $('.pagebar-pre,.pagebar-next,.pagebar-last,.pagebar-first,.pagebar-dot').click(function () {
                var index = $(this).data('pageIndex');
                $.ajax({
                    type: "GET",
                    url: _this.settings.url.replace('{{pageIndex}}', index),
                    data: _this.settings.param,
                    dataType: "json",
                    success: function (data) {
                        _this.settings.totalPages = _this.settings.getTotalPages(data);
                        _this.settings.pageIndex = index;
                        _this._render();
                        _this.settings.nextPage(data, index);

                    }
                });

            });
        },
        _loadData: function (pageIndex,_this) {

        },

        Options: function () {
            return this.settings;
        }
    }

    $.fn.pagebar = function (opts) {
        var pagebar = new PageBar(this, opts);
        return pagebar;
    }

})($, window, document);


//var scroll = $("#myList").scrollLoad({
//    url: '/data.json?pageIndex={{pageIndex}}',
//    param: {
//        type: type,
//        state: 1
//    },
//    loading: "#loading",
//    nextPage: function (data, $wrapper, currentPage) {
//        var list = data.result.list;
//        for (var i = 0; i < list.length; i++) {
//            var item = list[i];
//            var $item = $('<li>' + item.name + '</li>')
//            $wrapper.append($item);
//        }

//    }
//})