using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5Course.Models.ValidateAttribute
{
    public class 商品名稱驗證Attribute : DataTypeAttribute
    {
        //private static Regex _regex = new Regex("[]", RegexOptions.IgnoreCase);

        public 商品名稱驗證Attribute() : base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string str = (string)value;
            //return valueAsString != null && _regex.Match(valueAsString).Length > 0;
            return str.Contains("[Product]") ? true : false;
        }

    }
}