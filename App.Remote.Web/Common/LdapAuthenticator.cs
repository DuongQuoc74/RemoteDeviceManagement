using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace Jabil.Pico.Web.Common
{
    public static class LdapAuthenticator
    {
        private static readonly string LDAP_FILTER = "(SAMAccountName={0})";

        public static bool NovellAuthenticate(
          string Username,
          string Password)
        {
            try
            {
                DirectoryEntry searchRoot = new DirectoryEntry(ConfigurationManager.AppSettings["Jabil.Settings.LDAP_HOST"], ConfigurationManager.AppSettings["Jabil.Settings.LDAP_DOMAIN"] + "\\" + Username, Password);
                new DirectorySearcher(searchRoot)
                {
                    Filter = string.Format(LdapAuthenticator.LDAP_FILTER, (object)Username),
                    PropertiesToLoad = {
                      "cn"
                    }
                }.FindOne();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool NewAuthenticate(
          string Username,
          string Password)
        {
            try
            {
                using (var cn = new LdapConnection())
                {
                    cn.Connect(ConfigurationManager.AppSettings["Jabil.Settings.LDAP_HOST"], int.Parse(ConfigurationManager.AppSettings["Jabil.Settings.LDAP_PORT"]));
                    cn.Bind(string.Format(ConfigurationManager.AppSettings["Jabil.Settings.LDAP_DOMAIN"], Username), Password);
                    return cn.Bound;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}