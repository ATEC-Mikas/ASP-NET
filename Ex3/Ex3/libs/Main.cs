using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.libs
{
    public enum Permission
    {
        Reader = 0,
        Writer = 1,
        Reviewer = 2,
        Administrador = 3
    }
    public static class SQL
    {
        /// <summary>
        /// Fazer Escape de SQL
        /// </summary>
        /// <param name="s">String a dar escape</param>
        /// <returns>String escapada</returns>
        public static string EscapeSQL(string s)
        {
            return s.Replace("'", "''").Replace("%", "[%]");
        }

        /// <summary>
        /// Fazer Escape de SQL, mas somente os "'"
        /// </summary>
        /// <param name="s">String a dar escape</param>
        /// <returns>String escapada</returns>
        public static string EscapeSQLSQ(string s)
        {
            return s.Replace("'", "''");
        }
    }
}