using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoolBox.Models
{
    public class Bool
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string Type { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Duration { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        [Display(Name = "Spotify Playlist")]
        public string SpotifyID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(1)]
        [Required]
        public string Repeat { get; set; }
    }
}
