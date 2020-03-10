using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EfCoreMultipleProvidersExample
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options): base(options) {}
        public DbSet<Datum> Data { get; set; }

    }

    public class ProjectDbContextSqlServer: ProjectDbContext
    {
        public ProjectDbContextSqlServer(DbContextOptions<ProjectDbContext> options): base(options){}
    }

    public class ProjectDbContextPostgreSQL: ProjectDbContext
    {
        public ProjectDbContextPostgreSQL(DbContextOptions<ProjectDbContext> options): base(options){}
    }

    public class Datum
    {
        public int Id { get; set; }
        public int Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string connstr = "Server=localhost;Database=test_db";

            // setup di
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ProjectDbContext, ProjectDbContextPostgreSQL>(
                builder => builder.UseNpgsql(connstr)
            // )
            // .AddScoped<DbContextOptions<ProjectDbContext>>(
            //     sp => new DbContextOptionsBuilder<ProjectDbContext>().UseNpgsql(connstr).Options
            );
            // ^^uncomment above for success

            //get service
            var sp = services.BuildServiceProvider();

            var db = sp.GetService<ProjectDbContext>();
            db.Database.EnsureCreated();
            db.Data.Add(new Datum { Value = 42 });
            db.SaveChanges();

            Console.WriteLine("Success.");
        }
    }
}
