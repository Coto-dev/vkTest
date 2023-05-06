using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VKtest.Common.Exceptions;
using VKtest.Common.Interfaces;

namespace VKtest.Common.Middlewares {
	public class BasicAuthMiddleware {
		private readonly RequestDelegate _next;

		public BasicAuthMiddleware(RequestDelegate next) {
			_next = next;
		}

		public async Task Invoke(HttpContext context, IUserService userService) {
			try {
				var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
				var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
				var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
				var username = credentials[0];
				var password = credentials[1];
				if (!await userService.ValidateCredentials(username, password)) throw new UnauthorizedAccessException();
				
			}
			catch {
				throw new NotFoundException("dwa");
			}

			await _next(context);
		}
	}
}
