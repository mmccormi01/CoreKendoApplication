/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {
    setEnvironmentBanner();

    $(document).ajaxStart(function () {
        startLoading("#allcontent");
        //startLoading(document.body);
    });
    //----------------------------------------------
    $(document).ajaxStop(function () {
        stopLoading("#allcontent");
        //stopLoading(document.body);
    });

});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/

function startLoading(target) {
    kendo.ui.progress($(target), true);
    //Scroll so that the spinner is centered on the screen
    var height = $(target).height();
    height = (height / 2) + $(target).offset().top;
    height = height - ($(window).height() / 2);
    window.scrollTo(0, height);
}

/* Remove the loading icon and Un-Block the entire page*/
function stopLoading(target) {
    kendo.ui.progress($(target), false)
}

/*
 END all load-independent functions
*/
