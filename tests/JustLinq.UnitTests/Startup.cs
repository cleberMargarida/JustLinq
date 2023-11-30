using Microsoft.Extensions.DependencyInjection;
using JustLinq.SqlServer;

namespace JustLinq.UnitTests
{
    public class Startup
    {
        private const string ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDatabase>(x => new Database(opt => opt.UseSqlServer(ConnectionString)));
        }
    }
}
