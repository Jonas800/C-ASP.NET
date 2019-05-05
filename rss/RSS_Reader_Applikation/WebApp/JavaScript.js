$("#flip-checkbox-2").on("change", function (e) {
    var newValue = $("#flip-checkbox-2").val();
    if (newValue == "on") {
        $(".ui-overlay-a").toggleClass("dark")
        $(".outer_box").toggleClass("dark")
        $("body").toggleClass("dark")

    }
});