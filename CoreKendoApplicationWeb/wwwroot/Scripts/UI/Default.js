/*
 START all functions (like click-handlers) that need to occur at page load here
*/

$(document).ready(function () {

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
    var resourceUpdateUrl = $("#resourceGrid").data("updateResourceUrl");
    var resourceCreateUrl = $("#resourceGrid").data("createResourceUrl");

    var classesUrl = $("#resourceGrid").data("readClassesUrl");

    //alert(classesUrl);
    //alert(resourceUpdateUrl);
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
                update: {
                    type: "POST",
                    dataType: "json",
                    url: resourceUpdateUrl,
                    cache: false
                },
                create: {
                    type: "POST",
                    dataType: "json",
                    url: resourceCreateUrl,
                    cache: false
                }
            },
            autoSync: false,
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
                        ResourceClasses: { multi: true},
                        DesignatedStatus: {},
                        ModifiedDate: { editable: false },
                        GISId: {
                            type: "text",
                            //validation: {
                            //    noDuplicates: function (input) {
                            //        var gridData = $("#resourceGrid").data("kendoGrid").dataSource.data();
                            //        var validator = this;
                            //        var noMatchFound = true;
                            //        if (input.is("[name=GISId]") && (input.val() !== "")) {
                            //            input.attr("data-noduplicates-msg", "Duplicate GIS IDs are not allowed.");
                            //            var gridCnt = 0;
                            //            while (gridCnt < gridData.length && noMatchFound) {
                            //                if ((gridData[gridCnt].GISID == input.val() && input.val() != 0) && !gridData[gridCnt].dirty && (gridData[gridCnt].ProjectID != input[0].kendoBindingTarget.source.ProjectID)) {
                            //                    noMatchFound = false;
                            //                }

                            //                gridCnt += 1;
                            //            }
                            //        }

                            //        return noMatchFound;
                            //    }
                            //}
                        },
                        //       DocumentFilePath: {},
                    }
                }
            },
            
            //filter: applyFilters(),
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
                //format: "{0:n0}",
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
            {
                command: [
                    {
                        name: "edit",
                        text: "Edit"
                    },
                    {
                        name: "deactivate",
                        text: "Deactivate",
                        //click: function (e) {
                        //    alert("click...");
                        //    e.preventDefault();
                        //    var tr = $(e.target).closest("tr");
                        //    var data = this.dataItem(tr);
                        //    var windowTemplate = kendo.template($("#confirmDeactivation").html());
                        //    var window = $("#confirmDeactivationDiv").kendoWindow({
                        //        title: "Deactivation Confirmation",
                        //        visible: false,
                        //        width: "400px",
                        //        height: "180px",
                        //    }).data("kendoWindow");
                        //    window.content(windowTemplate(data));
                        //    window.center().open();
                        //    $("#yesButton").click(function () {
                        //        startLoading("#allcontent");
                        //        $.ajax({
                        //            contentType: 'application/json; charset=utf-8',
                        //            type: "GET",
                        //            dataType: "json",
                        //            url: projectDeactivateUrl + "?projectID=" + data.ProjectID,
                        //            contentType: "application/json",
                        //            cache: false,
                        //            success: function () {
                        //                stopLoading("#allcontent");
                        //                $("#projectgrid").data("kendoGrid").dataSource.read();
                        //            }
                        //        });
                        //        window.close();
                        //    });
                        //    $("#noButton").click(function () {
                        //        window.close();
                        //    });
                        //}
                    }],

                title: "&nbsp",
                width: 100,
                hidden: $("#userName").data("read") === "True"

            }],

        editable: false,
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
            alert("detail init....");
            alert(element.data.TemporalClasses);
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


            detailRow.find(".resourceclasslist").kendoListBox({
                dataSource: classDataSource,
                dataTextField: "Name",
                dataValueField: "Id",
                value: element.data.TemporalClasses,
                enable: false,
              //  edit: true,
                select: function (e) {
                    alert("testing active...");
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
        //beforeEdit: function (e) {
        //    alert("hit before edit...");
        //    //if (e.model.isNew()) {
        //    //    e.model.ProjectMgr = $("#userName").text();
        //    //}
        //},
        edit: function (e) {
            alert("hit edit function..." + e.model.uid);
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

            $("#resourceclasslist" + resourceId).data("kendoListBox").enable(true);
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
        save: function (e) {
            alert("hit save...");
          //  alert(e.model.ResourceClassId);
            //alert(e.model.ResourceClassName);
            //alert(e.model.ResourceId);
            var classlist = $("#resourceclasslist" + e.model.ResourceId).data("kendoMultiSelect");
            alert("value: " + classlist.value());
            e.model.ResourceClassId = classlist.value();
   
            alert("updating resource class..." + e.model.ResourceClassId);
         //   e.model.ResourceClassName = classlist.text();
            // The first 4 chars has to be #s from 0 - 9
            // The middle char is a dash, -
            // The last 4 chars has to be #s from 0 - 9
            //var fyInput = $("#fiscalYearInput" + e.model.ProjectID).val().toUpperCase();
            //var regex = new RegExp(/(^[0-9]{4}-[0-9]{4}$)/m);
            //var fiscalCorrectFormat = regex.test(fyInput) || fyInput == "PENDING" || fyInput == "" || fyInput == null || fyInput == 'undefined';
            //if (!fiscalCorrectFormat) {
            //    $("#fiscalYearError" + e.model.ProjectID).show();
            //    e.preventDefault();
            //} else {
            //    if (e.model.GISID == 0) {
            //        e.model.GISID = "";
            //    }
            //    var typelist = $("#projecttypelist" + e.model.ProjectID).data("kendoDropDownList");
            //    var complianceLevelList = $("#complianceLevel" + e.model.ProjectID).data("kendoDropDownList");
            //    var sponsorList = $("#sponsor" + e.model.ProjectID).data("kendoDropDownList");
            //    var departmentList = $("#countyDepartment" + e.model.ProjectID).data("kendoDropDownList");
            //    var regulatoryConList = $("#projectregcon" + e.model.ProjectID).data("kendoDropDownList");

            //    e.model.ProjectStartDate = e.model.ProjectStartDate.toUTCString();
            //    if (e.model.ProjectEndDate != null) {
            //        e.model.ProjectEndDate = e.model.ProjectEndDate.toUTCString();
            //    }
            //    $("button[value='" + e.model.ProjectID + "']").prop('disabled', false);

            //    e.model.ProjTypeId = typelist.value();
            //    e.model.ProjType = typelist.text();
            //    e.model.SponsorID = sponsorList.value();
            //    e.model.Sponsor = sponsorList.text();
            //    e.model.ComplianceLevelID = complianceLevelList.value();
            //    e.model.ComplianceLevel = complianceLevelList.text();
            //    e.model.CountyDepartmentID = departmentList.value();
            //    e.model.CountyDepartment = departmentList.text();
            //    e.model.RegulatoryConsultationID = regulatoryConList.value();
            //    e.model.RegulatoryConsultation = regulatoryConList.text();

            //    e.model.DocumentFilePath = $("#projectpath" + e.model.ProjectID).val();
            //    e.model.ProjNote = $("#projectnotes" + e.model.ProjectID).val();
            //    e.model.FiscalYear = fyInput;
            //    e.model.CommentsnDocument = $("#clearanceDocPath" + e.model.ProjectID).val();
            //    e.model.dirty = true;

            //    if (e.model.id == null || e.model.id == "") {
            //        e.sender.dataSource.filter({
            //            logic: "and",
            //            filters: [
            //                { field: "ProjectMgr", operator: "eq", value: $("#userName").text() },
            //                { field: "Completed", operator: "eq", value: "True" }
            //            ]
            //        });
            //    }
            //}
        },
        cancel: function (e) {
            e.sender.dataSource.read();
        },
    });

 
  //  alert($("#userName").data("admin"));
    if ($("#userName").data("read") === "False") {
     //   alert("hit user eval...");
        resourceGrid.data("kendoGrid").setOptions({
            toolbar: [
                { template: kendo.template($("#toolBarTemplateWrite").html()) }
            ],
            editable: "inline"
        });
    }

    var resourceDataSource = resourceGrid.data("kendoGrid").dataSource;
     // alert(JSON.stringify(resourceDataSource));


    //Populate all dataSources on page load in sequence instead of while navigating the grid and filter grid data.
    resourceDataSource.read()
        .then(function () {
            //if (resourceDataSource.total() == 0) {
            //    resourceDataSource.filter([{ field: "Completed", operator: "eq", value: "True" }]);
            //}
          //  alert("hit class read...");
            classDataSource.read();
            //.then(function () {
            //    managerDataSource.read();
        });

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
   // alert("HIT databound");
    //alert(resourceId);
    if (resourceId != null) {
        // disable existing grid filters
        //grid.sender.dataSource.filter = [];

        // select that row in the grid
        var item = grid.dataSource.get(resourceId);
        var tr = $("[data-uid='" + item.uid + "']", grid.tbody);
/*        alert(tr);*/
        grid.select(tr);
    }

    //stopLoading("#allcontent");
}

function getParameterByName(queryParamName) {
    var match = RegExp('[?&]' + queryParamName + '=([^&]*)').exec(window.location.search);
    return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
}
/*
 END all load-independent functions
*/
