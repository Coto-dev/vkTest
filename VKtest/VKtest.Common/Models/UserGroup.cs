using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.Common.Models {
	public class UserGroupDto {
		public Guid Id { get; set; } 
		public Group Code { get; set; }
		public string Description { get; set; }
	}
}
