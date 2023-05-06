using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.DAL.Entities {
	public class UserState {
		public Guid Id { get; set; } = Guid.NewGuid();
		public State Code { get; set; }
		public string Description { get; set; }

	}
}
