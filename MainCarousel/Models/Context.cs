using System.Data.Entity;

namespace MainCarousel.Models
{
    public class Context : DbContext
    {
        public Context() : base("Server=.;Database=MainCarouselApp;Trusted_Connection=True;")
        {

        }
        public DbSet<Carousel> carousel { get; set; }
    }
}