$(function () { 
	// Stack initialize
	var openspeed = 300;
	var closespeed = 300;
	$('.stack>button').toggle(function(){
		var vertical = 0;
		var horizontal = 0;
		var $el=$(this);
		$el.next().children().each(function(){
			$(this).animate({top: '-' + vertical + 'px', left: horizontal + 'px'}, openspeed);
			vertical = vertical + 30;
			horizontal = (horizontal+2.5)*2;
		});
		$el.next().animate({top: '-30px', right: '20px'}, openspeed).addClass('openStack')
		
	}, function(){
		//reverse above
		var $el=$(this);
		$el.next().removeClass('openStack').children('li').animate({ left: '-10px'}, closespeed);
	});
	
	// Stacks additional animation
	$('.stack li a').hover(function(){
		$("span",this).animate({marginRight: '8px'});
	},function(){
		$("span",this).animate({marginRight: '0'});
	});
});