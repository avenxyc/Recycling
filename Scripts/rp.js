$(document).ready(function () {
    
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

    var createAutocomplete = function () {

        var $input = $(this);

        var options = {
            source: $input.attr("data-rp-autocomplete")
        };

        $input.autocomplete(options);
    };


    $("form[data-rp-ajax='true']").submit(ajaxFormSubmit);
    $("input[data-rp-autocomplete]").each(createAutocomplete);

   
});