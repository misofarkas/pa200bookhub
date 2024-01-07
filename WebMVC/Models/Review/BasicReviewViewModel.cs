using Microsoft.Data.SqlClient.DataClassification;

namespace WebMVC.Models.Review
{
    public class BasicReviewViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int CustomerId { get; set; }
        public string CustomerUsername { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
