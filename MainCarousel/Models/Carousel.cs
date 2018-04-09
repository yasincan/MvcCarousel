using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainCarousel.Models
{
    [Table("Carousel")]
    public class Carousel
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public int Sira { get; set; }
        public string AlternateText { get; set; }
        public string Path { get; set; }
        public bool IsActive { get; set; }
    }
}