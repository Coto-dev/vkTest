using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VKtest.DAL;

namespace VKtest.BL.Extensions {
	public static class AddDbContext {
			public static IServiceCollection AddDependencyDbContext(this IServiceCollection services, IConfiguration configuration) {
			services.AddDbContext<AppDbContext>(
				options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
				);
				return services;

			}
		

	}
}
