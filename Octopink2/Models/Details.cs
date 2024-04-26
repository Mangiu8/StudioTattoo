namespace Octopink2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Details
    {
        public int DetailsID { get; set; }

        public int ProductID_FK { get; set; }

        public int OrderID_FK { get; set; }

        public int? Quantity { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Product Product { get; set; }

        public virtual Product Product1 { get; set; }
    }
}
