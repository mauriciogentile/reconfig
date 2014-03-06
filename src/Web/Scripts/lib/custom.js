var redirect = function (context) {
    var returnUrl = context.get_data().returnUrl;
    if (returnUrl) {
        window.location.href = returnUrl;
    }
};

var refresh = function refresh() {
    window.location.reload();
};

var addRemoveItem = function (collectionName) {
    var removeHandler = function () {
        $("tr[data-row='" + $(this).attr("data-row") + "']").remove();
        updateNames();
    };

    var updateNames = function () {
        var items = $("tr[data-row]");
        items.each(function (index, ele) {
            $(ele).attr("data-row", index);
            $(ele).find("input[data-name], select[data-name]").each(function (idx, ele) {
                $(ele).attr("name", collectionName + "[" + index + "]." + $(ele).attr("data-name"));
            });
        });
    };

    $(".remove.button").click(removeHandler);

    $("#addItem").click(function () {
        var clone = $("#items tr:last").clone();
        $("#items tr:last").after(clone);
        updateNames();
        clone.find(".remove.button").click(removeHandler);
        return false;
    });
};