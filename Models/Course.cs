using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseContent = new HashSet<CourseContent>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseContent> CourseContent { get; set; }
    }
}
