//加载		
//document.onreadystatechange = loading; 
//	function loading(){
//		if(document.readyState == "complete")
//		{ 
//			$("#fakeloader").hide();
//			$("#content").show();
//		}
//	}	

//下拉
//$(function(){
//	$('select').searchableSelect();
//});
$("#fakeloader").hide();
$("#content").show();
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

//顶部固定

var $head = $( '#header' );
			$( '.ha-waypoint' ).each( function(i) {
				var $el = $( this ),
					animClassDown = $el.data( 'animateDown' ),
					animClassUp = $el.data( 'animateUp' );

				$el.waypoint( function( direction ) {
					if( direction === 'down' && animClassDown ) {
						$head.attr('class', 'ha-header ' + animClassDown);
					}
					else if( direction === 'up' && animClassUp ){
						$head.attr('class', 'ha-header ' + animClassUp);
					}
				}, { offset: '100%' } );
			} );
			
//全屏导航			
if ('ontouchstart' in window) {
	    var click = 'touchstart';
	} else {
	    var click = 'click';
	}
	$('div.burger').on(click, function () {
	    if (!$(this).hasClass('open')) {
	        openMenu();
	    } else {
	        closeMenu();
	    }
	});
	$('div.menu a').on(click, function (e) {
	    //e.preventDefault();
	    closeMenu();
	});
	function openMenu() {
	    $('div.circle').addClass('expand');
	    $('div.burger').addClass('open');
	    $('div.x, div.y, div.z').addClass('collapse');
	    setTimeout(function () {
	        $('div.y').hide();
	        $('div.x').addClass('rotate30');
	        $('div.z').addClass('rotate150');
	    }, 70);
	    setTimeout(function () {
	        $('div.x').addClass('rotate45');
	        $('div.z').addClass('rotate135');
	    }, 120);
	}
	function closeMenu() {
	    $('div.burger').removeClass('open');
	    $('div.x').removeClass('rotate45').addClass('rotate30');
	    $('div.z').removeClass('rotate135').addClass('rotate150');
	    $('div.circle').removeClass('expand');
	    setTimeout(function () {
	        $('div.x').removeClass('rotate30');
	        $('div.z').removeClass('rotate150');
	    }, 50);
	    setTimeout(function () {
	        $('div.y').show();
	        $('div.x, div.y, div.z').removeClass('collapse');
	    }, 70);
	}
	

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

$(document).ready(function() {
	$("#owl-demo01").owlCarousel({
		items : 1	,
		itemsDesktop:[1099,1],
       itemsDesktopSmall:[979,1]	,
       itemsTablet:[768,1],
       itemsMobile:[479,1],
		lazyLoad : true,
		navigation : true,
		autoPlay: false
	});
});
$(document).ready(function() {
	$("#owl-demo02").owlCarousel({
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
//返回顶部
$(function(){
	var $body = $(document.body);;
	var $bottomTools = $('.bottom_tools');
		$(window).scroll(function () {
			var scrollHeight = $(document).height();
			var scrollTop = $(window).scrollTop();
			scrollTop > 50 ? $("#scrollUp").fadeIn(200).css("display","block") : $("#scrollUp").fadeOut(200);			
		});
		$('#scrollUp').click(function (e) {
			e.preventDefault();
			$('html,body').animate({ scrollTop:0});
		});
	
});

