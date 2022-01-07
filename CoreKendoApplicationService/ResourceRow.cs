using System;
using System.Collections.Generic;
using System.Text;

namespace CoreKendoApplicationService
{
    public class ResourceRow
    {
        //Header Fields
        public int ResourceId { get; set; }
        public string ResourceName { get; set; }

        public int? YearDesignated { get; set; }
        public int? ResourceTypeId { get; set; }
        public string ResourceTypeName { get; set; }
        public int? DesignationStatusId { get; set; }
        public string DesignationStatusName { get; set; }
        public string GISId { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //Detail Fields
        public string ResourceDescription { get; set; }
        public int? ResourceClassId { get; set; }
        public string ResourceClassName { get; set; }
        public string PrimaryASMSiteNumber { get; set; }
        public int? ParentDistrictId { get; set; }
        public string ParentDistrictName { get; set; }
        public int? ParentSensitivityZoneId { get; set; }
        public string ParentSensitivityZoneName { get; set; }

        public int? ParentResourceAreaId { get; set; }
        public string ResourceFolderPath { get; set; }

        //Multi values
      //  public List<int> ResourceClasses;
        public List<int> AssociatedSites;
        public List<int> TemporalClasses;
        public List<int> HistoricFunctions;
        public List<int> ArchitecturalStyles;
        public List<int> NRHPCriteria;
        public List<int> SignificanceAreas;

        //Management
        public int CurrentThreatValue { get; set; }
        public DateTime? ManagementActionDate { get; set; }
        public int ManagementTypeId { get; set; }
        public string ManagementTypeName { get; set; }
        public string ManagementNotes { get; set; }

        //Significance
        public DateTime? SignificanceStart { get; set; }
        public DateTime? SignificanceEnd { get; set; }

        public int IntegrityScoreId { get; set; }
        public int ResearchScoreId { get; set; }
        public int RarityScoreId { get; set; }
        public int EducationScoreId { get; set; }

        public DateTime? SignificanceDate { get; set; }
        public int ValueTypeId { get; set; }
        public int ValueScoreId { get; set; }
        public string SignificanceNotes { get; set; }

        //Site Stewards
        public bool SiteMonitored;
        public DateTime? MonitoredDate { get; set; }
        public string ReportedByName { get; set; }
        public string ReportedCondition { get; set; }
        public string ObservedImpacts { get; set; }
        public string SiteMonitoredNotes { get; set; }

        //County Reporting

        public DateTime? CountyReportingDate { get; set; }
        public int CountyReportTypeId { get; set; }
        public string CountyReportNotes { get; set; }

        //Site History
        public int SiteYear;
        public int SiteReportTypeId { get; set; }
        public string SiteCitation { get; set; }

        //Boundary Modification
        public DateTime? BoundaryModificationDate { get; set; }
        public string BoundaryModificationNotes { get; set; }

    }
}
