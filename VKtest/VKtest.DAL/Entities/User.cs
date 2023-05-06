using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VKtest.DAL.Entities {
	public class User : IdentityUser<Guid> {
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
		public UserGroup userGroup { get; set; }
		public UserState userState { get; set; }
	}
}
