using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Model
{
    internal class CalculatedOperation
    {
        public CalculatedOperation(string formula, decimal value)
        {
            Formula = formula;
            Value = value;
        }
        public int Id { get; set; }
        public string Formula { get; set; }
        public decimal Value { get; set; }
    }
}
