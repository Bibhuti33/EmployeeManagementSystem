const api = "https://localhost:7152/api/Employee";
const departmentApi = "https://localhost:7152/api/Department";

var modal;

$(document).ready(function () {

    loadDepartments();

    loadEmployees();

});

function loadDepartments() {

    $.ajax({

        url: departmentApi,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            $("#departmentId").html("<option value=''>-- Select Department --</option>");

            $.each(response.data, function (i, dept) {

                $("#departmentId").append(

                    `<option value="${dept.departmentId}">
                        ${dept.departmentName}
                    </option>`

                );

            });

        }

    });

}

function loadEmployees() {

    $.ajax({

        url: api,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            $("#employeeTable").html("");

            $.each(response.data, function (i, emp) {

                $("#employeeTable").append(

                    `<tr>

                        <td>${emp.employeeCode}</td>

                        <td>${emp.firstName} ${emp.lastName}</td>

                        <td>${emp.email}</td>

                        <td>${emp.departmentName}</td>

                        <td>${emp.salary}</td>

                        <td>

                            <button class="btn btn-warning btn-sm"
                                onclick="editEmployee(${emp.employeeId})">

                                Edit

                            </button>

                            <button class="btn btn-danger btn-sm"
                                onclick="deleteEmployee(${emp.employeeId})">

                                Delete

                            </button>

                        </td>

                    </tr>`

                );

            });

        }

    });

}

function showAddModal() {

    $("#employeeId").val("");

    $("#employeeCode").val("");

    $("#firstName").val("");

    $("#lastName").val("");

    $("#email").val("");

    $("#phone").val("");

    $("#gender").val("Male");

    $("#dob").val("");

    $("#departmentId").val("");

    $("#salary").val("");

    $("#joiningDate").val("");

    $("#address").val("");

    modal = new bootstrap.Modal(document.getElementById("employeeModal"));

    modal.show();

}

function saveEmployee() {

    var employee = {

        employeeId: parseInt($("#employeeId").val()) || 0,

        employeeCode: $("#employeeCode").val(),

        firstName: $("#firstName").val(),

        lastName: $("#lastName").val(),

        email: $("#email").val(),

        phone: $("#phone").val(),

        gender: $("#gender").val(),

        dob: $("#dob").val(),

        address: $("#address").val(),

        departmentId: parseInt($("#departmentId").val()),

        salary: parseFloat($("#salary").val()),

        joiningDate: $("#joiningDate").val(),

        isActive: true

    };

    $.ajax({

        url: api,

        type: $("#employeeId").val() == "" ? "POST" : "PUT",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        contentType: "application/json",

        data: JSON.stringify(employee),

        success: function () {

            alert($("#employeeId").val() == "" ?

                "Employee Added Successfully"

                :

                "Employee Updated Successfully");

            modal.hide();

            loadEmployees();

        },

        error: function () {

            alert("Unable to Save Employee");

        }

    });

}

function editEmployee(id) {

    $.ajax({

        url: api + "/" + id,

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            var emp = response.data;

            $("#employeeId").val(emp.employeeId);

            $("#employeeCode").val(emp.employeeCode);

            $("#firstName").val(emp.firstName);

            $("#lastName").val(emp.lastName);

            $("#email").val(emp.email);

            $("#phone").val(emp.phone);

            $("#gender").val(emp.gender);

            $("#dob").val(emp.dob.substring(0, 10));

            $("#address").val(emp.address);

            $("#departmentId").val(emp.departmentId);

            $("#salary").val(emp.salary);

            $("#joiningDate").val(emp.joiningDate.substring(0, 10));

            modal = new bootstrap.Modal(document.getElementById("employeeModal"));

            modal.show();

        }

    });

}

function deleteEmployee(id) {

    if (!confirm("Are you sure you want to delete this employee?"))

        return;

    $.ajax({

        url: api + "/" + id,

        type: "DELETE",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function () {

            alert("Employee Deleted Successfully");

            loadEmployees();

        },

        error: function () {

            alert("Unable to Delete Employee");

        }

    });

}

function searchEmployee() {

    var keyword = $("#txtSearch").val();

    if (keyword == "") {

        loadEmployees();

        return;

    }

    $.ajax({

        url: api + "/search?keyword=" + encodeURIComponent(keyword),

        type: "GET",

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        },

        success: function (response) {

            $("#employeeTable").html("");

            $.each(response.data, function (i, emp) {

                $("#employeeTable").append(

                    `<tr>

                        <td>${emp.employeeCode}</td>

                        <td>${emp.firstName} ${emp.lastName}</td>

                        <td>${emp.email}</td>

                        <td>${emp.departmentId}</td>

                        <td>${emp.salary}</td>

                        <td>

                            <button class="btn btn-warning btn-sm"
                                onclick="editEmployee(${emp.employeeId})">

                                Edit

                            </button>

                            <button class="btn btn-danger btn-sm"
                                onclick="deleteEmployee(${emp.employeeId})">

                                Delete

                            </button>

                        </td>

                    </tr>`

                );

            });

        }

    });

}