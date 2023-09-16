using Azure;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;
using NTSY.WebBlog; 
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().
    WithExposedHeaders("Content-Disposition"); ;
}));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// lấy connectionString
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddScoped<IUnitOfWork>(sp =>
    new UnitOfWork(connectionString));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IFollowsRepository, FollowsRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommentService, CommentsService>();
builder.Services.AddScoped<IFollowsService, FollowsService>(); 
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPostService,PostService>();
builder.Services.AddScoped<IUserService,UserService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}" 
    );
});
app.MapControllers();

app.Run();
