function login() {

    var login = {
        userName: $("#txtUsername").val(),
        password: $("#txtPassword").val()
    };

    $.ajax({

        url: "https://localhost:7152/api/Auth/login",

        type: "POST",

        contentType: "application/json",

        data: JSON.stringify(login),

        success: function (response) {

            localStorage.setItem("token", response.token);

            alert("Login Successful");

            window.location.href = "dashboard.html";
        },

        error: function () {

            alert("Invalid Username or Password");

        }

    });

}