using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class TestClass
    {
        public TestClass() { }

        public TestClass(int i, string s)
        {
            Int = i;
            String = s;
        }
        [Key]
         public int Id { get; set;}
        public int Int { get; set; }

        public string String { get; set; }
    }
}
