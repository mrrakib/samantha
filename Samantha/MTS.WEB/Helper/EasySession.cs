using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MTS.Helper
{
    public static class EasySession
    {
        private const string roleId = "RoleId";
        private const string roleName = "RoleName";
        public static string RoleId
        {
            get
            {
                return (string)(HttpContext.Current.Session[roleId] ?? "");
            }

            set
            {
                HttpContext.Current.Session[roleId] = value;
            }
        }
        public static string RoleName
        {
            get
            {
                return (string)(HttpContext.Current.Session[roleName] ?? "");
            }

            set
            {
                HttpContext.Current.Session[roleName] = value;
            }
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
            //HttpContext.Current.Session.Abandon();

        }
    }
}