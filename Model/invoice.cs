using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace PrototypeEDUCOM.Model
{

    /// <filename>Invoice.cs</filename> 
    /// <author>Alain FRESCO</author> 
    /// <author>Romain THERISOD</author> 
    /// <date>01/08/2014 </date> 
    /// <summary>
    ///     Classe de type Model, représentant les données relatives aux factures.
    ///     Générée avec EntityFramework comme couche d'abstraction avec la base de données.
    /// </summary>

    [Table("EducomDb.invoices")]
    public partial class Invoice
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

        public virtual Program program { get; set; }
    }
}
