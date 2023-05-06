using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VKtest.Common.Interfaces;

namespace VKtest.Common {

	public class BasicAuthorizeAttribute : TypeFilterAttribute {
		
		public BasicAuthorizeAttribute() : base(typeof(BasicAuthorizeFilter)) {
		}

		private class BasicAuthorizeFilter : IAsyncAuthorizationFilter {
			private readonly IUserService _userService;

			public BasicAuthorizeFilter(IUserService userService) {
				_userService = userService;
			}


			public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
				if (!context.HttpContext.User.Identity.IsAuthenticated) {
					string authHeader = context.HttpContext.Request.Headers["Authorization"];

					if (authHeader != null && authHeader.StartsWith("Basic ")) {
						string encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
						byte[] decodedBytes = Convert.FromBase64String(encodedCredentials);
						string decodedCredentials = Encoding.UTF8.GetString(decodedBytes);

						string[] parts = decodedCredentials.Split(':');
						string username = parts[0];
						string password = parts[1];
						if (username == "freeuser1") {
							context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
							context.Result = new UnauthorizedResult();
							return;
						}
						if (await _userService.ValidateCredentials(username, password)) return;

					}

					context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
					context.Result = new UnauthorizedResult();
				}
			}
		}

	}
}
