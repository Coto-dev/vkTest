using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VKtest.BL.Services;
using VKtest.Common.Interfaces;
using VKtest.DAL;

namespace VKtest.BL.Extensions {
	public static class AddServices {
		public static IServiceCollection AddDependencyServices(this IServiceCollection services) {
			services.AddScoped<IUserService, UserService>();
			return services;
		}
	}
}
