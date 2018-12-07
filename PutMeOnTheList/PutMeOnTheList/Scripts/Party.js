function ShowDates() {
    var x = document.getElementById("Attending").value;
    if (x == "Yes") {
        $('#PartyDates').show();
    }
    else {
        $('#PartyDates').hide();
    }
};


function CheckForDate() {
    var attending = document.getElementById("Attending").value;
    date1 = document.getElementById("PartyDate1").checked
    date2 = document.getElementById("PartyDate2").checked
    if (attending == "Yes" && date1 == false && date2 == false) {
        alert("Please select party date.");
        event.preventDefault();
    }
};




