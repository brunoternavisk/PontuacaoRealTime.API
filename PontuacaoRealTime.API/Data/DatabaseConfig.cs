using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PontuacaoRealTime.API.Data;

namespace PontuacaoRealTime.API.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=pontuacao.db"));
        }
    }
}