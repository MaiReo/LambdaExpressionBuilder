using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class TestClass2
    {
        public TestClass2()
        {
        }
        [Key]
        public int Id { get; set;}

        public string String { get; set; }
        
        public int Int { get; set; }

        public int? TestClassId {get;set;}
        
        [ForeignKey(nameof(TestClassId))]
        public TestClass TestClass { get; set; }
    }
}
