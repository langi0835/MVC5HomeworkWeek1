using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVC5HomeworkWeek1.DatatypeAttributes
{

	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class EmailAddressAttribute : DataTypeAttribute
	{
		private static Regex _regex = new Regex("PATTERN", RegexOptions.IgnoreCase);
		public EmailAddressAttribute() : base(DataType.EmailAddress)
		{
			ErrorMessage = "Email格式錯誤";
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