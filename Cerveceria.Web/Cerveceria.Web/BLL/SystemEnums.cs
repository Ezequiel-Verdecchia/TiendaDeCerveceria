using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.BLL
{
    public class SystemEnums
    {
        public enum Roles
        {
            Admin = 0,
            Cliente = 1
        }
        public class Constantes
        {
            public const string Section = "appSettings";
            public const string EmailOrigen = "EmailOrigen";
            public const string Password = "Password";
            public const string Path = @"BLL\Properties\mailAppSettings.json";
        }
        
    }
}
