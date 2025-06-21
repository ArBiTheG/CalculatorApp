using CalculatorApp.Model;
using CalculatorApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApp.Service
{
    internal static class CalculatorService
    {
        public static decimal CalculateBaseOperation(CalcOperation operation, decimal firstNumber, decimal secondNumber)
        {
            decimal result;
            switch (operation)
            {
                case CalcOperation.Plus: result = firstNumber + secondNumber; break;
                case CalcOperation.Minus: result = firstNumber - secondNumber; break;
                case CalcOperation.Multiply: result = firstNumber * secondNumber; break;
                case CalcOperation.Divide: result = firstNumber / secondNumber; break;
                default: result = secondNumber; break;
            }
            return result;
        }

        public static decimal CalculatePercent(decimal firstNumber, decimal secondNumber)
        {
            return firstNumber* secondNumber / 100;
        }
        public static decimal CalculateSquareRoot(decimal number)
        {
            return (decimal)Math.Sqrt((double)number);
        }
        public static decimal CalculateSquare(decimal number)
        {
            return (decimal)Math.Pow((double)number, 2);
        }
        public static decimal CalculateReverse(decimal number)
        {
            return number * -1;
        }

        public static string GetFormulaStr(CalcOperation operation, decimal firstNumber, decimal secondNumber)
        {
            string temp;
            switch (operation)
            {
                case CalcOperation.Plus:
                    temp = firstNumber + " + " + secondNumber + " = "; break;
                case CalcOperation.Minus:
                    temp = firstNumber + " - " + secondNumber + " = "; break;
                case CalcOperation.Multiply:
                    temp = firstNumber + " * " + secondNumber + " = "; break;
                case CalcOperation.Divide:
                    temp = firstNumber + " / " + secondNumber + " = "; break;
                default:
                    temp = ""; break;
            }
            return temp;
        }
        public static string GetFormulaStr(CalcOperation operation, decimal firstNumber)
        {
            string temp;
            switch (operation)
            {
                case CalcOperation.Plus:
                    temp = firstNumber + " + "; break;
                case CalcOperation.Minus:
                    temp = firstNumber + " - "; break;
                case CalcOperation.Multiply:
                    temp = firstNumber + " * "; break;
                case CalcOperation.Divide:
                    temp = firstNumber + " / "; break;
                default:
                    temp = ""; break;
            }
            return temp;
        }
    }
}
