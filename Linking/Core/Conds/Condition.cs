﻿using System;
using System.Windows.Forms;
using Linking.Core.Var;

namespace Linking.Core.Conds
{
    public class Condition
    {
        public Condition()
        {

        }

        public bool Check(VariableTable table)
        {
            if (LType == ValueType.Cond)
            {
                if (RType == ValueType.Cond)
                {
                    switch(Compare)
                    {
                        case CompareType.And:
                            return ((Condition)L).Check(table) && ((Condition)R).Check(table);
                        case CompareType.Or:
                            return ((Condition)L).Check(table) || ((Condition)R).Check(table);
                        case CompareType.Not:
                            return !((Condition)L).Check(table);
                        default:
                            throw new ArgumentException("Conditions cannot be compared");
                    }
                }
                else
                    throw new ArgumentException("Both are not Cond");
            }
            else
            {
                dynamic lres, rres;
                lres = LType == ValueType.Val ? L : table[L.ToString()].Value;
                rres = RType == ValueType.Val ? R : table[R.ToString()].Value;
                switch(Compare)
                {
                    case CompareType.Equal:
                        return lres.ToString() == rres.ToString();
                    case CompareType.NotEqual:
                        return lres.ToString() != rres.ToString();
                    case CompareType.Smaller:
                        return lres < rres;
                    case CompareType.Bigger:
                        return lres > rres;
                    case CompareType.SmallerEqual:
                        return lres <= rres;
                    case CompareType.BiggerEqual:
                        return lres >= rres;
                    default:
                        throw new ArgumentException("Vars or Vals cannot be compared logical");
                }
            }
        }

        [Flags]
        public enum ValueType { Var = 1, Str = 2, Num = 4, Bool = 8, Val = Str | Num | Bool, Cond = 16 }
        public enum CompareType { Equal, NotEqual, Smaller, Bigger, SmallerEqual, BiggerEqual, And, Or, Not }

        public CompareType Compare { get; set; }
        public ValueType LType { get; set; }
        public object L { get; set; }
        public ValueType RType { get; set; }
        public object R { get; set; }
        
    }
}
