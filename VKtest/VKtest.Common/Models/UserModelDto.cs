using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.Common.Models {
	public class UserModelDto {
		public string Login { get; set; }
		public string Email { get; set; }
		public DateTime CreatedDate { get; set; }
		public UserGroupDto UserGroup { get; set; }
		public UserStateDto UserState { get; set; }

	}
}
