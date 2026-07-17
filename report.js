const reportApi = "https://localhost:7152/api/Report";

function downloadEmployeePdf() {

    fetch(reportApi + "/employees/pdf", {

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        }

    })
        .then(response => response.blob())
        .then(blob => {

            const url = window.URL.createObjectURL(blob);

            const a = document.createElement("a");

            a.href = url;

            a.download = "Employees.pdf";

            a.click();

        });

}

function downloadEmployeeExcel() {

    fetch(reportApi + "/employees/excel", {

        headers: {

            "Authorization": "Bearer " + localStorage.getItem("token")

        }

    })
        .then(response => response.blob())
        .then(blob => {

            const url = window.URL.createObjectURL(blob);

            const a = document.createElement("a");

            a.href = url;

            a.download = "Employees.xlsx";

            a.click();

        });

}