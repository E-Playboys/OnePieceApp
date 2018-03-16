using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnePiece.Web.Utilities
{
    public static class Extensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum obj, object selectedValue)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.GetName(typeof(TEnum), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text", selectedValue);
        }
    }
}
