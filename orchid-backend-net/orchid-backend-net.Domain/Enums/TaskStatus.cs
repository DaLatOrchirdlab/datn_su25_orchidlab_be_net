using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Enums
{
    public enum TaskStatus
    {
        Assign,
        Taken,
        InProcess,
        DoneInTime,
        DoneInLate,
        Cancel,

    }
}
