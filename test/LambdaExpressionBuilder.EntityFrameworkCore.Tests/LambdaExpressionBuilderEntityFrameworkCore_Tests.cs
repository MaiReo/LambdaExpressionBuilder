using System;
using Xunit;
using LambdaExpressionBuilder;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using System.Linq.Expressions;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class LambdaExpressionBuilderEntityFrameworkCore_Tests : DbContextTestBase
    {
        [Fact]
        public void DbContext_Builder_Test()
        {
            Expression<Func<TestClass, bool>> expr = x => x.Int == 1;

            var builder = ExpressionBuilder
                          .BuildFor<TestClass>()
                          .Where( expr );

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses.Where( expr ).ToList() );

            src.Count.ShouldBe( src_direct.Count );
        }

        [Fact]
        public void DbContext_Builder_Test2()
        {
            Expression<Func<TestClass2, bool>> expr = x => x.Int == 1;

            var builder = ExpressionBuilder
                          .BuildFor<TestClass2>()
                          .Where( expr )
                          /*
                           * If use y.Contains method (use a referenced type like navigation property in lambda expression) will cause
                           * System.ArgumentException : Expression of type '{Type name, here is x.TestClass}' 
                           * cannot be used for parameter of type 'Microsoft.EntityFrameworkCore.Storage.ValueBuffer'
                           * of method 'Boolean Contains[ValueBuffer](System.Collections.Generic.IEnumerable`1[Microsoft.EntityFrameworkCore.Storage.ValueBuffer],
                           * Microsoft.EntityFrameworkCore.Storage.ValueBuffer)'
                           */
                          //.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass))
                          .Where( ( TestClass2 x, IQueryable<TestClass> y ) => y.Any( z => z.Id == x.TestClassId ) )
                          ;

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses2
            .Where( expr )
            //.Where(x=>dbContext.TestClasses.Contains(x.TestClass))
            .Where( x => dbContext.TestClasses.Any( z => z.Id == x.TestClassId ) )
            .ToList() );
            src.Count.ShouldBe( src_direct.Count );
        }

        [Fact]
        public void DbContext_Builder_Test3()
        {
            Expression<Func<TestClass2, bool>> expr = x => x.Int == 1;

            var builder = ExpressionBuilder
                          .BuildFor<TestClass2>()
                          .Where( expr )
                          .Where( ( TestClass2 x, IQueryable<TestClass> y ) => y.Any( z => z.Id == x.TestClassId ) )
                          .Where( ( TestClass2 x, IQueryable<TestClass3> y ) => y.Max( z => z.Int ) <= x.Int )
                          ;

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses2
            .Where( expr )
            .Where( x => dbContext.TestClasses.Any( z => z.Id == x.TestClassId ) && dbContext.TestClasses3.Max( z => z.Int ) <= x.Int )
            .ToList() );
            src.Count.ShouldBe( src_direct.Count );
        }

        [Fact]
        public void DbContext_Builder_Test4()
        {
            var builder = ExpressionBuilder
                          .BuildFor<TestClass2>()
                          .Where( ( TestClass2 x, IQueryable<TestClass> y, IQueryable<TestClass3> z ) => y.Any( _ => _.Id == x.TestClassId ) && z.Max( __ => __.Int ) <= x.Int )
                          ;

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses2
            .Where( x => dbContext.TestClasses.Any( z => z.Id == x.TestClassId ) && dbContext.TestClasses3.Max( __ => __.Int ) <= x.Int )
            .ToList() );
            src.Count.ShouldBe( src_direct.Count );
        }

        [Fact]
        public void DbContext_Builder_Test5()
        {
            var builder = ExpressionBuilder
                          .BuildFor<TestClass2>()
                          .Where( ( TestClass2 tc2,
                                    IQueryable<TestClass2> tc2_src, 
                                    IQueryable<TestClass2> tc2_src2 ) => !tc2_src.Where( x => x.Int < 1 ).Any( x => x.Id == tc2.Id ) )
                          ;

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses2
            .Where( tc2 => !dbContext.TestClasses2.Where( x => x.Int < 1 ).Any( x => x.Id == tc2.Id ) )
            .ToList() );
            src.Count.ShouldBe( src_direct.Count );
        }
        [Fact]
        public void DbContext_Builder_Test6()
        {
            var builder = ExpressionBuilder
                          .BuildFor<TestClass2>()
                          .Where( ( TestClass2 tc2, IQueryable<TestClass2> tc2_src, IQueryable<TestClass2> tc2_src2 ) => !tc2_src.Where( x => x.Int < 1 ).Any( x => x.Id == tc2.Id ) && tc2_src2.Where( x => x.Int >= 0 ).Any( x => x.Id == tc2.Id ) )
                          ;

            var src = UsingDbContext( builder );
            var src_direct = UsingDbContext( dbContext => dbContext.TestClasses2
            .Where( tc2 => !dbContext.TestClasses2.Where( x => x.Int < 1 ).Any( x => x.Id == tc2.Id ) && dbContext.TestClasses2.Where( x => x.Int >= 0 ).Any( x => x.Id == tc2.Id ) )
            .ToList() );
            src.Count.ShouldBe( src_direct.Count );
        }
    }
}
