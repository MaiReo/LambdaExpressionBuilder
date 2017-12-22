using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class TestDbContext : DbContext
    {
        private static readonly ServiceProvider _serviceProvider;
        static TestDbContext()
        {
            _serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .AddDbContext<TestDbContext>(
                ( serviceProvider, options ) =>
                options.UseInMemoryDatabase( Guid.NewGuid().ToString() )
                       .UseInternalServiceProvider( serviceProvider ),
                       ServiceLifetime.Transient,
                       ServiceLifetime.Transient )
            .BuildServiceProvider();
        }
        private static TestDbContext New() => _serviceProvider.GetRequiredService<TestDbContext>();
        public static TestDbContext Instance => New();
        public TestDbContext( DbContextOptions options ) : base( options ) { }

        public DbSet<TestClass> TestClasses { get; set; }
        public DbSet<TestClass2> TestClasses2 { get; set; }
        public DbSet<TestClass3> TestClasses3 { get; set; }
    }
}


