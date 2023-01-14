using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KeyboardTrainer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class Symbols
    {
        public Border border { get; }
        public TextBlock textblock { get; }
        public Key button { get; }
        public char symbol { get; }
        public char symbolCase { get; }
        public Symbols(Border bor, TextBlock tb, Key b, char s, char sc)
        {
            border = bor;
            textblock = tb;
            this.button = b;
            this.symbol = s;
            this.symbolCase = sc;
        }
    }
    public partial class MainWindow : Window
    {
        private static int mistakes;
        private int index;
        private Symbols[] symbols;
        private DispatcherTimer timer;
        int time;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoadForm(object sender, EventArgs e)
        {
            slider.Width *= 2;
            mistakes = 0;
            index = 0;
            symbols = new Symbols[]
            {
                new Symbols(keyApostrof, TkeyApostrof, Key.Oem3, '`', '~'), new Symbols(keyD1, TkeyD1, Key.D1, '1', '!'), new Symbols(keyD2, TkeyD2, Key.D2, '2', '@'), new Symbols(keyD3, TkeyD3, Key.D3, '3', '#'),
                new Symbols(keyD4, TkeyD4, Key.D4, '4', '$'), new Symbols(keyD5, TkeyD5, Key.D5, '5', '%'), new Symbols(keyD6, TkeyD6, Key.D6, '6', '^'), new Symbols(keyD7, TkeyD7, Key.D7, '7', '&'),
                new Symbols(keyD8, TkeyD8, Key.D8, '8', '*'), new Symbols(keyD9, TkeyD9, Key.D9, '9', '('), new Symbols(keyD0, TkeyD0, Key.D0, '0', ')'), new Symbols(keyMinus, TkeyMinus, Key.OemMinus, '-', '_'),
                new Symbols(keyPlus, TkeyPlus, Key.OemPlus, '=', '+'), new Symbols(keyLSkobka, TkeyLSkobka, Key.OemOpenBrackets, '[', '{'), new Symbols(keyRSkobka, TkeyRSkobka, Key.Oem6, ']', '}'),
                new Symbols(keySlesh, TkeySlesh, Key.Oem5, '\\', '|'), new Symbols(keyTochZap, TkeyTochZap, Key.Oem1, ';', ':'), new Symbols(keyKavichki, TkeyKavichki, Key.OemQuotes, '\'', '"'),
                new Symbols(keyZapataya, TkeyZapataya, Key.OemComma, ',', '<'), new Symbols(keyTochka, TkeyTochka, Key.OemPeriod, '.', '>'), new Symbols(keyRazdelit, TkeyRazdelit, Key.OemQuestion, '/', '?'),
                new Symbols(keyQ, TkeyQ, Key.Q, 'q', 'Q'), new Symbols(keyW, TkeyW, Key.W, 'w', 'W'), new Symbols(keyE, TkeyE, Key.E, 'e', 'E'), new Symbols(keyR, TkeyR, Key.R, 'r', 'R'),
                new Symbols(keyT, TkeyT, Key.T, 't', 'T'), new Symbols(keyY, TkeyY, Key.Y, 'y', 'Y'), new Symbols(keyU, TkeyU, Key.U, 'u', 'U'), new Symbols(keyI, TkeyI, Key.I, 'i', 'I'),
                new Symbols(keyO, TkeyO, Key.O, 'o', 'O'), new Symbols(keyP, TkeyP, Key.P, 'p', 'P'), new Symbols(keyA, TkeyA, Key.A, 'a', 'A'), new Symbols(keyS, TkeyS, Key.S, 's', 'S'),
                new Symbols(keyD, TkeyD, Key.D, 'd', 'D'), new Symbols(keyF, TkeyF, Key.F, 'f', 'F'), new Symbols(keyG, TkeyG, Key.G, 'g', 'G'), new Symbols(keyH, TkeyH, Key.H, 'h', 'H'),
                new Symbols(keyJ, TkeyJ, Key.J, 'j', 'J'), new Symbols(keyK, TkeyK, Key.K, 'k', 'K'), new Symbols(keyL, TkeyL, Key.L, 'l', 'L'), new Symbols(keyZ, TkeyZ, Key.Z, 'z', 'Z'),
                new Symbols(keyX, TkeyX, Key.X, 'x', 'X'), new Symbols(keyC, TkeyC, Key.C, 'c', 'C'), new Symbols(keyV, TkeyV, Key.V, 'v', 'V'), new Symbols(keyB, TkeyB, Key.B, 'b', 'B'),
                new Symbols(keyN, TkeyN, Key.N, 'n', 'N'), new Symbols(keyM, TkeyM, Key.M, 'm', 'M')
            };       
        }
        private void ValueSliderChanged(object sender, EventArgs e)
        {
            difficult.Text = slider.Value.ToString();
        }
        private void genText(int level)
        {
            if (text.Text.Length == 0)
            {
                int n = level * 100;
                Random rand = new Random();
                for (int i = 0; i < n; ++i)
                {
                    if ((bool)registr.IsChecked)
                    {
                        text.Text += (char)rand.Next(65, 91);
                        ++i;
                    }
                    int n1 = rand.Next(4, 11);
                    for (int j = 0; j < n1; ++j, ++i)
                    {
                        text.Text += (char)rand.Next(97, 123);
                    }
                    text.Text += " ";
                    n1 = rand.Next(5);
                    for (int j = 0; j < n1; ++j, ++i)
                    {
                        char ch = (char)rand.Next(33, 126);
                        if ((ch >= 65 && ch <= 90) || (ch >= 97 && ch <= 126))
                        {
                            ch -= (char)26;
                            if (ch >= 65 && ch <= 90) ch -= (char)26;
                        }
                        text.Text += ch;
                    }
                    if (n1 != 0) text.Text += " ";
                }
            }
        }
        private void ClickStart(object sender, EventArgs e)
        {
            if (start.Opacity == 1)
            {
                start.Opacity = 0.5;
                stop.Opacity = 1;
                genText((int)slider.Value);
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += Timer_Tick;
                timer.Start();
                time = 0;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (text.Text.Length > index)
            {
                time += TimeSpan.FromSeconds(1).Seconds;
                speed.Text = ((int)(textW.Text.Length / ((float)time / 60))).ToString();
            }
            else
                timer.Stop();
        }

        private void ClickStop(object sender, EventArgs e)
        {
            if (stop.Opacity == 1)
            {
                text.Text = "";
                textW.Text = "";
                stop.Opacity = 0.5;
                start.Opacity = 1;
                index = 0;
                mistakes = 0;
                fails.Text = "0";
                speed.Text = "0";
                timer.Stop();
            }
        }
        
        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (text.Text == "" || text.Text.Length == textW.Text.Length) return;
            
            if (e.KeyboardDevice.IsKeyDown(Key.RightShift))
            {
                keyRShift.Opacity = 0.3;
                ChangeKeyboard();
            }
            else
            if (e.KeyboardDevice.IsKeyDown(Key.LeftShift))
            {
                keyLShift.Opacity = 0.3;
                ChangeKeyboard();
            }

            foreach (var s in symbols)
            {
                if (e.Key == s.button)
                {
                    s.border.Opacity = 0.3;
                    if (e.KeyboardDevice.Modifiers == ModifierKeys.Shift || keyCaps.Opacity != 1)
                    {
                        if (text.Text[index] != s.symbolCase)
                        {
                            mistakes++;
                            Run r = new Run();
                            r.Foreground = new SolidColorBrush(Colors.Red);
                            r.Text = "" + s.symbolCase;
                            textW.Inlines.Add(r);
                        }
                        else
                        {
                            Run r = new Run();
                            r.Foreground = new SolidColorBrush(Colors.Black);
                            r.Text = "" + s.symbolCase;
                            textW.Inlines.Add(r);
                        }
                    }
                    else
                    {
                        if (text.Text[index] != s.symbol)
                        {
                            mistakes++;
                            Run r = new Run();
                            r.Foreground = new SolidColorBrush(Colors.Red);
                            r.Text = "" + s.symbol;
                            textW.Inlines.Add(r);
                        }
                        else
                        {
                            Run r = new Run();
                            r.Foreground = new SolidColorBrush(Colors.Black);
                            r.Text = "" + s.symbol;
                            textW.Inlines.Add(r);
                        }
                    }
                    fails.Text = mistakes.ToString();
                    index++;
                    break;
                }
            }
            
            switch (e.Key)
            {
                case Key.Space:
                    {
                        keySpace.Opacity = 0.3;
                        Run r = new Run();
                        r.Text = " ";
                        textW.Inlines.Add(r);
                        index++;
                        break;
                    }
                case Key.Back:
                    {
                        keyBack.Opacity = 0.3;
                        if (textW.Text != "")
                        {
                            Run r = new Run();
                            textW.Inlines.Remove(textW.Inlines.LastInline);
                            index--;
                        }
                        break;
                    }
                case Key.Tab:
                    {
                        keyTab.Opacity = 0.3;
                        Run r = new Run();
                        r.Text = "    ";
                        textW.Inlines.Add(r);
                        index++;
                        break;
                    }
                case Key.CapsLock:
                    {
                        if (keyCaps.Opacity == 1)
                            keyCaps.Opacity = 0.3;
                        else
                            keyCaps.Opacity = 1;
                        ChangeKeyboard();
                        break;
                    }
                case Key.Enter:
                    {
                        keyEnter.Opacity = 0.3;
                        Run r = new Run();
                        r.Text = "\n";
                        textW.Inlines.Add(r);
                        index++;
                        break;
                    }
                case Key.LeftAlt:
                    {
                        keyLAlt.Opacity = 0.3;
                        break;
                    }
                case Key.RightAlt:
                    {
                        keyRAlt.Opacity = 0.3;
                        break;
                    }
                case Key.LeftCtrl:
                    {
                        keyLCtrl.Opacity = 0.3;
                        break;
                    }
                case Key.RightCtrl:
                    {
                        keyRCtrl.Opacity = 0.3;
                        break;
                    }
                case Key.LWin:
                    {
                        keyLWin.Opacity = 0.3;
                        break;
                    }
                case Key.RWin:
                    {
                        keyRWin.Opacity = 0.3;
                        break;
                    }
                default: break;
            }
            e.Handled = true;
        }
        private void KeyAbsolve(object sender, KeyEventArgs e)
        {
            foreach (var s in symbols)
            {
                if (s.border.Opacity != 1)
                    s.border.Opacity = 1;
            }
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyRShift.Opacity = 1;
                keyLShift.Opacity = 1;
                ChangeKeyboard();
            }
            keyBack.Opacity = 1;
            keyTab.Opacity = 1;
            keyEnter.Opacity = 1;
            keyLAlt.Opacity = 1;
            keyRAlt.Opacity = 1;
            keyLCtrl.Opacity = 1;
            keyRCtrl.Opacity = 1;
            keyLWin.Opacity = 1;
            keyRWin.Opacity = 1;
            keySpace.Opacity = 1;
        }
        private void ChangeKeyboard()
        {
            if (keyRShift.Opacity != 1 || keyLShift.Opacity != 1 || keyCaps.Opacity != 1)
                foreach (var s in symbols)
                    s.textblock.Text = "" + s.symbolCase;
            else
                foreach (var s in symbols)
                    s.textblock.Text = "" + s.symbol;
        }
    }
}