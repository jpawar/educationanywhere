using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Subject { get; set; }

        [NotMapped]
        public string Grade { get; set; }

    }
}
