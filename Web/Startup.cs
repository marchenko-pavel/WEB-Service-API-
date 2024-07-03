using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql(connectionString));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextOptions<AppDbContext> dbContext)
        {
            EnsureDatabase(dbContext);

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private async Task EnsureDatabase(DbContextOptions<AppDbContext> dbContext)
        {
            using (var db = new AppDbContext(dbContext)) { db.Database.EnsureCreated(); }
        }
    }
}
