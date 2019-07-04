using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace Ex3.libs
{
    public class Users
    {
        private static DAL _dal = new DAL("User");

        public static User FindByUser(string nickname)
        {
            OleDbDataReader data = _dal.find("[nickname],[name],[password],[permission],[status],[approver]", $"where nickname='{SQL.EscapeSQLSQ(nickname)}'");
            User t = null;
            try
            {
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        t = new User(data.GetString(0),
                                     data.GetString(1),
                                     data.GetString(2),
                                     data.GetInt32(3),
                                     data.GetBoolean(4),
                                     data.GetString(5));
                    }
                }
            }
            catch(Exception)
            {
                t = null;
            }
            data.Close();
            return t;
        }

        public static bool exists(string nickname)
        {
            return _dal.exists($"where username='{SQL.EscapeSQLSQ(nickname)}'");
        }
    }
}