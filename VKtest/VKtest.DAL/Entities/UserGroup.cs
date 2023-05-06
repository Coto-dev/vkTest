using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.DAL.Entities {
	public class UserGroup {
		public Guid Id { get; set; } = Guid.NewGuid();
		public Group Code { get; set; }
		public string Description { get; set; }
	}
}
