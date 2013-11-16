using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Tutorial
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int CourseGradeId { get; set; }

        [NotMapped]
        public string Grade { get; set; }

        [NotMapped]
        public string Subject { get; set; }

        public string FullFilePath { get; set; }
    }
}
