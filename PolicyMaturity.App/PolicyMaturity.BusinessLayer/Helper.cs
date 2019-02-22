using System;
using PolicyMaturity.Contracts;

namespace PolicyMaturity.BusinessLayer
{
    static class Helper
    {
        public static bool BooleanAliasesHelper(string membership)
        {
            return Convert.ToBoolean(Enum.Parse(typeof(BooleanAliases), membership));
        }
    }
}
