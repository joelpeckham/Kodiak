using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class CourseContent
    {
        public int CourseId { get; set; }
        public int LessonId { get; set; }
        public int Position { get; set; }

        public virtual Course Course { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
