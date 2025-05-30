using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace orchid_backend_net.Domain.Constants
{
    public static class UserRole
    {
        public static readonly Dictionary<int, string> UserDictionary = new Dictionary<int, string>
        {
            {1, "Admin" },
            {2, "Researcher" },
            {3, "Technician" }
        };
    }
}
