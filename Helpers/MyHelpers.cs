using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public static class MyHelpers
    {        
        //Tested in Views/Disciplines/Index
        public static HtmlString Select<T, TVal>(this IHtmlHelper html, IEnumerable<T> xs, string valName, string txtName, TVal selected)
        {
            var propTxt = typeof(T).GetProperty(txtName);
            var propVal = typeof(T).GetProperty(valName);
            IEnumerable<string> txts = xs.Select(x => (string)propTxt.GetValue(x));
            IEnumerable<TVal> values = xs.Select(x => (TVal)propVal.GetValue(x));            
            var builder = new StringBuilder();
            builder.Append($"<select>");            
            for (int i=0; i<xs.Count(); i++)
            {
                var val = values.ElementAt(i);
                string txt = txts.ElementAt(i);
                builder.Append($"<option value=\"{val}\"");
                if (val.Equals(selected))
                    builder.Append("selected");
                builder.Append($">{txt}</option>");
            }
            builder.Append("</select>");
            return new HtmlString(builder.ToString());
        }
    }
}
