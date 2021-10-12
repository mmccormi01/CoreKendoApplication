/*
 START all functions (like click-handlers) that need to occur at page load here
*/
$(document).ready(function () {

    $("#adminicon").addClass('highlighted');
    createAdminTabStrip();
    createSecurityGrid();
    createAddUserDialog();

});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/

function createAddUserDialog() {

    $("#addUserDialog").kendoWindow({
        draggable: false,
        height: "225px",
        modal: true,
        pinned: false,
        position: {
            top: 100,
            left: 100
        },
        resizable: false,
        title: "Add User",
        width: "500px",
        visible: false,
        open: initializeAddUserWindow
    });

    function initializeAddUserWindow() {
        $("#userRole").data("kendoDropDownList").select(0);

        // no need to keep creating masked textboxes
        // when one already exists
        if ($("#adQueryField").data("kendoMaskedTextBox") === undefined) {
            $("#adQueryField").kendoMaskedTextBox({
                mask: "u000000"
            });
        }
    }

    $("#userRole").kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "Read Only", value: "Read" },
            { text: "Read/Write", value: "Write" },
            { text: "Admin", value: "Admin" }
        ],
        optionlabel: "None"
    });

    // Default new user to read role
    $("#userRole").data("kendoDropDownList").select(0);

    $('#addFailureNotification').kendoNotification();

    $('#confirmAddUser').kendoButton({
        click: function (e) {
            var addSecurityUserUrl = $("#addUserDialog").data("addSecurityUserUrl");

            var searchActiveDirectoryUrl = $("#addUserDialog").data("searchActiveDirectoryUrl");
            var unumber = $("#adQueryField").data("kendoMaskedTextBox").value();

            $.ajax({
                type: "GET",
                dataType: "json",
                url: searchActiveDirectoryUrl + "?UserName=" + unumber,
                success: function (currentUserDataItem) {

                    if (currentUserDataItem == null) {
                        var addFailureNotification = $("#addFailureNotification").kendoNotification().data("kendoNotification");
                        addFailureNotification.warning("This user does not exist.");
                        return;
                    }

                    var securityGridData = $("#securityGrid").data().kendoGrid.dataSource.view();

                    var foundMatch = false;
                    for (let cnt = 0; cnt < securityGridData.length; cnt++) {
                        if (securityGridData[cnt].UserName === currentUserDataItem.UserName) { foundMatch = true; }
                    }

                    if (!foundMatch) {
                        var data = {
                            UserName: currentUserDataItem.UserName,
                            RoleName: $("#userRole").data("kendoDropDownList").value(),
                            Email: currentUserDataItem.UserEmail,
                            DisplayName: currentUserDataItem.DisplayName
                        };

                        $.ajax({
                            url: addSecurityUserUrl,
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify(data),
                            cache: false,
                            success: function (result) {
                                // clear out once added
                                $("#adQueryField").data("kendoMaskedTextBox").value("");
                                $("#addUserDialog").data("kendoWindow").close();
                                $("#securityGrid").data("kendoGrid").dataSource.read();
                            }
                        });

                    } else {
                        var addFailureNotification = $("#addFailureNotification").kendoNotification().data("kendoNotification");
                        addFailureNotification.warning("This user already exists. Unable to add to the system.");
                    }

                }
            });
        }
    });

    $('#cancelAddUser').kendoButton({
        click: function (e) {
            $("#addUserDialog").data("kendoWindow").close();
        }
    });

}

function openAddUserDialog() {
    $("#addUserDialog").data("kendoWindow").center().open();
}

function createAdminTabStrip() {
    $("#adminTabStrip").kendoTabStrip({
        scrollable: false,
        value: "Users"
    });
}

function createSecurityGrid() {
    var getSecurityUsersUrl = $("#securityGrid").data("getSecurityUsersUrl");
    var updateSecurityUserUrl = $("#securityGrid").data("updateSecurityUserUrl");
    var deleteSecurityUserUrl = $("#securityGrid").data("deleteSecurityUserUrl");

    var securityDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "GET",
                dataType: "json",
                url: getSecurityUsersUrl,
                cache: false
            },
            update: {
                type: "POST",
                dataType: "json",
                data: "",
                url: updateSecurityUserUrl,
                cache: false
            },
            destroy: {
                type: "POST",
                dataType: "json",
                data: "",
                url: deleteSecurityUserUrl,
                cache: false
            }
        },
        schema: {
            model: {
                id: "UserName",
                fields: {
                    UserName: { type: "string", editable: false },
                    DisplayName: { type: "string", editable: false },
                    RoleName: { type: "string", editable: true }
                }
            }
        },
        pageSize: 10,
        requestEnd: function (e) {
            if (e.type == "update") {
                location.reload();
            }
        }
    });

    $("#securityGrid").kendoGrid({
        columns: [
            {
                field: "DisplayName",
                title: "Name",
                editable: false,
                filterable: {
                    multi: true,
                    search: true
                }
            },
            {
                field: "UserName",
                title: "User ID",
                editable: false,
                filterable: {
                    multi: true,
                    search: true
                }
            },
            {
                field: "RoleName",
                title: "Role",
                editor: roleDropDownEditor,
                template: function (e) {
                    if (e.RoleName === "Read") {
                        return "Read Only";
                    }
                    if (e.RoleName === "Write") {
                        return "Read/Write";
                    }
                    if (e.RoleName === "Admin") {
                        return "Admin";
                    }
                },
                filterable: {
                    multi: true,
                    search: true
                }
            },
            {
                command: [
                    {
                        name: "edit"
                    },
                    {
                        name: "Delete",
                        click: function (e) {  //add a click event listener on the delete button
                            e.preventDefault(); //prevent page scroll reset
                            var tr = $(e.target).closest("tr"); //get the row for deletion
                            var data = this.dataItem(tr); //get the row data so it can be referred later

                            if (data.id == userName) {
                                kendo.alert("User " + userName + " cannot delete user " + data.id + " . Please contact an alternate administrator to delete this user.");
                            } else {
                                kendo.confirm("Are you sure that you want to delete user " + data.UserName + "?").then(function () {
                                    securityDataSource.remove(data);
                                    securityDataSource.sync();
                                });
                            }
                        }
                    }
                ],
                title: "&nbsp",
                width: "200px"
            }
        ],
        dataSource: securityDataSource,
        editable: "inline",
        mobile: true,
        sortable: true,
        filterable: {
            messages: {
                search: "Search"
            }
        },
        pageable: {
            pageSizes: true
        },
        toolbar: [
            { template: '<a class="k-button" href="javascript:void(0);" onclick="openAddUserDialog()">Add User</a>' },
            { name: "excel" }
        ],
        excel: {
            allPages: true,
            fileName: "UserRoles.xlsx"
        }
    });
}


function roleDropDownEditor(container, options) {
    $('<input required name="' + options.field + '"/>').appendTo(container).kendoDropDownList({
        dataTextField: "text",
        dataValueField: "value",
        dataSource: [
            { text: "Read Only", value: "Read" },
            { text: "Read/Write", value: "Write" },
            { text: "Admin", value: "Admin" }
        ]
    });
}

/*
 END all load-independent functions
*/
