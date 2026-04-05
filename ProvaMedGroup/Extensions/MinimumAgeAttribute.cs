using ProvaMedGroup.DomainService.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProvaMed.Api.Extensions
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        public override bool IsValid(object value)
        {

            DateTime date;

            if (DateTime.TryParse(value.ToString(), out date))
            {
                return UtilsHelper.MinimumAge(_minimumAge, date);
            }

            return false;


        }
    }
}
