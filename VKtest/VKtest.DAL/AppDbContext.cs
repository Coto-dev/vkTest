using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VKtest.Common.Enums;
using VKtest.DAL.Entities;

namespace VKtest.DAL {
	public class AppDbContext : 
		IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>> {
		public override DbSet<User> Users { get; set; }
		public  DbSet<UserGroup> UserGroups { get; set; }
		public  DbSet<UserState> UserStates { get; set; }
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			Initialize();
		}

		private void Initialize() {
			if (!UserGroups.Any()) {
				var entity = new List<UserGroup> {
				new UserGroup
					{ Code = Group.User,
					Description= "It is User",
					Id= Guid.NewGuid(),
					},
				new UserGroup
					{ Code = Group.Admin,
					Description= "It is Administator",
					Id= Guid.NewGuid(),
					},

				};
				UserGroups.AddRange(entity);
			}
			if (!UserStates.Any()) {
				var entity = new List<UserState> {
				new UserState
					{ Code = State.Blocked,
					Description= "You're blocked",
					Id= Guid.NewGuid(),
					},
				new UserState
					{ Code = State.Active,
					Description= "You're active",
					Id= Guid.NewGuid(),
					},

				};
				UserStates.AddRange(entity);
			}

			SaveChanges();
		}

	}
}
