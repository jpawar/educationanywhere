using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    
        public string Subject { get; set; }
    }
}
