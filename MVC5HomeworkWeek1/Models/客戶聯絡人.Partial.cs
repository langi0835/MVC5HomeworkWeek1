namespace MVC5HomeworkWeek1.Models
{
	using MVC5HomeworkWeek1.DatatypeAttributes;
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
	using System.Linq;

	[MetadataType(typeof(客戶聯絡人MetaData))]
	public partial class 客戶聯絡人 : IValidatableObject
	{
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var db = new 客戶資料Entities();

			if (this.Id == 0)
			{
				// Create
				if (db.客戶聯絡人.Where(p => p.客戶Id == this.客戶Id && p.Email == this.Email).Any())
					yield return new ValidationResult("Email已存在", new string[] { nameof(this.Email) });
			}
			else
			{
				// Edit
				if (db.客戶聯絡人.Where(p => p.客戶Id == this.客戶Id && p.Id != this.Id
					&& p.Email == this.Email).Any())
					yield return new ValidationResult("Email已存在", new string[] { nameof(this.Email) });
			}

			yield return ValidationResult.Success;
		}
	}

	public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
		[EmailAddress()]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
		[PhoneFormat()]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

		[Required]
		public bool 是否已刪除 { get; set; }

		public virtual 客戶資料 客戶資料 { get; set; }
    }
}
