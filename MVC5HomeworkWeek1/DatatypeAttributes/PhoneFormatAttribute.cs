using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5HomeworkWeek1.DatatypeAttributes
{
	public class PhoneFormatAttribute : DataTypeAttribute
	{
		private static Regex _regex = new Regex(@"\d{4}-\d{6}", RegexOptions.IgnoreCase);
		public PhoneFormatAttribute() : base(DataType.Text)
		{
			ErrorMessage = "電話號碼格式錯誤";
		}

		public override bool IsValid(object value)
		{
			if (value == null)
				return true;

			string valueAsString = value as string;
			return valueAsString != null && _regex.Match(valueAsString).Length > 0;
		}
	}
}