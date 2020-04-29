using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class LessonContent
    {
        public int LessonId { get; set; }
        public int ActivityId { get; set; }
        public int Position { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
