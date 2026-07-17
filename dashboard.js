$(document).ready(function () {

    loadDashboard();

});

function loadDashboard() {

    $.ajax({

        url: "https://localhost:7152/api/Dashboard",

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (data) {

            $("#totalEmployees").text(data.totalEmployees);

            $("#totalDepartments").text(data.totalDepartments);

            $("#presentToday").text(data.presentToday);

            $("#absentToday").text(data.absentToday);

            $("#totalSalary").text("₹ " + data.totalSalary);

        },

        error: function () {

            alert("Unable to load dashboard.");

        }

    });

}

function logout() {

    localStorage.removeItem("token");

    window.location.href = "login.html";

}