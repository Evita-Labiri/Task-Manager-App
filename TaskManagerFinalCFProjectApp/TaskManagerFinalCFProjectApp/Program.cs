using TaskManagerFinalCFProjectApp.DAO.CategoryDAO;
using TaskManagerFinalCFProjectApp.DAO.TaskDAO;
using TaskManagerFinalCFProjectApp.Service.CategoryService;
using TaskManagerFinalCFProjectApp.Service.TaskService;

namespace TaskManagerFinalCFProjectApp
{
    public class Program
    {  
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<ITaskDAO, TaskDAOImpl>();
            builder.Services.AddScoped<ICategoryDAO, CategoryDAOImpl>();
            builder.Services.AddScoped<ITaskService, TaskServiceImpl>();
            builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();      


            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}