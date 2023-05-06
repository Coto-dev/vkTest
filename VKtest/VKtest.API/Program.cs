using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using VKtest.BL.Extensions;
using VKtest.Common;
using VKtest.Common.Middlewares;
using VKtest.DAL;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(opts => {
	var enumConverter = new JsonStringEnumConverter();
	opts.JsonSerializerOptions.Converters.Add(enumConverter);
});
builder.Services.AddIdentityDependency();
builder.Services.AddDependencyServices();
builder.Services.AddDependencyDbContext(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManager", Version = "v1" });
	c.AddSecurityDefinition("basic", new OpenApiSecurityScheme {
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "basic",
		In = ParameterLocation.Header,
		Description = "Basic Authorization header using the Bearer scheme."
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
						{
						Type = ReferenceType.SecurityScheme,
						Id = "basic"
						}
					},
		new string[] {}
		}
	});
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<BasicAuthMiddleware>();

app.MapControllers();

app.Run();
