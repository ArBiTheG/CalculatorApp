using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorApp.View
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            window.KeyDown += HandleKeyPress;
        }
        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
           
            Key key = e.Key;
            Trace.WriteLine($"Нажата клавиша на клавиатуре: {key}");
            switch (key) { 
                case Key.NumPad0:
                case Key.D0:
                    ButtonClick(btnKey0);
                    break;
                case Key.NumPad1:
                case Key.D1:
                    ButtonClick(btnKey1);
                    break;
                case Key.NumPad2:
                case Key.D2:
                    ButtonClick(btnKey2);
                    break;
                case Key.NumPad3:
                case Key.D3:
                    ButtonClick(btnKey3);
                    break;
                case Key.NumPad4:
                case Key.D4:
                    ButtonClick(btnKey4);
                    break;
                case Key.NumPad5:
                case Key.D5:
                    ButtonClick(btnKey5);
                    break;
                case Key.NumPad6:
                case Key.D6:
                    ButtonClick(btnKey6);
                    break;
                case Key.NumPad7:
                case Key.D7:
                    ButtonClick(btnKey7);
                    break;
                case Key.NumPad8:
                case Key.D8:
                    ButtonClick(btnKey8);
                    break;
                case Key.NumPad9:
                case Key.D9:
                    ButtonClick(btnKey9);
                    break;
                case Key.Decimal:
                    ButtonClick(btnKeyDot);
                    break;
                case Key.OemPlus:
                case Key.Add:
                    ButtonClick(btnKeyPlus);
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    ButtonClick(btnKeyMinus);
                    break;
                case Key.Multiply:
                    ButtonClick(btnKeyMultiply);
                    break;
                case Key.Divide:
                    ButtonClick(btnKeyDivide);
                    break;
                case Key.Return:
                    ButtonClick(btnKeyEqually);
                    break;
                case Key.Back:
                    ButtonClick(btnKeyDelete);
                    break;
            }
        }

        public void ButtonClick(Button button)
        {
            button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            if (button.Command != null)
            {
                var parameter = button.CommandParameter;
                if (button.Command.CanExecute(parameter))
                    button.Command.Execute(parameter);
            }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 600)
            {
                historyColumn.Width = new GridLength(3, GridUnitType.Star);
            }
            else
            {
                historyColumn.Width = new GridLength(0, GridUnitType.Star);
            }
        }
    }
}
