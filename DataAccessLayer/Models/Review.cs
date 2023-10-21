using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Review : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

}
