// tabchart
function openChart(evt, cityName) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
// login
function openNav() {
    document.getElementById("myNav").style.display = "block";
}

function closeNav() {
    document.getElementById("myNav").style.display = "none";
}
// register
function openNav1() {
    document.getElementById("myNav").style.display = "none";
    document.getElementById("myNav1").style.display = "block";
}
function closeNav1() {
    document.getElementById("myNav1").style.display = "none";
}