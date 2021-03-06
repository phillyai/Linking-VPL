﻿using System;

namespace Linking.Core.Var
{
    public class Variable : ICloneable
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Variable(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public object Clone()
        {
            return new Variable(Name, Value);
        }
    }
}
