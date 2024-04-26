namespace Octopink2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Artist")]
    public partial class Artist
    {
        public int ArtistID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Aka { get; set; }

        [Required]
        public string Descrizione { get; set; }
        [StringLength(100)]
        public string InstaLink { get; set; }

        [StringLength(50)]
        public string Foto1 { get; set; }

        [StringLength(50)]
        public string Foto2 { get; set; }
        [StringLength(50)]

        public string Foto3 { get; set; }
        [StringLength(50)]

        public string Foto4 { get; set; }
        [StringLength(50)]

        public string Foto5 { get; set; }
        [StringLength(50)]

        public string Foto6 { get; set; }
        [StringLength(50)]

        public string Foto7 { get; set; }
        [StringLength(50)]

        public string Foto8 { get; set; }
        [StringLength(50)]

        public string Foto9 { get; set; }
        [StringLength(50)]

        public string Foto10 { get; set; }
    }
}
