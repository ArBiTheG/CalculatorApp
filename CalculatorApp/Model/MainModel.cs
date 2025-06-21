using CalculatorApp.Service;
using CalculatorApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Model
{
    internal class MainModel
    {
        private decimal _previousNumber;
        private CalcOperation _previousOperation;

        public MainModel()
        {
            _previousNumber = 0;
            _previousOperation = CalcOperation.None;
        }

        public decimal PreviousNumber => _previousNumber;
        public CalcOperation PreviousOperation => _previousOperation;

        public decimal ExecutePercent(decimal value)
        {
            return CalculatorService.CalculatePercent(_previousNumber, value);
        }
        public decimal ExecuteSquareRoot(decimal value)
        {
            return CalculatorService.CalculateSquareRoot(value);
        }
        public decimal ExecuteSquare(decimal value)
        {
            return CalculatorService.CalculateSquare(value);
        }
        public decimal ExecuteReverse(decimal value)
        {
            return CalculatorService.CalculateReverse(value);
        }
        public void ExecuteClear()
        {
            _previousNumber = 0;
            _previousOperation = CalcOperation.None;
        }
        public decimal ExecuteBaseOperation(CalcOperation operation, decimal value)
        {
            decimal result = CalculatorService.CalculateBaseOperation(_previousOperation, _previousNumber, value);

            string formulaStr = CalculatorService.GetFormulaStr(_previousOperation, _previousNumber, value);

            _previousNumber = result;
            _previousOperation = operation;

            return result;
        }

        public decimal ExecuteEqually(decimal value)
        {
            decimal result = CalculatorService.CalculateBaseOperation(_previousOperation, _previousNumber, value);
            CalcOperation operation = CalcOperation.Equally;

            string formulaStr = CalculatorService.GetFormulaStr(_previousOperation, _previousNumber, value);

            _previousNumber = result;
            _previousOperation = operation;

            return result;
        }
    }
}
