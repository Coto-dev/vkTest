using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VKtest.DAL;
using VKtest.DAL.Entities;

namespace VKtest.BL.Extensions {
	public static class AddIdentity {
		public static IServiceCollection AddIdentityDependency(this IServiceCollection services) {

			services.AddIdentity<User, IdentityRole<Guid>>(options => { options.SignIn.RequireConfirmedEmail = false && options.User.RequireUniqueEmail; })
						.AddEntityFrameworkStores<AppDbContext>()
						.AddDefaultTokenProviders()
						.AddSignInManager<SignInManager<User>>()
						.AddUserManager<UserManager<User>>();
			return services;

		}

	}
}
