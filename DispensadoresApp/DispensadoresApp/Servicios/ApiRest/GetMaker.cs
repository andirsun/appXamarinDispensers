using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DispensadoresApp.Servicios.ApiRest
{
    public class GetMaker
    {
        public string Replace(string url, string[] contenido)
        {
            
            var regex = new Regex(Regex.Escape("*"));
            for (int i = 0; i < contenido.Length; i++)
            {
                url = regex.Replace(url, contenido[i], 1);
            }
            return url;
        }
    }
}
