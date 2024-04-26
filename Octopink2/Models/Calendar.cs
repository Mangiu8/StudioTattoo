namespace Octopink2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Calendar")]
    public partial class Calendar
    {
        [Key]
        public int AppointmentID { get; set; }

        public int UserID_FK { get; set; }

        public string Notes { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual User User { get; set; }
    }
}
