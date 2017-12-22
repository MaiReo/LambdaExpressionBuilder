using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LambdaExpressionBuilder.EntityFrameworkCore.Tests
{
    public class TestClass3
    {
        [Key]
        public int Id { get; set; }

        public int Int { get; set; }
    }
}
