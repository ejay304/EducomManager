namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.invoices")]
    public partial class invoice
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime creation_date { get; set; }

        public float? amount_issued_chf { get; set; }

        public float? amount_issued_foreign { get; set; }

        public float? amount_received_chf { get; set; }

        public float? amount_received_foreign { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string foreign_type { get; set; }

        public int programs_id { get; set; }

        public virtual program program { get; set; }
    }
}
