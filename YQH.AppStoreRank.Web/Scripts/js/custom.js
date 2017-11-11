/*
	Scripts for Sympathique - V1.0
*/

jQuery(document).ready(function($) {
	
	//To top jQueryuery
	jQuery().UItoTop({ easingType: 'easeOutQuart' });	
	
});



//TAB
jQuery(document).ready(function() {
	// TABS FUNCTION //
	$('.tabs-wrapper').each(function() {
		$(this).find(".tab-content").hide(); //Hide all content
		$(this).find("ul.tabs li:first").addClass("active").show(); //Activate first tab
		$(this).find(".tab-content:first").show(); //Show first tab content
	});
	$("ul.tabs li").click(function(e) {
		$(this).parents('.tabs-wrapper').find("ul.tabs li").removeClass("active"); //Remove any "active" class
		$(this).addClass("active"); //Add "active" class to selected tab
		$(this).parents('.tabs-wrapper').find(".tab-content").hide(); //Hide all tab content

		var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
		$("li.tab-item:first-child").css("background", "none" );
		$(this).parents('.tabs-wrapper').find(activeTab).fadeIn(); //Fade in the active ID content
		e.preventDefault();
	});
	$("ul.tabs li a").click(function(e) {
		e.preventDefault();
	});
	$("li.tab-item:last-child").addClass('last-item');

});


//下拉
$(function(){
	//$('select').searchableSelect();
});


//滚动
$(document).ready(function() {
	$("#owl-demo").owlCarousel({
		items : 1	,
		itemsDesktop:[1099,1],
       itemsDesktopSmall:[979,1]	,
       itemsTablet:[768,1],
       itemsMobile:[479,1],
		lazyLoad : true,
		navigation : true,
		autoPlay:true
	});
});
//滚动
$(document).ready(function() {
	//$("#owl-demo02").owlCarousel({
	//	items : 1	,
	//	itemsDesktop:[1099,1],
    //   itemsDesktopSmall:[979,1]	,
    //   itemsTablet:[768,1],
    //   itemsMobile:[479,1],
	//	lazyLoad : true,
	//	navigation : true,
	//	autoPlay:8000
	//});
});


//垂直手风琴菜单

$(function () {
    var Accordion = function (el, multiple) {
        this.el = el || {};
        this.multiple = multiple || false;
        var links = this.el.find('.link');
        links.on('click', {
            el: this.el,
            multiple: this.multiple
        }, this.dropdown);
    };
    Accordion.prototype.dropdown = function (e) {
        var $el = e.data.el;
        $this = $(this), $next = $this.next();
        $next.slideToggle();
        $this.parent().toggleClass('open');
        if (!e.data.multiple) {
            $el.find('.submenu').not($next).slideUp().parent().removeClass('open');
        }
        ;
    };
    var accordion = new Accordion($('#accordion'), false);
});