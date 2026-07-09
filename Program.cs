using CrackJobs.Application.Interfaces;
using CrackJobs.Application.Services;
using CrackJobs.Application.Mappings;
using CrackJobs.Infrastructure.Repositories;
using CrackJobs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add application services
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();

// Add Answer services
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();

// Add Comment services
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();

// Add Company services
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

// Add Technology services
builder.Services.AddScoped<ITechnologyRepository, TechnologyRepository>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();

// Add Rating services
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();

// Add QuestionAnswerMap services
builder.Services.AddScoped<IQuestionAnswerMapRepository, QuestionAnswerMapRepository>();
builder.Services.AddScoped<IQuestionAnswerMapService, QuestionAnswerMapService>();

// Add QuestionCompanyMap services
builder.Services.AddScoped<IQuestionCompanyMapRepository, QuestionCompanyMapRepository>();
builder.Services.AddScoped<IQuestionCompanyMapService, QuestionCompanyMapService>();

// Add QuestionTechnologyMap services
builder.Services.AddScoped<IQuestionTechnologyMapRepository, QuestionTechnologyMapRepository>();
builder.Services.AddScoped<IQuestionTechnologyMapService, QuestionTechnologyMapService>();

// Add JobRole services
builder.Services.AddScoped<IJobRoleRepository, JobRoleRepository>();
builder.Services.AddScoped<IJobRoleService, JobRoleService>();

// Add QuestionJobRoleMap services
builder.Services.AddScoped<IQuestionJobRoleMapRepository, QuestionJobRoleMapRepository>();
builder.Services.AddScoped<IQuestionJobRoleMapService, QuestionJobRoleMapService>();

// Add Like services
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<ILikeService, LikeService>();

// Allow the React dev server (and any other origin in dev) to call the API directly.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//Add Helath Checks
builder.Services.AddHealthChecks();

// Add DbContext
builder.Services.AddDbContext<CrackJobContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech Jobs API V1");
        options.RoutePrefix = string.Empty; // Swagger UI at root
    });
// }

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
