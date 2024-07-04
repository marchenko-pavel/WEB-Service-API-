using Infrastructure.DAL;
using Infrastructure.Data;
using Infrastructure.Data.Sets;
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
            services.AddTransient<IRepository, Repository>();
            services.AddSingleton<BdAutoFiller>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContextFactory<AppDbContext>(options => options.UseNpgsql(connectionString));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            DbContextOptions<AppDbContext> dbContext, BdAutoFiller autoFiller)
        {
            EnsureDatabase(dbContext, autoFiller);

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
        private async Task EnsureDatabase(DbContextOptions<AppDbContext> dbContext, BdAutoFiller autoFiller)
        {
            bool isCreatedBd;
            using (var db = new AppDbContext(dbContext)) { isCreatedBd = db.Database.EnsureCreated(); }

            if (isCreatedBd && !autoFiller.IsFilledBd) { await autoFiller.FillAsync(); }
        }
    }
}
