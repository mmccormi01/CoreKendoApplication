/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {

    createContactDialog();

    $('.panel-collapse').on('show.bs.collapse', function () {
        $(this).parent().find(".glyphicons").removeClass("glyphicons-plus").addClass("glyphicons-minus");
    });


    $('.panel-collapse').on('hide.bs.collapse', function () {
        $(this).parent().find(".glyphicons").removeClass("glyphicons-minus").addClass("glyphicons-plus");
    });

    $(document).on("click", ".contact-icon", function () {
        $("#contactinfo").data("kendoDialog").open();
    });

    $(document).on("click", ".k-overlay", function () {
        $("#contactinfo").data("kendoDialog").close();
    });

    $('body').scrollspy({
        target: '.bs-docs-sidebar',
        offset: 40
    });

});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/

function createContactDialog() {
    $("#contactinfo").kendoDialog({
        closable: false,
        title: false,
        visible: false
    });
}

/*
 END all load-independent functions
*/