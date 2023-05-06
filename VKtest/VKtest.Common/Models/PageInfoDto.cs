using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKtest.Common.Models {
		/// <summary>
		/// information about page
		/// </summary>
		public class PageInfoDto {
			/// <summary>
			/// current number of page
			/// </summary>
			public int Current { get; set; }
			/// <summary>
			/// number of max size elements on page
			/// </summary>
			public int Size { get; set; } = 6;
			/// <summary>
			/// total number of pages
			/// </summary>
			public int Count { get; set; }
		}

	
}
