using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreKendoApplicationService.EntityModels
{
    public class ReferenceType
    {
        [Column("ListOrder")]
        public int Order { get; set; }

        [Column("Active")]
        public bool Active { get; set; }

//        [Required]
        [StringLength(100)]
        [Column("ModifiedBy")]
        public string ModifiedBy { get; set; }

//        [Required]
        [Column("ModifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
