using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter.API.Enums
{
    public enum TalkProcessTask
    {
        None = 0,
        Manager = 1,
        CheckPass = 2,
        ReturnData = 3,
        CheckAdviser = 4,
        AttachToAdviser = 5,
        Conversation = 6
    }
}
