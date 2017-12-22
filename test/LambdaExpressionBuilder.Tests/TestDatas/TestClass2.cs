using System;
using System.Collections.Generic;

using System.Text;

namespace LambdaExpressionBuilder.Tests
{
    public class TestClass2
    {
        public TestClass2(TestClass testClass)
        {
            this.TestClass = testClass;
        }

       
        public int Int { get; set; }

        public string String { get; set; }
       
        public TestClass TestClass { get; set; }
    }
}
