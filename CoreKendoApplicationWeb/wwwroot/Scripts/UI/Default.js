/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {

    setHelloWorldText();

    createKendoGrid();
});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/

function setHelloWorldText(){
    var name = "World";

    $.ajax({
        url: "/api/Default/GetHelloWorld" + "?name=" + name,
        type: "GET",
        contentType: 'application/json; charset=utf-8',
        cache: false,
        success: function (result) {
            $("#helloworld").text(result);
        }
    });

}

function createKendoGrid() {

    $("#kendogrid").kendoGrid({
        dataSource: [
                { technology: "Kendo UI", resource: "http://docs.telerik.com/kendo-ui/api/javascript/class" },
                { technology: "Bootstrap", resource: "https://www.w3schools.com/bootstrap/" },
                { technology: "Entity Framework Core", resource: "https://docs.microsoft.com/en-us/ef/" },
                { technology: "Serilog", resource: "https://serilog.net/" },
                { technology: "Git", resource: "https://git-scm.com/about" },
                { technology: "", resource: "" }
        ],
        columns: [
            {
                field: "technology",
                title: "Technology"
            },
            {
                field: "resource",
                title: "Resource"
            }
        ]
    });
}

/*
 END all load-independent functions
*/
