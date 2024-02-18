using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace EveMailHelper.Web.Models
{
    public static class IskDoubleToString
    {
        public static string ToISKString(this double value, bool withISKpostix = false)
        {
            var result = $"{value:#,##0.00}";
            if (withISKpostix)
                return result + " ISK";
            return result;
        }

        public static string ToPercentString(this double value, bool withPercentpostfix = false)
        {
            var result =  $"{value:0.00}";
            if (withPercentpostfix)
                return result + "%";
            return result;
        }
    }
}
