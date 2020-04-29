using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            CourseContent = new HashSet<CourseContent>();
            LessonContent = new HashSet<LessonContent>();
        }

        public int LessonId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseContent> CourseContent { get; set; }
        public virtual ICollection<LessonContent> LessonContent { get; set; }
    }
}
