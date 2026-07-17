const attendanceApi = "https://localhost:7152/api/Attendance";

var attendanceModal;

$(document).ready(function () {

    loadAttendance();

});

function loadAttendance() {

    $.ajax({

        url: attendanceApi,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            $("#attendanceTable").html("");

            $.each(response.data, function (i, item) {

                $("#attendanceTable").append(`

<tr>

<td>${item.attendanceId}</td>

<td>${item.employeeId}</td>

<td>${item.attendanceDate.substring(0, 10)}</td>

<td>${item.status}</td>

<td>

<button class="btn btn-warning btn-sm"
onclick="editAttendance(${item.attendanceId})">

Edit

</button>

<button class="btn btn-danger btn-sm"
onclick="deleteAttendance(${item.attendanceId})">

Delete

</button>

</td>

</tr>

`);

            });

        }

    });

}

function showAttendanceModal() {

    $("#attendanceId").val("");

    $("#employeeId").val("");

    $("#attendanceDate").val("");

    $("#status").val("Present");

    attendanceModal = new bootstrap.Modal(document.getElementById("attendanceModal"));

    attendanceModal.show();

}

function saveAttendance() {

    var attendance = {

        attendanceId: parseInt($("#attendanceId").val()) || 0,

        employeeId: parseInt($("#employeeId").val()),

        attendanceDate: $("#attendanceDate").val(),

        status: $("#status").val()

    };

    $.ajax({

        url: attendanceApi,

        type: $("#attendanceId").val() == "" ? "POST" : "PUT",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        contentType: "application/json",

        data: JSON.stringify(attendance),

        success: function () {

            alert("Attendance Saved Successfully");

            attendanceModal.hide();

            loadAttendance();

        },

        error: function () {

            alert("Unable to Save Attendance");

        }

    });

}

function editAttendance(id) {

    $.ajax({

        url: attendanceApi + "/" + id,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            var a = response.data;

            $("#attendanceId").val(a.attendanceId);

            $("#employeeId").val(a.employeeId);

            $("#attendanceDate").val(a.attendanceDate.substring(0, 10));

            $("#status").val(a.status);

            attendanceModal = new bootstrap.Modal(document.getElementById("attendanceModal"));

            attendanceModal.show();

        }

    });

}

function deleteAttendance(id) {

    if (!confirm("Delete attendance?"))
        return;

    $.ajax({

        url: attendanceApi + "/" + id,

        type: "DELETE",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function () {

            alert("Attendance Deleted Successfully");

            loadAttendance();

        }

    });

}