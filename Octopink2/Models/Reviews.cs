namespace Octopink2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reviews
    {
        [Key]
        public int ReviewID { get; set; }

        public int UserID_FK { get; set; }

        public int ProductID_FK { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
