using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.Library.Core.Rules
{
    public static class OperatorDictionary
    {
        public static Dictionary<Type, List<Operator>> Operators { get; set; }

        static OperatorDictionary()
        {
            Operators = new Dictionary<Type, List<Operator>>();
            Operators.Add(typeof(decimal),
                new List<Operator>() 
                { 
                    new Operator() { Symbol = "=", Name = "Equals" }, 
                    new Operator() { Symbol = ">", Name = "GreaterThan" }, 
                    new Operator() { Symbol = ">=", Name = "GreaterThanEqualTo" }, 
                    new Operator() { Symbol = "<", Name = "LessThan" }, 
                    new Operator() { Symbol = "<=", Name = "LessThanEqualTo" }
                }
            );
            Operators.Add(typeof(int),
                new List<Operator>() 
                { 
                    new Operator() { Symbol = "=", Name = "Equals" }, 
                    new Operator() { Symbol = ">", Name = "GreaterThan" }, 
                    new Operator() { Symbol = ">=", Name = "GreaterThanEqualTo" }, 
                    new Operator() { Symbol = "<", Name = "LessThan" }, 
                    new Operator() { Symbol = "<=", Name = "LessThanEqualTo" }
                }
            );
            Operators.Add(typeof(string),
                new List<Operator>() 
                { 
                    new Operator() { Symbol = "=", Name = "Equals" }, 
                    new Operator() { Symbol = "Matches", Name = "Matches" }
                }
            );
            Operators.Add(typeof(Enum),
                new List<Operator>() 
                { 
                    new Operator() { Symbol = "=", Name = "Equals" }
                }
            );
            Operators.Add(typeof(bool),
                new List<Operator>() 
                { 
                    new Operator() { Symbol = "=", Name = "Equals" }
                }
);
        }
    }
}
