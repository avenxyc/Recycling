$(document).ready(function () {
    // Define Ajax Form Search Funtion
    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-rp-target"));
            $target.html(data);
        });

       
    };

    // Define AutoComlete Function
    var createAutocomplete = function () {

        var $input = $(this);

        var options = {
            source: $input.attr("data-rp-autocomplete")
        };

        $input.autocomplete(options);
    };

    //// Define get pagedList url Function
    //var getPage = function () {
    //    var $page = $(this);

    //    var options = {
    //        url: $page.attr("href"),
    //        type: "get"
    //    };

    //    alert("it is called");

    //    $.ajax(options).done(function (data) {
            
    //        var target = $page.parent("div.pagedList").attr("data-rp-target");
    //        $(target).replaceWith(data);
    //    });
    //    return false;

    //}


    $("form[data-rp-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-rp-autocomplete]").each(createAutocomplete);

    //$("container body-content").on("click", ".pagedList a", getPage);

   
});