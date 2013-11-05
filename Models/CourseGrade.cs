using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CourseGrade
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string Grade { get; set; }
    }
}
