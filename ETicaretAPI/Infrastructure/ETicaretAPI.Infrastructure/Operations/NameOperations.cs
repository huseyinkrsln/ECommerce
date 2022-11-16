﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Operations
{

    /*Class'ın amacı→
     * Seo için oluşturulmuş sınıf
     */


    public static class NameOperations
    {
        public static string CharacterRegulatory(string name)

            => name.Replace("\"", "")
               .Replace("!", "")
               .Replace("'", "")
               .Replace("^", "")
               .Replace("+", "")
               .Replace("%", "")
               .Replace("&", "")
               .Replace("/", "")
               .Replace("(", "")
               .Replace(")", "")
               .Replace("=", "")
               .Replace("_", "")
               .Replace("@", "")
               .Replace("£", "")
               .Replace("~", "")
               .Replace("€", "")
               .Replace("`", "")
               .Replace(",", "")
               .Replace(";", "")
               .Replace(".", "-")
               .Replace("<", "")
               .Replace(">", "")
               .Replace("|", "")
               .Replace("Ö", "o")
               .Replace("ö", "o")
               .Replace("Ü", "u")
               .Replace("ü", "u")
               .Replace("ı", "i")
               .Replace("İ", "i")
               .Replace("ğ", "g")
               .Replace("Ğ", "g")
               .Replace("ğ", "g")
               .Replace("Ş", "S")
               .Replace("ş", "s")
               .Replace("Ç", "C")
               .Replace("ç", "c");

    }
}
