using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.libs
{
    public class User
    {
        private string _nickname;
        private string _name;
        private string _password;
        private Permission _permission;
        private bool _status;
        private string _approver;

        private bool _nameChanged = false;
        private bool _passwordChanged = false;
        private bool _permissionChanged = false;
        private bool _statusChanged = false;
        private bool _approverChanged = false;

        public User()
        {
            _nickname = "";
            _name = "";
            _password = "";
            _permission = Permission.Reader;
            _status = false;
            _approver = "";
        }

        public User(string nickname, string name, string password, int permission, bool status, string approver)
        {
            _nickname = nickname;
            _name = name;
            _password = password;
            _permission = (Permission)permission;
            _status = status;
            _approver = approver;
        }

        public string Nickname
        {
            get { return _nickname; }
            set { if (validarNickname(value)) { _nickname = value;}}
        }

        public string Name
        {
            get { return _name; }
            set { if (validarName(value)) { _name = value; _nameChanged = false; } }
        }

        public string Password
        {
            get { return _password; }
            set { if (validarPassword(value)) { _password = BCrypt.Net.BCrypt.HashPassword(value); _passwordChanged = true; } }
        }

        public Permission Permission
        {
            get { return _permission; }
            set { _permission = value; _permissionChanged = true; }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; _statusChanged = true; }
        }

        public string Approver
        {
            get { return _approver; }
            set { _approver = value; _approverChanged = true; }
        }

        private bool validarPassword(string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value); 
        }

        private bool validarName(string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value);
        }

        private bool validarNickname(string value)
        {
            return string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, _password);
        }

        public bool save()
        {
            DAL dal = new DAL("User");
            List<KeyValuePair<string, object>> kv = new List<KeyValuePair<string, object>>();
            if (_nameChanged)
                kv.Add(new KeyValuePair<string, object>("name", SQL.EscapeSQLSQ(_name) ));
            if (_passwordChanged)
                kv.Add(new KeyValuePair<string, object>("password", SQL.EscapeSQLSQ(_password) ));
            if (_permissionChanged)
                kv.Add(new KeyValuePair<string, object>("permission", (int)_permission));
            if (_statusChanged)
                kv.Add(new KeyValuePair<string, object>("status", _status));
            if (_approverChanged)
                kv.Add(new KeyValuePair<string, object>("approver", SQL.EscapeSQLSQ(_approver) ));

            if (dal.exists($"where nickname = '{SQL.EscapeSQLSQ(_nickname)}'"))
            {
                return dal.update(kv, $"where username = '{SQL.EscapeSQLSQ(_nickname)}'");
            }
            else
            {
                kv.Add(new KeyValuePair<string, object>("username", SQL.EscapeSQLSQ(_nickname)));
                return dal.insert(kv);
            }
        }

        public bool delete()
        {
            DAL dal = new DAL("User");
            if (dal.exists($"where nickname = '{SQL.EscapeSQLSQ(_nickname)}'"))
                return dal.delete($"where nickname = '{SQL.EscapeSQLSQ(_nickname)}'");
            return false;
        }
    }
}