/*jslint browser: true*/
/*global $, jQuery, TweenMax*/

(function ($) {
    'use strict';

    $.fn.slideOut = function (options) {

        var defaults = {
                self: this,
                panels: this.children(".panelHolder"),
                tabs: this.find(".tab"),
                model: this.find(".model"),
                callback: function () {}
            },

            settings = $.extend({}, defaults, options),

            divPositionRight = [],

            divPositionLeft = [],

            animatingPanel = false,

            active_string = "active",

            tab_string = "tab",

            model_string = "model",

            panelHolder_string = "panelHolder",

            slideOutPanel = {

                init: function () {
                    this.setPanel();
                    this.bindEvents();
                },

                currentContainerWidth: settings.self.width(),

                tabWidth: settings.tabs.outerWidth(true),

                modelWidth: settings.model.width(),

                setPanel: function () {

                    var offset = parseFloat(settings.panels.css("padding-right"));

                    //Get the postion of each panle and put it in an arry
                    settings.panels.each($.proxy(this.positionLoop, this, offset))
                        .each(this.setPanelPosition);
                },

                positionLoop: function (offset, index, val) {

                    var $this = $(val),
                        leftPostion = $this.position().left;

                    divPositionRight.push(leftPostion);

                    //What the postion of the panle will be when its on the right side
                    divPositionLeft.push((this.tabWidth + offset) * index);

                },

                setPanelPosition: function (index, val) {

                    //Postion each panle apsolutely bascied off of the postion it was in the dom on load 
                    $(val).css({
                        left: divPositionRight[index],
                        position: "absolute"
                    }).data("right", divPositionRight[index]).data("position", "right");
                },

                bindEvents: function () {

                    settings.self.on("mouseenter click", "." + panelHolder_string, $.proxy(this.hoverOverPanelEvent, this));

                    //settings.self.on("mouseleave", $.proxy(this.hoverOutParentEvent, this));
                },

                setDataPosition: function (hoverOverIndex) {

                    settings.panels.each($.proxy(this.dataLoop, this, hoverOverIndex));
                },

                dataLoop: function (hoverOverIndex, index, val) {
                    
                    var $this = $(val), //Current panel in loop
                        currentLeftPosition = parseFloat($this.css("left"));

                    //Set the date so when the hover event is call it know how to animate
                    if (index < hoverOverIndex) {

                        $this.data("position", "left");

                    } else {

                        $this.data("position", "right");
                    }

                    /*
                     * Check to see if the element you hovered over has other panels in front of it 
                     * if it does move all the panle to the left or right
                     *
                    */
                    
                    if (currentLeftPosition !== divPositionLeft[index] && $this.data("position") === "left") {

                        TweenMax.to($this, 0.5, {
                            left: divPositionLeft[index]
                        });

                    } else if (index !== hoverOverIndex && currentLeftPosition !== divPositionRight[index] && $this.data("position") === "right") {

                        TweenMax.to($this, 0.5, {
                            left: divPositionRight[index]
                        });
                    }
                },

                hoverOverPanelEvent: function (event) {

                    var $this = $(event.currentTarget), //Element that was hovered over
                        currentIndex = $this.index(), //Element index
                        active = settings.self.find("." + active_string);

                    if (!$this.hasClass(active_string) && animatingPanel === false) {

                        if ($this.data("position") !== "right") {

                            this.openPanelLeft($this, active, currentIndex);

                        } else {

                            this.openPanelRight($this, active, currentIndex);
                        }
                    }
                },

                showModelTab: function ($ele) {

                    $ele.parents("." + panelHolder_string).removeClass(active_string);
                },

                hideModelTab: function ($ele) {

                    $ele.parents("." + panelHolder_string).css({
                        left: $ele.data("right"),
                        width: this.tabWidth
                    });
                },

                moveCurrentTab: function ($ele) {

                    $ele.removeAttr("style")
                        .parents("." + panelHolder_string).addClass(active_string);
                },

                showModel: function ($ele) {

                    $ele.removeAttr("style");

                    animatingPanel = false;
                },

                showCurrentModel: function ($ele) {

                    $ele.parents("." + panelHolder_string).addClass(active_string);

                    animatingPanel = false;
                },

                openPanelLeft: function ($this, active, currentIndex) {

                    var modelTab = active.find("." + tab_string),
                        modelImg = active.find("." + model_string),
                        currentTab = $this.find("." + tab_string),
                        currentModel = $this.find("." + model_string);

                    animatingPanel = true;

                    TweenMax.to(modelTab, 0.5, {
                        opacity: 1,
                        onComplete: this.showModelTab,
                        onCompleteParams: [modelTab]
                    });

                    TweenMax.to(modelImg, 0.5, {
                        opacity: 0,
                        onComplete: $.proxy(this.hideModelTab, this),
                        onCompleteParams: [modelImg]
                    });

                    TweenMax.to($this, 0.5, {
                        width: this.modelWidth
                    });

                    TweenMax.to(currentTab, 0, {
                        left: active.data("right"),
                        onComplete: this.moveCurrentTab,
                        onCompleteParams: [currentTab]
                    });

                    TweenMax.to(currentModel, 0, {
                        opacity: 1,
                        onComplete: this.showModel,
                        onCompleteParams: [currentModel]
                    });

                    //Reset data position for non open elements 
                    this.setDataPosition(currentIndex);
                },

                openPanelRight: function ($this, active, currentIndex) {

                    var modelImg = active.find("." + model_string),
                        modelTab = active.find("." + tab_string),
                        currentTab = $this.find("." + tab_string),
                        currentModel = $this.find("." + model_string);

                    animatingPanel = true;

                    TweenMax.to(active, 0.5, {
                       // width: this.tabWidth
                    });

                    TweenMax.to(modelImg, 0.5, {
                        opacity: 0
                    });
					
                    TweenMax.to(modelTab, 0.5, {
                        opacity: 1,
                        onComplete: this.showModelTab,
                        onCompleteParams: [modelTab]
                    });

                    TweenMax.to($this, 0.5, {
                        width: this.modelWidth,
                        left: 0
                    });

                    TweenMax.to(currentTab, 0.5, {
                        opacity: 0
                    });

                    TweenMax.to(currentModel, 0.5, {
                        opacity: 1,
                        onComplete: this.showCurrentModel,
                        onCompleteParams: [currentModel]
                    });

                    //Reset data position for non open elements 
                    this.setDataPosition(currentIndex);
                },

                hoverOutParentEvent: function () {

                    if (animatingPanel) {

                        setTimeout(this.hoverOutParentEvent, 50);

                    } else {
                        settings.panels.eq(0).trigger("click");
                    }
                }
            };

        slideOutPanel.init();

        settings.callback.call(settings.self);
    };
}(jQuery));

$('#panles').slideOut();