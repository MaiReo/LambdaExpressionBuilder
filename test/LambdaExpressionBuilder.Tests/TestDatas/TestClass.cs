﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LambdaExpressionBuilder.Tests
{
    public class TestClass
    {
        public TestClass() { }

       

        public TestClass(int i, string s)
        {
            Int = i;
            String = s;
        }

        public int Int { get; set; }

        public string String { get; set; }
    }
}
