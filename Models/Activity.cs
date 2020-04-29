using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class Activity
    {
        public Activity()
        {
            LessonContent = new HashSet<LessonContent>();
        }

        public int ActivityId { get; set; }
        public string ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string Data { get; set; }

        public virtual ActivityType ActivityType { get; set; }
        public virtual ICollection<LessonContent> LessonContent { get; set; }
    }
}
