using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKtest.Common.Models;

namespace VKtest.Common.Interfaces {
	public interface IUserService {
		public Task<Response> AddUser(UserRegisterModelDto model);
		public Task<Response> DeleteUser(Guid Id);
		public Task<UserModelDto> GetUserById(Guid Id);
		public Task<UserPagedListDto> GetUsers(int Page = 1);
		public Task<bool> ValidateCredentials(string username, string passwrod);


	}
}
