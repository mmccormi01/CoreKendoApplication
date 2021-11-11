using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    [Table("Resource_Main")]
    public partial class Resource
    {
        [Key]
        public int ResourceId { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; }
        public string ResourceDescription { get; set; }

        public int? ResourceTypeId { get; set; }
        public int? ResourceClassId { get; set; }
        public int? ResourceAreaId { get; set; }
        public int? DesignationStatusId { get; set; }
        public int? YearDesignated { get; set; }
        public string GISId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string PrimaryASMSiteNumber { get; set; }
        public int? ParentDistrictId { get; set; }
        public int? ParentSensitivityZoneId { get; set; }
        public string FolderPath { get; set; }
        public int? CurrentThreatId { get; set; }
        public int? SignificanceStart { get; set; }
        public int? SignificanceEnd { get; set; }
        public DateTime? SignificanceDate { get; set; }
        public string SignificanceNotes { get; set; }
        public int? IntegritiyScoreId { get; set; }
        public int? ResearchScoreId { get; set; }
        public int? RarityScoreId { get; set; }
        public int? EducationScoreId { get; set; }
        public int? ValueTypeId { get; set; }
        public int? ValueScoreId { get; set; }
        public bool SiteMonitored { get; set; }
        public DateTime? MonitoredDate { get; set; }
        public string ReportByName { get; set; }
        public string ReportCondition { get; set; }
        public string ObservedImpacts { get; set; }
        public DateTime? CountyReportingDate { get; set; }
        public int? CountyReportingTypeId { get; set; }
        public string CountyReportingNotes { get; set; }
        public int? SiteYear { get; set; }
        public int? SiteReportTypeId { get; set; }
        public string SiteCitation { get; set; }
        public DateTime? BoundaryModificationDate { get; set; }
        public string BoundaryModificationNotes { get; set; }

    }

}
