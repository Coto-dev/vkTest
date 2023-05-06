using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKtest.Common;
using VKtest.Common.Interfaces;
using VKtest.Common.Models;

namespace VKtest.API.Controllers {
	[ApiController]
	[Route("api/user")]
	public class UserController : ControllerBase {
		private readonly IUserService _userService;
		public UserController(IUserService userService) {
			_userService = userService;
		}
		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<Response>> AddUser(UserRegisterModelDto model) {
			return Ok(await _userService.AddUser(model));
		}
		[HttpDelete]
		[BasicAuthorize]
		[Route("delete")]
		public async Task<ActionResult<Response>> DeleteUser(Guid Id) {
			return Ok(await _userService.DeleteUser(Id));

		}
		
		[HttpGet]
		[BasicAuthorize]
		[Route("{UserId}")]
		public async Task<ActionResult<UserModelDto>> GetUserById(Guid UserId) {
			return Ok(await _userService.GetUserById(UserId));

		}
		[HttpGet]
		[BasicAuthorize]
		[Route("all")]
		public async Task<ActionResult<UserPagedListDto>> GetUsers(int Page = 1) {
			return Ok(await _userService.GetUsers(Page));
		}
	}
}