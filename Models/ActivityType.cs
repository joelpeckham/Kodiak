using System;
using System.Collections.Generic;

namespace kodiak.Models
{
    public partial class ActivityType
    {
        public ActivityType()
        {
            Activity = new HashSet<Activity>();
        }

        public string ActivityTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
    }
}
