$('#Attending').on('change', function () {
    if ($ === "Yes") {
        $("#PartyDates").show()
    }
    else {
        $("#PartyDates").hide()
    }
});

