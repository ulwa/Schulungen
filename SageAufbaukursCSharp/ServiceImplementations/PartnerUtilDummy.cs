using SageAufbaukursCSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SageAufbaukursCSharp.ServiceImplementations
{
    public class PartnerUtilDummy : IPartnerUtil
    {
        public bool TestState { get; set; }

        public bool IsConnected {
            get
            {
                return TestState;
            }
        }
    }
}
