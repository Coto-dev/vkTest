using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Enums;

namespace VKtest.Common.Models {
	public class UserStateDto {
		public Guid Id { get; set; } 
		public State Code { get; set; }
		public string Description { get; set; }
	}
}
