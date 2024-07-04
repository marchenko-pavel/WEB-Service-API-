namespace Web;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Логируем
            CreateHostBuilder(args).Build().Run();
            // Логируем
        }
        catch (Exception ex)
        {
            // Логируем
        }
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseKestrel();
                webBuilder.UseUrls("http://localhost:8050");
            });
}
