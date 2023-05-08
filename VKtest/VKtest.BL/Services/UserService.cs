using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VKtest.Common;
using VKtest.Common.Enums;
using VKtest.Common.Exceptions;
using VKtest.Common.Interfaces;
using VKtest.Common.Models;
using VKtest.DAL;
using VKtest.DAL.Entities;

namespace VKtest.BL.Services {
	public class UserService : IUserService {
		private readonly UserManager<User> _userManager;
		private readonly AppDbContext _context;

		public UserService(UserManager<User> userManager, AppDbContext context) {
			_userManager = userManager;
			_context = context;
		}

		public async Task<Response> AddUser(UserRegisterModelDto model) {
			var state = await _context.UserStates.FirstOrDefaultAsync(s => s.Code == State.Active);
			if (state == null) throw new NotFoundException("status does not found");
			var group = await _context.UserGroups.FirstOrDefaultAsync(s => s.Code == model.userGroup);
			if (group == null) throw new NotFoundException("group does not found");
			var admin = await _userManager.Users.FirstOrDefaultAsync(u => u.userGroup.Code == Group.Admin);
			if (admin != null & group.Code == Group.Admin) throw new ConflictException("user with group Admin already exists");
			var user = new User { 
				UserName = model.Login,
				Email = model.Email,
				userState = state,
				userGroup = group
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (result.Succeeded) {
				return new Response {
					Message = $"User {model.Login} successfully created",
					Statuts = "200"
				};
			}
			else {
				var errors = string.Join(", ", result.Errors.Select(x => x.Description));
				throw new BadRequestException(errors);

			}
			
			// Thread.Sleep(5000);
		}

		public async Task<Response> DeleteUser(Guid Id) {
			var user = await _userManager.Users
				.Include(u => u.userState)
				.FirstOrDefaultAsync(x => x.Id == Id);
			if (user == null) throw new KeyNotFoundException("user with this Id does not found");
			var state = await _context.UserStates.FirstOrDefaultAsync(s => s.Code == State.Blocked);
			if (state == null) throw new NotFoundException("status does not found");
			if (user.userState.Code == State.Blocked) throw new ConflictException("user already blocked");
			user.userState = state;
			await _userManager.UpdateAsync(user);
			return new Response {
				Message = $"User {user.UserName} successfully blocked",
				Statuts = "200"
			};
		}

		public async Task<UserModelDto> GetUserById(Guid Id) {
			var user = await _userManager.Users
				.Include(u=>u.userGroup)
				.Include(u=>u.userState)
				.FirstOrDefaultAsync(x => x.Id == Id);
			if (user == null) throw new KeyNotFoundException("user with this Id does not found");
			return new UserModelDto {
				Login = user.UserName,
				Email = user.Email,
				UserGroup = new UserGroupDto {
					Id = user.userGroup.Id,
					Code = user.userGroup.Code,
					Description = user.userGroup.Description,
				},
				UserState = new UserStateDto {
					Id = user.userState.Id,
					Code = user.userState.Code,
					Description = user.userState.Description,
				}

			};

		}

		public async Task<UserPagedListDto> GetUsers(int Page = 1) {
			if (Page <= 0) throw new BadRequestException("wrong page");

			var totalItems =  await _userManager.Users.CountAsync();
			var totalPages = (int)Math.Ceiling((double)totalItems / AppConstants.PageSize);
			if (Page > totalPages) throw new BadRequestException("this page does not exist");
			var users = await _userManager.Users
				   .Include(u => u.userGroup)
				   .Include(u => u.userState)
				   .Skip((Page - 1) * AppConstants.PageSize)
				   .Take(AppConstants.PageSize)
				   .ToListAsync();
			var usersDto = users.Select(x => new UserModelDto {
				Login = x.UserName,
				Email = x.Email,
				CreatedDate= x.CreatedDate.Date,
				UserGroup = new UserGroupDto {
					Id = x.userGroup.Id,
					Code = x.userGroup.Code,
					Description = x.userGroup.Description,
				},
				UserState = new UserStateDto {
					Id = x.userState.Id,
					Code = x.userState.Code,
					Description = x.userState.Description,
				}
			}
			).ToList();
			return new UserPagedListDto {
				Users = usersDto,
				PageInfo = new PageInfoDto {
					Count = totalPages,
					Current = Page,
					Size = AppConstants.PageSize
				}

			};
		}

		public async Task<bool> ValidateCredentials(string username, string passwrod) {
			var user = await _userManager.Users
				.Include(u=>u.userState)
				.FirstOrDefaultAsync(u=>u.UserName == username);
			if (user == null) throw new BadRequestException("wrong username or password");
			if (user.userState.Code == State.Blocked) throw new BadRequestException("you're blocked");
			var check = await _userManager.CheckPasswordAsync(user, passwrod);
			return check;
		}
	}
}
