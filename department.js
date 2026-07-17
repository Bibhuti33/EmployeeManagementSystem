const departmentApi = "https://localhost:7152/api/Department";

var departmentModal;

$(document).ready(function () {

    loadDepartments();

});

function loadDepartments() {

    $.ajax({

        url: departmentApi,

        type: "GET",

        headers: {
            "Authorization": "Bearer " + localStorage.getItem("token")
        },

        success: function (response) {

            $("#departmentTable").html("");

            $.each(response.data, function (i, dept) {

                $("#departmentTable").append(`

                <tr>

                    <td>${dept.departmentId}</td>

                    <td>${dept.departmentName}</td>

                    <td>${dept.description ?? ""}</td>

                    <td>

                        <button class="btn btn-warning btn-sm"
                        onclick="editDepartment(${dept.departmentId})">

                        Edit

                        </button>

                        <button class="btn btn-danger btn-sm"
                        onclick="deleteDepartment(${dept.departmentId})">

                        Delete

                        </button>

                    </td>

                </tr>

                `);

            });

        }

    });

}

function showDepartmentModal() {

    $("#departmentId").val("");

    $("#departmentName").val("");

    $("#description").val("");

    departmentModal = new bootstrap.Modal(document.getElementById("departmentModal"));

    departmentModal.show();

}

function saveDepartment() {

    var department = {

        departmentId: parseInt($("#departmentId").val()) || 0,

        departmentName: $("#departmentName").val(),

        description: $("#description").val(),

        isActive: true

    };

    $.ajax({

        url: departmentApi,

        type: $("#departmentId").val() == "" ? "POST" : "PUT",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        contentType: "application/json",

        data: JSON.stringify(department),

        success: function () {

            alert("Department Saved Successfully");

            departmentModal.hide();

            loadDepartments();

        },

        error: function () {

            alert("Unable to Save Department");

        }

    });

}

function editDepartment(id) {

    $.ajax({

        url: departmentApi + "/" + id,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            var dept = response.data;

            $("#departmentId").val(dept.departmentId);

            $("#departmentName").val(dept.departmentName);

            $("#description").val(dept.description);

            departmentModal = new bootstrap.Modal(document.getElementById("departmentModal"));

            departmentModal.show();

        }

    });

}

function deleteDepartment(id) {

    if (!confirm("Delete this department?"))
        return;

    $.ajax({

        url: departmentApi + "/" + id,

        type: "DELETE",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function () {

            alert("Department Deleted Successfully");

            loadDepartments();

        }

    });

}