using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.Common.Models {
	public class UserRegisterModelDto {
		[Required]
		[Display(Name = "Login")]
		public string Login { get; set; }
		[Required]
		//[PasswordPropertyText]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Display(Name = "Email")]
		[Required]

		public Group userGroup { get; set; } = Group.User;
	}
}
