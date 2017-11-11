$(document).ready(function(){
	
	todoList();
	discussionWidget();
	
	
	/*------- Main Calendar -------*/
	$('#external-events div.external-event').each(function() {

		// it doesn't need to have a start or end
		var eventObject = {
			title: $.trim($(this).text()) // use the element's text as the event title
		};
		
		// store the Event Object in the DOM element so we can get to it later
		$(this).data('eventObject', eventObject);
		
		// make the event draggable using jQuery UI
		$(this).draggable({
			zIndex: 999,
			revert: true,      // will cause the event to go back to its
			revertDuration: 0  //  original position after the drag
		});
		
	});
	
	
	$('#recent a:first').tab('show');
	$('#recent a').click(function (e) {
	  e.preventDefault();
	  $(this).tab('show');
	}); 
	
	
	/*------- Realtime Update Chart -------*/
	
	
});


//日历插件
//var disabledDays = [0, 6];

//$('.disabled-days').datepicker({
//	language: 'cn'
//	//onRenderCell: function (date, cellType) {
//	//	if (cellType == 'day') {
//	//		var day = date.getDay(),
//	//			isDisabled = disabledDays.indexOf(day) != -1;
//	//		return {
//	//			disabled: isDisabled
//	//		}
//	//	}
//	//}
//})