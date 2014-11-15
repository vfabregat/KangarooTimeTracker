(function ($) {
    toastr.options.closeButton = true;
    toastr.options.debug = false;
    toastr.options.positionClass = "toast-top-full-width";

    $(document).ajaxError(function () {
        toastr.error("An error has been ocurred, please try again later");
    });

    $("[data-toggle='tooltip']").tooltip();
    $("[data-toggle='popover']").popover({ trigger: 'hover', 'placement': 'top' });

}(jQuery));