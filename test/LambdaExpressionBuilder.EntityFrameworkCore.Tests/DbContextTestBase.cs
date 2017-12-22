using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class DbContextTestBase
    {
        internal protected static TestDbContext BuildDbContext()
        {
            var dbContext = TestDbContext.Instance;
            var tc1 = new TestClass() { Id = 1, Int = 0 };
            var tc2 = new TestClass() { Id = 2, Int = 1, String = "S" };
            var tc3 = new TestClass() { Id = 3, Int = 1, String = "B" };
            var tc4 = new TestClass( 2, "N" ) { Id = 4 };
            var tc5 = new TestClass( 3, "O" ) { Id = 5 };
            var tc6 = new TestClass( 4, "P" ) { Id = 6 };
            dbContext.TestClasses.Add( tc1 );
            dbContext.TestClasses.Add( tc2 );
            dbContext.TestClasses.Add( tc3 );
            dbContext.TestClasses.Add( tc4 );
            dbContext.TestClasses.Add( tc5 );
            dbContext.TestClasses.Add( tc6 );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 1, Int = 2, TestClassId = tc1.Id, TestClass = tc1 } );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 2, Int = 2, TestClassId = tc2.Id, TestClass = tc2 } );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 3, Int = 2, TestClassId = tc3.Id, TestClass = tc3 } );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 4, Int = 3, String = "F", TestClassId = tc4.Id, TestClass = tc4 } );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 5, Int = 3, String = "G", TestClassId = tc5.Id, TestClass = tc5 } );
            dbContext.TestClasses2.Add( new TestClass2 { Id = 6, Int = 3, String = "I", TestClassId = tc6.Id, TestClass = tc6 } );
            dbContext.TestClasses3.Add( new TestClass3 { Id = 1, Int = 10 } );
            dbContext.SaveChanges();
            return dbContext;
        }

        protected virtual List<T> UsingDbContext<T>( Expression<Func<TestDbContext, List<T>>> expression )
        {
            using (var context = BuildDbContext()) return expression?.Compile()?.Invoke( context );
        }

        protected virtual List<T> UsingDbContext<T>( IExpressionBuilder<T> builder ) where T : class
        {
            using (var context = BuildDbContext()) return context.Build( builder ).ToList();
        }

        protected virtual void ProceedDbContext( Expression<Action<TestDbContext>> expression )
        {
            using (var context = BuildDbContext())
            {
                expression?.Compile()?.Invoke( context );
                context.SaveChanges();
            }
        }
    }
}