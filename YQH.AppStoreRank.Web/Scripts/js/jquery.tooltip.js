/**
+-------------------------------------------------------------------
* jQuery tooltip - 弹窗插件
+-------------------------------------------------------------------
* @version 2.0.1
* @update 2015.07.30
* @author haibao <hhb219@163.com> <http://www.hehaibao.com/>
+-------------------------------------------------------------------
*/
; (function ($, window, document, undefined) {


   var $B = $('body');
    var methods = {
        remove: function (a) { $(a).remove(); },
        fire: function (event, data) { if($.isFunction(event)) { return event.call(this, data); } }
    };
    $.extend({
		
		tooltip: function (t1, t2, t3, callback) {
			t1 = t1 != undefined ? t1 : 'Error...'; t2 = t2 != undefined ? parseInt(t2) : 2500;
			var tip = '<div class="HTooltip fadeInDown animated" style="width:100%; height:60px; padding:0 20px;line-height:60px; text-align:center;background-color:rgba(255,100,100,.8);color:#fff;position:fixed; left:0; top:0; z-index:100001;">' + t1 + '</div>';
			if (t3 == true && t3 != undefined) { tip = '<div class="HTooltip fadeInDown animated" style="width:100%; height:60px; padding:0 20px;line-height:60px; text-align:center;background-color:rgba(70,200,210,.8);color:#fff;position:fixed; left:0; top:0; z-index:100001;">' + t1 + '</div>'; }
			methods.remove('.HTooltip');
			$B.stop().append(tip);
			clearTimeout(t);
			var t = setTimeout(function () {
				methods.remove('.HTooltip');
				if (callback != undefined) { methods.fire.call(this, callback); } //隐藏后的回调方法 
			}, t2);
		}
	
	
	});
})(jQuery, window, document);
