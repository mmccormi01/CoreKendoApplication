/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {

 /*   setHelloWorldText();*/

 //   createKendoGrid();
 //   alert("...kendo grid");
    createResourceGrid();
//    alert("..resource grid");
});

/*
 END all functions (like click-handlers) that need to occur at page load here
*/

/*
 START all load-independent functions
*/
function createResourceGrid() {
    var resourceReadUrl = $("#resourceGrid").data("readResourcesUrl");
    var classesUrl = $("#resourceGrid").data("readClassesUrl");

   // alert(classesUrl);
    var classDataSource = new kendo.data.DataSource({
        transport: {
            read: {
                type: "GET",
                dataType: "json",
                url: classesUrl,
                cache: false
            }
        }
    });

    var resourceGrid = $("#resourceGrid").kendoGrid({
        dataSource: {
            transport: {
                read: {
                    type: "GET",
                    dataType: "json",
                    url: resourceReadUrl,
                    cache: false
                },
                //update: {
                //    type: "POST",
                //    dataType: "json",
                //    url: resourceUpdateUrl,
                //    cache: false
                //},
                //create: {
                //    type: "POST",
                //    dataType: "json",
                //    url: resourceCreateUrl,
                //    cache: false
                //}
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
                        //       DocumentFilePath: {},
                    }
                }
            },
            //filter: applyFilters(),
            //requestEnd: function (e) {
            //    if (e.type !== "read") {
            //        this.read();
            //    }
            //},
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
            },
            {
                field: "ResourceTypeName",
                title: "Resource Type",
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

                //filterable: {
                //    extra: false,
                //    operators: {
                //        number: {
                //            eq: "Equal to",
                //            isnull: "Empty",
                //            gte: "Greater than or equal to",
                //            lte: "Less than or equal to"
                //        }
                //    },
                //    ui: function (element) {
                //        element.kendoNumericTextBox({
                //            format: "n0",
                //            value: ""
                //        });
                //    }
                //},
                //template: function (dataItem) {
                //    var a = parseInt(dataItem.GISID);
                //    if (!isNaN(a)) {
                //        return a;
                //    }
                //    return "";
                //},
                editable: function (dataItem) {
                    return ($("#userName").data("admin") === "True");
                },
                //editor: function (container, options) {
                //    var input = $("<input />");
                //    input.attr("name", options.field);
                //    input.appendTo(container);
                //    input.kendoNumericTextBox({
                //        value: "",
                //        format: "0",
                //        spinners: false
                //    });
                //}
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
            { template: kendo.template($("#toolBarTemplateWrite").html()) }
        ],
        autoBind: false,
        filterable: true,
        scrollable: false,
        sortable: true,
        dataBound: onDataBound,
        detailTemplate: kendo.template($("#resourcedetailtemplate").html()),

        detailInit: function (element) {
            var ID = element.data.id;
            var resourceId = element.data.ResourceId.toString().trim().toLowerCase();
            var detailRow = element.detailRow;
            detailRow.find(".resourcedesc")[0].id = "resourcedesc" + resourceId;
            detailRow.find(".resourceclasslist")[0].id = "resourceclasslist" + resourceId;
            detailRow.find(".primarysite")[0].id = "primarysite" + resourceId;

            if ((resourceId !== null) && (resourceId !== "")) {

                $("#resourcedesc" + resourceId).val(element.data.ResourceDescription);
                $("#resourceclasslist" + resourceId).val(element.data.ResourceClassName);
                $("#primarysite" + resourceId).val(element.data.PrimaryASMSite);
            }
            detailRow.find(".resourcetabstrip")[0].id = "resourcetabstrip" + resourceId;
            $("#resourcetabstrip" + resourceId).kendoTabStrip({
                dataTextField: "Name"
            });


            detailRow.find(".resourceclasslist").kendoDropDownList({
                dataSource: classDataSource,
                dataTextField: "Name",
                dataValueField: "Id",
                value: element.data.ResourceClassId,
                enable: true,
                select: function (e) {
                    if (!e.dataItem.Active) {
                        removeFromDropDown("#resourceclasslist" + resourceId, e.dataItem.Id);
                        e.preventDefault();
                    }
                },
                dataBound: function (e) {
                    removeFromDropDown("#resourceclasslist" + resourceId);
                },
                template: kendo.template($("#dropDownTemplate").html())
            });
        },
        edit: function (e) {
            e.sender.expandRow(e.container);
            var resourceId = e.model.ResourceId.toString().trim().toLowerCase();
            // toggleEditAgencies(resourceId);
            //setTimeout(function () {
            //    $("button[value='" + resourceId + "']").prop('disabled', true);
            //    $("#addAgencyAssociations" + resourceId)[0].disabled = false;
            //}, 800);
            var tabStrip = $("#resourcetabstrip" + resourceId).data("kendoTabStrip");

            tabStrip.select(0);
            tabStrip.disable(tabStrip.tabGroup.children().eq(1));
            tabStrip.disable(tabStrip.tabGroup.children().eq(2));
            tabStrip.disable(tabStrip.tabGroup.children().eq(3));

            $("#resourceclasslist" + resourceId).data("kendoDropDownList").enable(true);
            //$("#projectregcon" + projectID).data("kendoDropDownList").enable(true);
            //$("#projectpath" + projectID).removeAttr("hidden");
            //$(".projectlink" + projectID).hide();
            //$("#projectnotes" + projectID).removeAttr("readonly");
            //$("#fiscalYearInput" + projectID).removeAttr("hidden");
            //$("#fiscalYearText" + projectID).hide();
            //$("#clearanceDocPath" + projectID).removeAttr("hidden");
            //$(".clearanceDocLink" + projectID).hide();
            //$("#complianceLevel" + projectID).data("kendoDropDownList").enable(true);
            //$("#sponsor" + projectID).data("kendoDropDownList").enable(true);
            //$("#countyDepartment" + projectID).data("kendoDropDownList").enable(true);

            $("tr[data-uid='" + e.model.uid + "'] .k-hierarchy-cell").html("");
        },
    });

    var resourceDataSource = resourceGrid.data("kendoGrid").dataSource;
     // alert(JSON.stringify(resourceDataSource));


    //Populate all dataSources on page load in sequence instead of while navigating the grid and filter grid data.
     alert("hit read...");
    classDataSource.read();
    // alert("hit read success...");
    resourceDataSource.read();
        //.then(function () {
        //    //if (resourceDataSource.total() == 0) {
        //    //    resourceDataSource.filter([{ field: "Completed", operator: "eq", value: "True" }]);
        //    //}
        //    //alert("hit read...");
        //    //classDataSource.read();
        //    //.then(function () {
        //    //    managerDataSource.read();
        //});

}


function toggleEditAgencies(projectID) {
    setTimeout(function () {
        var agencyRadios = $("input[type='radio'][name='" + projectID + "']");
        for (var i = 0; i < agencyRadios.length; i++) {
            agencyRadios[i].disabled = false;
        }
        var agencyDeleteButtons = $('button[name="' + projectID + '"]');
        for (var i = 0; i < agencyDeleteButtons.length; i++) {
            agencyDeleteButtons[i].disabled = false;
        }
    }, 800);
}

function onDataBound(e) {

    var grid = e.sender;

    // Grab the value of projectId from the query string
    var resourceId = getParameterByName("resourceId");
    //alert("HIT");
    //if (resourceId != null) {
    //    // disable existing grid filters
    //    //grid.sender.dataSource.filter = [];

    //    // select that row in the grid
    //    var item = grid.dataSource.get(resourceId);
    //    var tr = $("[data-uid='" + item.uid + "']", grid.tbody);
    //    alert(tr);
    //    grid.select(tr);
    //}

    //stopLoading("#allcontent");
}

function getParameterByName(queryParamName) {
    var match = RegExp('[?&]' + queryParamName + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}
/*
 END all load-independent functions
*/
