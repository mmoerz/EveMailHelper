using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace at.gv.bmi.bk.Factotum.BusinessLogicLibrary.Tools
{
    static class BuildInformation
    {
        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;
    }
}
