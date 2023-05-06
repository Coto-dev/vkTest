using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKtest.Common.Models {
	public class UserPagedListDto {
		public List<UserModelDto> Users { get; set; }
		public PageInfoDto PageInfo { get; set; }
	}
}
