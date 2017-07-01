using System;
using System.CodeDom;
using CallCenter.API.Enums;

namespace CallCenter.API.Models.Activiti
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProcessInstanceId { get; set; }

        public TalkProcessTask ProcessTask
        {
            get
            {
                var result = Enum.TryParse(Name, out TalkProcessTask task);

                if (result)
                    return (TalkProcessTask) Enum.Parse(typeof(TalkProcessTask), Name, true);
                else
                    return TalkProcessTask.None;
            }
        }
    }
}
