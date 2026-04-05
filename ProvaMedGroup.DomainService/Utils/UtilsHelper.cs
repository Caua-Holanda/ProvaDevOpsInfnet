using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProvaMedGroup.DomainService.Utils
{
    public static class UtilsHelper
    {
        public static bool MinimumAge(int age, DateTime niver)
        {
            DateTime minimumDate = DateTime.Now.AddYears(-age);
            return niver <= minimumDate;
        }

    }
}
