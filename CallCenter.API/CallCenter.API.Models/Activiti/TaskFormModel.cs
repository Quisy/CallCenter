using System.Collections.Generic;

namespace CallCenter.API.Models.Activiti
{
    public class TaskFormModel
    {
        public string TaskId { get; set; }
        public IList<TaskFormProperty> Properties { get; set; }
    }
}
