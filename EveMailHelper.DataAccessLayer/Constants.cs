using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveMailHelper.DataAccessLayer
{
    internal static partial class Constants
    {
        public const string SCHEMA_SDE = "Sde";
        public const int SIZE_TEXT = 100;
        public const int SIZE_NAME = 150;
        public const int SIZE_FILENAME = 200;
        public const int SIZE_DESCRIPTION = 1000;
        public const int SIZE_SHORTDESCRIPTION = 500;
        public const int SIZE_TEXT_MAX = 4096; // more than 4000 for mssql
    }
}
