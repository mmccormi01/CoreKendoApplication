/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {

 /*   setHelloWorldText();*/

    createKendoGrid();
    createResourceGrid();
});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/
function createResourceGrid() {
    var resourceReadUrl = $("#resourcesgrid").data("readResourcesUrl");
    var resourceGrid = $("#resourceGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    type: "GET",
                    dataType: "json",
                    url: resourceReadUrl,
                    cache: false
                },
                update: {
                    type: "POST",
                    dataType: "json",
                    url: resourcetUpdateUrl,
                    cache: false
                },
                create: {
                    type: "POST",
                    dataType: "json",
                    url: resourceCreateUrl,
                    cache: false
                }
            },
            schema: {
                model: {
                    id: "ResourceId",
                    fields: {
                        ResourceId: {},
                        ResourceName: {
                            validation: { required: true }
                        },
                        YearDesignated: {},
                        ResourceType: {},
                        DesignatedStatus: {},
                        ModifiedDate: { editable: false },
                        GISId: {
                            type: "number",
                            validation: {
                                noDuplicates: function (input) {
                                    var gridData = $("#resourceGrid").data("kendoGrid").dataSource.data();
                                    var validator = this;
                                    var noMatchFound = true;
                                    if (input.is("[name=GISId]") && (input.val() !== "")) {
                                        input.attr("data-noduplicates-msg", "Duplicate GIS IDs are not allowed.");
                                        var gridCnt = 0;
                                        while (gridCnt < gridData.length && noMatchFound) {
                                            if ((gridData[gridCnt].GISID == input.val() && input.val() != 0) && !gridData[gridCnt].dirty && (gridData[gridCnt].ProjectID != input[0].kendoBindingTarget.source.ProjectID)) {
                                                noMatchFound = false;
                                            }

                                            gridCnt += 1;
                                        }
                                    }

                                    return noMatchFound;
                                }
                            }
                        },
                        //ProjectEndDate: {
                        //    validation: {
                        //        laterEndDate: function (input) {
                        //            input.attr("data-laterenddate-msg", "End Date must be later than Start Date.");
                        //            var later = true;
                        //            if (input.is("[name=ProjectEndDate]")) {
                        //                var end = new Date(input.val());
                        //                end.setHours(0, 0, 0, 0);
                        //                var start = new Date($("[name=ProjectStartDate]").val());
                        //                start.setHours(0, 0, 0, 0);

                        //                if (end < start) {
                        //                    later = false;
                        //                }
                        //            }

                        //            return later;
                        //        }
                        //    },
                        //    type: "date",
                        //    defaultValue: null
                        //},


                        DocumentFilePath: {},

                        //CommentsnDocument: {},

                        //PrimaryAgencyType: {},
                        //PrimaryAgency: {},

                    }
                }
            },
            filter: applyFilters(),
            requestEnd: function (e) {
                if (e.type !== "read") {
                    this.read();
                }
            },
            requestStart: function (e) {
                startLoading("#allcontent");
            }
        },
        columns: [
            {
                field: "ResourceId",
                title: "Resource ID",
                hidden: true
            },
            {
                field: "ResourceName",
                title: "Resource Name",
                sortable: true,
                filterable: {
                    extra: false,
                    operators: {
                        string: {
                            contains: "Contains",
                            isempty: "Empty"
                        }
                    }
                }
            },
            {
                field: "YearDesignated",
                title: "Year Designated",
                hidden: true
            },
            {
                field: "ResourceTypeName",
                title: "ResourceType",
                sortable: true,
            },
            {
                field: "DesignationStatusName",
                title: "Designation Status",
                sortable: true,
            },
            {
                field: "GISId",
                title: "GIS ID",
                format: "{0:n0}",
                sortable: true,
                filterable: {
                    extra: false,
                    operators: {
                        number: {
                            eq: "Equal to",
                            isnull: "Empty",
                            gte: "Greater than or equal to",
                            lte: "Less than or equal to"
                        }
                    },
                    ui: function (element) {
                        element.kendoNumericTextBox({
                            format: "n0",
                            value: ""
                        });
                    }
                },
                template: function (dataItem) {
                    var a = parseInt(dataItem.GISID);
                    if (!isNaN(a)) {
                        return a;
                    }
                    return "";
                },
                editable: function (dataItem) {
                    return ($("#userName").data("admin") === "True");
                },
                editor: function (container, options) {
                    var input = $("<input />");
                    input.attr("name", options.field);
                    input.appendTo(container);
                    input.kendoNumericTextBox({
                        value: "",
                        format: "0",
                        spinners: false
                    });
                }
            },

            {
                field: "ModifiedDate",
                title: "Modified",
                template: function (e) {
                    if ((e.ModifiedDate == null) || e.ModifiedDate == "") {
                        return "";
                    }
                    return new Date(e.ModifiedDate).toLocaleDateString('en-US', { year: 'numeric', month: '2-digit', day: '2-digit' });
                },
                editable: false,
                width: 90
            },

        ],
        toolbar: [
            {
                name: "sync",
                text: "Sync",
                className: "k-grid-sync-changes",
                imageClass: "k-icon ob-only-icon k-i-tick"
            }
        ],

        sortable: true,
        editable: true,
        navigatable: true,
        selectable: false,
        filterable: true,
        pageable: {
            input: true,
            numeric: true
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
