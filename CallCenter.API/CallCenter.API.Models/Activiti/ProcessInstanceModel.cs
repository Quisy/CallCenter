using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Models.Activiti
{
    public class ProcessInstanceModel
    {
        public int Id { get; set; }
        public string BusinessKey { get; set; }
        public string ProcessDefinitionId { get; set; }
        public string ActivityId { get; set; }
        public bool Suspended { get; set; }
        public bool Ended { get; set; }
        public bool Completed { get; set; }
    }
}
