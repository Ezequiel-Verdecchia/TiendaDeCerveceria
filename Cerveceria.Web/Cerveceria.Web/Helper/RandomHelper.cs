using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cerveceria.Web.Helper
{
    public static class RandomHelper
    {
        public static string Random()
        {
            Random obj = new Random();
            string posibles = "0123456789";
            int longitud = posibles.Length;
            char letra;
            int longitudnuevacadena = 6;
            string nuevacadena = "";
            for (int i = 0; i < longitudnuevacadena; i++)
            {
                letra = posibles[obj.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }
    }
}
