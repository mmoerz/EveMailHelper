using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace at.gv.bmi.bk.Factotum.BusinessLogicLibrary.Utilities
{
    static class BuildInformation
    {
        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(0, 0);
    }
}
