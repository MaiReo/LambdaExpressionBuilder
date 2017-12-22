using System;
using Xunit;
using LambdaExpressionBuilder;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace LambdaExpressionBuilder.Tests
{
    public class LambdaExpressionBuilder_Tests
    {
        [Fact]
        public void LambdaExpressionBuilder_Test()
        {

            var builder = ExpressionBuilder.BuildFor<TestClass>();
            builder = builder.Where(x => x.Int == 1);

            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var src = source.AsQueryable().Where(builder).ToList();
            src.Count.ShouldBe(2);
        }

        [Fact]
        public void LambdaExpressionBuilder_Test2()
        {

            var builder = ExpressionBuilder.BuildFor<TestClass>();
            builder = builder.Where(x => x.Int == 1);
            builder = builder.Where(x => x.String == "S");

            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var src = source.AsQueryable().Where(builder).ToList();
            src.Count.ShouldBe(1);
        }

        [Fact]
        public void LambdaExpressionBuilder_Test3()
        {
            var builder = ExpressionBuilder.BuildFor<TestClass2>();
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass));

            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var source2 = new List<TestClass2>
            {
                new TestClass2(source[0]) { Int = 2 },
                new TestClass2(source[1]) { Int = 3, String = "C" },
                new TestClass2(source[2]) { Int = 3, String = "E" },
                new TestClass2(new TestClass(2, "N") { Int = 3, String = "F" }),
                new TestClass2(new TestClass(3, "O") { Int = 3, String = "G" }),
                new TestClass2(new TestClass(4, "P") { Int = 3, String = "I" })
            };

            var src = source2.AsQueryable().Where(builder, source.AsQueryable()).ToList();
            var srcDirect = source2.Where(x => source.Contains(x.TestClass)).ToList();
            src.Count.ShouldBe(srcDirect.Count);
        }

        [Fact]
        public void LambdaExpressionBuilder_Test4()
        {
            var builder = ExpressionBuilder.BuildFor<TestClass2>();
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass));
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => x.Int != 3);

            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var source2 = new List<TestClass2>
            {
                new TestClass2(source[0]) { Int = 2 },
                new TestClass2(source[1]) { Int = 3, String = "C" },
                new TestClass2(source[2]) { Int = 3, String = "E" },
                new TestClass2(new TestClass(2, "N") { Int = 3, String = "F" }),
                new TestClass2(new TestClass(3, "O") { Int = 3, String = "G" }),
                new TestClass2(new TestClass(4, "P") { Int = 3, String = "I" })
            };

            var src = source2.AsQueryable().Where(builder, source.AsQueryable()).ToList();
            var srcDirect = source2.Where(x => x.Int != 3 && source.Contains(x.TestClass)).ToList();
            src.Count.ShouldBe(srcDirect.Count);
        }

        [Fact]
        public void LambdaExpressionBuilder_Test5()
        {
            var builder = ExpressionBuilder.BuildFor<TestClass2>();
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass));
            builder = builder.Where(x => x.Int != 3);

            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var source2 = new List<TestClass2>
            {
                new TestClass2(source[0]) { Int = 2 },
                new TestClass2(source[1]) { Int = 3, String = "C" },
                new TestClass2(source[2]) { Int = 3, String = "E" },
                new TestClass2(new TestClass(2, "N") { Int = 3, String = "F" }),
                new TestClass2(new TestClass(3, "O") { Int = 3, String = "G" }),
                new TestClass2(new TestClass(4, "P") { Int = 3, String = "I" })
            };

            var src = source2.AsQueryable().Where(builder, source.AsQueryable()).ToList();
            var srcDirect = source2.Where(x => x.Int != 3 && source.Contains(x.TestClass)).ToList();
            src.Count.ShouldBe(srcDirect.Count);
        }
        [Fact]
        public void LambdaExpressionBuilder_Test6()
        {
            var builder = ExpressionBuilder.BuildFor<TestClass2>();
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass));
            builder = builder.Where(x => x.Int != 3);
            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var source2 = new List<TestClass2>
            {
                new TestClass2(source[0]) { Int = 2 },
                new TestClass2(source[1]) { Int = 3, String = "C" },
                new TestClass2(source[2]) { Int = 3, String = "E" },
                new TestClass2(new TestClass(2, "N") { Int = 3, String = "F" }),
                new TestClass2(new TestClass(3, "O") { Int = 3, String = "G" }),
                new TestClass2(new TestClass(4, "P") { Int = 3, String = "I" })
            };

            var src = source2.AsQueryable().Where(builder, source.AsQueryable()).ToList();
            var srcDirect = source2.Where(x => x.Int != 3 && source.Contains(x.TestClass)).ToList();
            src.Count.ShouldBe(srcDirect.Count);
        }
        [Fact]
        public void LambdaExpressionBuilder_Test7()
        {
            var builder = ExpressionBuilder.BuildFor<TestClass2>();
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => y.Contains(x.TestClass));
            builder = builder.Where(x => x.Int != 3);
            builder = builder.Where((TestClass2 x, IQueryable<TestClass> y) => true);
            var source = new List<TestClass>
            {
                new TestClass() { Int = 0 },
                new TestClass() { Int = 1, String = "S" },
                new TestClass() { Int = 1, String = "B" }
            };

            var source2 = new List<TestClass2>
            {
                new TestClass2(source[0]) { Int = 2 },
                new TestClass2(source[1]) { Int = 3, String = "C" },
                new TestClass2(source[2]) { Int = 3, String = "E" },
                new TestClass2(new TestClass(2, "N") { Int = 3, String = "F" }),
                new TestClass2(new TestClass(3, "O") { Int = 3, String = "G" }),
                new TestClass2(new TestClass(4, "P") { Int = 3, String = "I" })
            };

            var src = source2.AsQueryable().Where(builder, source.AsQueryable()).ToList();
            var srcDirect = source2.Where(x => x.Int != 3 && source.Contains(x.TestClass)).ToList();
            src.Count.ShouldBe(srcDirect.Count);
        }

    }
}
