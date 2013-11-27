using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CourseTutorial
    {
        public Course Course { get; set; }
        public List<Tutorial> Tutorials { get; set; }

        public CourseTutorial()
        {
            Tutorials = new List<Tutorial>();
        }
    }
}
