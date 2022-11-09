using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

namespace EveMailHelper.ServiceLayer.Utilities
{
    public static class PreRunChecks
    {
        public static void CheckIfMainConfigItemExists(this WebApplicationBuilder builder, string configItemName)
        {
            _ = builder.Configuration[configItemName] ?? throw new Exception($"appsettings.json is missing {configItemName}");
        }
    }
}
