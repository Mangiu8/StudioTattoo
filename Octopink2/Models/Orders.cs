namespace Octopink2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Details = new HashSet<Details>();
        }

        [Key]
        public int OrderID { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public int UserID_FK { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        public bool Shipped { get; set; }

        [StringLength(100)]
        public string Note { get; set; }

        public DateTime Date { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Details> Details { get; set; }

        public virtual User User { get; set; }
    }
}
