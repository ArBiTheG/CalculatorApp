using CalculatorApp.Model;
using CalculatorApp.Service;
using CalculatorApp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CalculatorApp.ViewModel
{
    internal class MainViewModel: INotifyPropertyChanged
    {
        MainModel _model = new MainModel();

        private string _formulaStr = "";
        private string _numberStr = "0";
        private ObservableCollection<CalculatedOperation> _operationHistory ;
        
        private RelayCommand? inputCommand;

        public MainViewModel()
        {
            _model = new MainModel();
            _operationHistory = new ObservableCollection<CalculatedOperation>();
        }
        public string NumberStr
        {
            get
            {
                return _numberStr;
            }
            set
            {
                _numberStr = value;
                OnPropertyChanged(nameof(NumberStr));
            }
        }
        public string FormulaStr
        {
            get
            {
                return _formulaStr;
            }
            set
            {
                _formulaStr = value;
                OnPropertyChanged(nameof(FormulaStr));
            }
        }

        public ObservableCollection<CalculatedOperation> OperationHistory
        {
            get
            {
                return _operationHistory;
            }
            set
            {
                _operationHistory = value;
                OnPropertyChanged(nameof(OperationHistory));
            }
        }

        public RelayCommand InputCommand
        {
            get
            {
                return inputCommand ?? (inputCommand = new RelayCommand(obj =>
                {
                    CalcKey inputKey = CalcKey.None;
                    if (obj != null)
                        if (obj is CalcKey)
                        inputKey = (CalcKey)obj;
                    InputGeneralKey(inputKey);
                }));
            }
        }
        public void InputGeneralKey(CalcKey key)
        {
            Trace.WriteLine($"Определена кнопка калькулятора: {key}");
            switch (key)
            {
                case CalcKey.KeyClear:
                    ClearNumberStr();
                    break;
                case CalcKey.KeyAllClear:
                    ClearAll();
                    break;
                case CalcKey.KeyDelete:
                    RemoveSymbolFormNumberStr();
                    break;
                case CalcKey.KeyPlus:
                    InputPlus();
                    break;
                case CalcKey.KeyMinus:
                    InputMinus();
                    break;
                case CalcKey.KeyMultiply:
                    InputMultiply();
                    break;
                case CalcKey.KeyDivide:
                    InputDivide();
                    break;
                case CalcKey.KeyEqually:
                    InputEqually();
                    break;
                case CalcKey.KeyPercent:
                    InputPercent();
                    break;
                case CalcKey.KeySquareRoot:
                    InputSquareRoot();
                    break;
                case CalcKey.KeySquare:
                    InputSquare();
                    break;
                case CalcKey.KeyReverse:
                    InputReverse();
                    break;
                case CalcKey.Key0:
                case CalcKey.Key1:
                case CalcKey.Key2:
                case CalcKey.Key3:
                case CalcKey.Key4:
                case CalcKey.Key5:
                case CalcKey.Key6:
                case CalcKey.Key7:
                case CalcKey.Key8:
                case CalcKey.Key9:
                case CalcKey.KeyDot:
                    InputNumberKey(key);
                    break;
            }
        }

        public void InputNumberKey(CalcKey inputKey)
        {
            char inputSymbol = GetSymbolByKey(inputKey);
            bool hasDot = NumberStr.Contains(',');

            if (inputSymbol == ',' && hasDot)
                return;

            if (NumberStr == "" || NumberStr == "0")
                if (inputSymbol == ',')
                    NumberStr = "0,";
                else
                    NumberStr = inputSymbol.ToString();
            else
                NumberStr += inputSymbol;
        }

        public void InputPlus()
        {
            decimal number = GetDecimalNumberStr();
            decimal previousNumber = _model.PreviousNumber;

            decimal result = _model.ExecuteBaseOperation(CalcOperation.Plus,number);

            OperationHistory.Add(new CalculatedOperation(CalculatorService.GetFormulaStr(CalcOperation.Plus, previousNumber, number), result));

            FormulaStr = CalculatorService.GetFormulaStr(CalcOperation.Plus, result);
            
            ClearNumberStr();
        }
        public void InputMinus()
        {
            decimal number = GetDecimalNumberStr();
            decimal previousNumber = _model.PreviousNumber;
            decimal result = _model.ExecuteBaseOperation(CalcOperation.Minus, number);

            OperationHistory.Add(new CalculatedOperation(CalculatorService.GetFormulaStr(CalcOperation.Plus, previousNumber, number), result));

            FormulaStr = CalculatorService.GetFormulaStr(CalcOperation.Minus, result);

            ClearNumberStr();
        }
        public void InputMultiply ()
        {
            decimal number = GetDecimalNumberStr();
            decimal previousNumber = _model.PreviousNumber;
            decimal result = _model.ExecuteBaseOperation(CalcOperation.Multiply, number);

            OperationHistory.Add(new CalculatedOperation(CalculatorService.GetFormulaStr(CalcOperation.Plus, previousNumber, number), result));

            FormulaStr = CalculatorService.GetFormulaStr(CalcOperation.Multiply, result);

            ClearNumberStr();
        }
        public void InputDivide()
        {
            decimal number = GetDecimalNumberStr();
            decimal previousNumber = _model.PreviousNumber;
            decimal result = _model.ExecuteBaseOperation(CalcOperation.Divide, number);

            OperationHistory.Add(new CalculatedOperation(CalculatorService.GetFormulaStr(CalcOperation.Plus, previousNumber, number), result));

            FormulaStr = CalculatorService.GetFormulaStr(CalcOperation.Divide, result);

            ClearNumberStr();
        }
        public void InputPercent()
        {
            decimal number = GetDecimalNumberStr();
            decimal result = _model.ExecutePercent(number);

            NumberStr = result.ToString();
        }
        public void InputSquareRoot()
        {
            decimal number = GetDecimalNumberStr();
            decimal result = _model.ExecuteSquareRoot(number);

            NumberStr = result.ToString();
        }
        public void InputSquare()
        {
            decimal number = GetDecimalNumberStr();
            decimal result = _model.ExecuteSquare(number);

            NumberStr = result.ToString();
        }
        public void InputReverse()
        {
            decimal number = GetDecimalNumberStr();
            decimal result = _model.ExecuteReverse(number);

            NumberStr = result.ToString();
        }

        public void InputEqually()
        {
            decimal numberFirst = _model.PreviousNumber;
            CalcOperation operation = _model.PreviousOperation;
            decimal numberSecond = GetDecimalNumberStr();
            decimal result = _model.ExecuteEqually(numberSecond);

            string formulaStr = CalculatorService.GetFormulaStr(operation, numberFirst, numberSecond);

            OperationHistory.Add(new CalculatedOperation(formulaStr, result));

            FormulaStr = formulaStr;
            NumberStr = result.ToString();
        }

        public void ClearAll()
        {
            ClearNumberStr();
            FormulaStr = "";
            _model.ExecuteClear();
        }

        public void ClearNumberStr()
        {
            NumberStr = "0";
        }

        public void RemoveSymbolFormNumberStr()
        {
            if (NumberStr.Length > 1)
                NumberStr = NumberStr.Remove(NumberStr.Length - 1);
            else
                NumberStr = "0";
        }

        private decimal GetDecimalNumberStr()
        {
            try
            {
                decimal result = Convert.ToDecimal(NumberStr);
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private char GetSymbolByKey(CalcKey key)
        {
            switch (key)
            {
                case CalcKey.Key0: return '0';
                case CalcKey.Key1: return '1';
                case CalcKey.Key2: return '2';
                case CalcKey.Key3: return '3';
                case CalcKey.Key4: return '4';
                case CalcKey.Key5: return '5';
                case CalcKey.Key6: return '6';
                case CalcKey.Key7: return '7';
                case CalcKey.Key8: return '8';
                case CalcKey.Key9: return '9';
                case CalcKey.KeyDot: return ',';
                default: return '0';

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
