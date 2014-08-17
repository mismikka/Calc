using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Calc
{

    [Activity(Label = "Ultra Super Mega Jesus Calculator", MainLauncher = true, Icon = "@drawable/CalcIcon")]
    internal sealed class MainActivity : Activity
    { 
        private enum Keys
        { 
            Digit, 
            Operator, 
            Equal, 
            DecimalPoint,
            Sign,
            Unary
        }

        private Keys? _lastKeyInput = null;
        IOperation op = null;
        List<string> parameters = new List<string>();

        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

            ActionBar.Tab tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.calcTab));
            tab.TabSelected += (sender, args) =>
            {
                // Do something when tab is selected
            };
            ActionBar.AddTab(tab);

            tab = ActionBar.NewTab();
            tab.SetText(Resources.GetString(Resource.String.convertTab));
            tab.TabSelected += (sender, args) =>
            {
                // Do something when tab is selected
            };
            ActionBar.AddTab(tab);

			Button button0 = FindViewById<Button>(Resource.Id.Button0);
			Button button1 = FindViewById<Button>(Resource.Id.Button1);
			Button button2 = FindViewById<Button>(Resource.Id.Button2);
			Button button3 = FindViewById<Button>(Resource.Id.Button3);
			Button button4 = FindViewById<Button>(Resource.Id.Button4);
			Button button5 = FindViewById<Button>(Resource.Id.Button5);
			Button button6 = FindViewById<Button>(Resource.Id.Button6);
			Button button7 = FindViewById<Button>(Resource.Id.Button7);
			Button button8 = FindViewById<Button>(Resource.Id.Button8);
			Button button9 = FindViewById<Button>(Resource.Id.Button9);

			Button buttonAddition = FindViewById<Button>(Resource.Id.ButtonAdd);
			Button buttonDivision = FindViewById<Button>(Resource.Id.ButtonDiv);
			Button buttonFraction = FindViewById<Button>(Resource.Id.ButtonFraction);
			Button buttonMultiplication = FindViewById<Button>(Resource.Id.ButtonMulti);
			Button buttonNegation = FindViewById<Button>(Resource.Id.ButtonNegate);
			Button buttonSqrt = FindViewById<Button>(Resource.Id.ButtonSqrt);
			Button buttonSubstraction = FindViewById<Button>(Resource.Id.ButtonSub);
			Button buttonEqual = FindViewById<Button>(Resource.Id.ButtonEqual);
            Button buttonBackspace = FindViewById<Button>(Resource.Id.ButtonBackspace);
            Button buttonComma = FindViewById<Button>(Resource.Id.ButtonComma);

			Button buttonC = FindViewById<Button>(Resource.Id.ButtonC);

            TextView resultField = FindViewById<TextView>(Resource.Id.textView1);
			TextView txt = FindViewById<TextView>(Resource.Id.TextView);

            Button sin = FindViewById<Button>(Resource.Id.sin);
            Button asin = FindViewById<Button>(Resource.Id.asin);
            Button cos = FindViewById<Button>(Resource.Id.cos);
            Button acos = FindViewById<Button>(Resource.Id.acos);
            Button tan = FindViewById<Button>(Resource.Id.tan);
            Button atan = FindViewById<Button>(Resource.Id.atan);
            Button ctg = FindViewById<Button>(Resource.Id.ctg);
            Button actg = FindViewById<Button>(Resource.Id.actg);
            Button ln = FindViewById<Button>(Resource.Id.ln); // e
            Button log = FindViewById<Button>(Resource.Id.log); // 10
            Button pi = FindViewById<Button>(Resource.Id.pi);
            Button exp = FindViewById<Button>(Resource.Id.exp);
            Button pov = FindViewById<Button>(Resource.Id.pov);
            Button rand = FindViewById<Button>(Resource.Id.rand);
            Button mod = FindViewById<Button>(Resource.Id.mod);
            Button div = FindViewById<Button>(Resource.Id.div);

			#region button[0-9] handlers
            
			button0.Click += (object sender, EventArgs e) =>
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
                    resultField.Text += 0;
                    _lastKeyInput = Keys.Digit;
                };

			button1.Click += (object sender, EventArgs e) =>
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
                    resultField.Text += 1;
                    _lastKeyInput = Keys.Digit;
                };

			button2.Click += (object sender, EventArgs e) =>
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
                    resultField.Text += 2;
                    _lastKeyInput = Keys.Digit;
                };

			button3.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 3;
                    _lastKeyInput = Keys.Digit;
                };

			button4.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 4;
                    _lastKeyInput = Keys.Digit;
                };

			button5.Click += (object sender, EventArgs e) =>
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 5;
                    _lastKeyInput = Keys.Digit;
                };

			button6.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 6;
                    _lastKeyInput = Keys.Digit;
                };

			button7.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 7;
                    _lastKeyInput = Keys.Digit;
                };

			button8.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
				    resultField.Text += 8;
                    _lastKeyInput = Keys.Digit;
                };

			button9.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Operator)
                        resultField.Text = "";
                    resultField.Text += 9;
                    _lastKeyInput = Keys.Digit;
                };

			#endregion

            buttonAddition.Click += (object sender, EventArgs e) =>
            {
                if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign)
                {
                    parameters.Add(resultField.Text);
                    txt.Text += resultField.Text + "+";
                    resultField.Text = "";

                    if (parameters.Count == 2)
                    {
                        double[] p = this.Parse(parameters);
                        double r = op.Calculate(p);
                        resultField.Text = r.ToString();
                        parameters.Clear();
                        parameters.Add(r.ToString());
                    } 
                        
                    if (op == null || op.GetType() != typeof(Addition))
                        op = new Addition();
                    _lastKeyInput = Keys.Operator;
                    return;
                }
                    
                if (_lastKeyInput == Keys.Operator)
                {
                    if (op == null || op.GetType() != typeof(Addition))
                    {
                        op = new Addition();
                        txt.Text = ReplaceLastChar(txt.Text, '+');              
                    }
                    _lastKeyInput = Keys.Operator;
                }           
            };
                    
            buttonSubstraction.Click += (object sender, EventArgs e) =>
            {
                    if (_lastKeyInput == Keys.Digit ||_lastKeyInput == Keys.Sign)
                {
                    parameters.Add(resultField.Text);
                    txt.Text += resultField.Text + "-";
                    resultField.Text = "";

                    if (parameters.Count == 2)
                    {
                        double[] p = this.Parse(parameters);
                        double r = op.Calculate(p);
                        resultField.Text = r.ToString();
                        parameters.Clear();
                        parameters.Add(r.ToString());
                    }
                        
                    if (op == null || op.GetType() != typeof(Subtraction))
                        op = new Subtraction();
                    _lastKeyInput = Keys.Operator;
                    return;
                }

                if (_lastKeyInput == Keys.Operator)
                {
                    if (op == null || op.GetType() != typeof(Subtraction))
                    {
                        op = new Subtraction();
                        txt.Text = ReplaceLastChar(txt.Text, '-');
                    }
                    _lastKeyInput = Keys.Operator;
                }
            };
                    
            buttonMultiplication.Click += (object sender, EventArgs e) =>
            {
                    if (_lastKeyInput == Keys.Digit ||_lastKeyInput == Keys.Sign)
                    {
                        parameters.Add(resultField.Text);
                        txt.Text += resultField.Text + "*";
                        resultField.Text = "";

                        if (parameters.Count == 2)
                        {
                            double[] p = this.Parse(parameters);
                            double r = op.Calculate(p);
                            resultField.Text = r.ToString();
                            parameters.Clear();
                            parameters.Add(r.ToString());
                        }
                        
                        if (op == null || op.GetType() != typeof(Multiplication))
                            op = new Multiplication();
                        
                        _lastKeyInput = Keys.Operator;
                        return;
                    }

                    if (_lastKeyInput == Keys.Operator)
                    {
                        if (op == null || op.GetType() != typeof(Multiplication))
                        {
                            op = new Multiplication();
                            txt.Text = ReplaceLastChar(txt.Text, '*');
                        }
                        _lastKeyInput = Keys.Operator;
                    }
            };
                    
            buttonDivision.Click += (object sender, EventArgs e) =>
            {
                    if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign)
                {
                    parameters.Add(resultField.Text);
                    txt.Text += resultField.Text + "/";
                    resultField.Text = "";
                        
                    if (parameters.Count == 2)
                    {
                        double[] p = this.Parse(parameters);
                        double r = op.Calculate(p);
                        resultField.Text = r.ToString();
                        parameters.Clear();
                        parameters.Add(r.ToString());
                    }
                    if (op == null || op.GetType() != typeof(Division))
                        op = new Division();
                        
                    _lastKeyInput = Keys.Operator;
                    return;
                }

                if (_lastKeyInput == Keys.Operator)
                {
                    if (op == null || op.GetType() != typeof(Division))
                    {
                        op = new Division();
                        txt.Text = ReplaceLastChar(txt.Text, '/');
                    }
                    _lastKeyInput = Keys.Operator;
                }
            };

            buttonEqual.Click += (object sender, EventArgs e) =>
            {
                    if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign)
                {
                    parameters.Add(resultField.Text);
                    txt.Text = string.Empty;
                    if (parameters.Count == 2)
                    {
                        double r = op.Calculate(this.Parse(parameters));
                        resultField.Text = r.ToString();
                    }
                        _lastKeyInput = Keys.Equal;
                    return;
                }

                if (_lastKeyInput == Keys.Operator)
                {
                    txt.Text = "";
                    resultField.Text = op.Calculate(double.Parse(parameters[0]), double.Parse(resultField.Text)).ToString();
                    parameters.Clear();
                        _lastKeyInput = Keys.Equal;
                    return;
                }

                    if (_lastKeyInput == Keys.Equal && op != null)
                {
                    resultField.Text = op.Calculate(double.Parse(resultField.Text), double.Parse(resultField.Text)).ToString();
                        _lastKeyInput = Keys.Equal;

                }
            };

            buttonNegation.Click += (object sender, EventArgs e) =>
            {
                if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign || _lastKeyInput == Keys.Equal)
                {
                    //if (op == null || op.GetType() != typeof(Negation))
                     //   op = new Negation();

                    resultField.Text = new Negation().Calculate(double.Parse(resultField.Text)).ToString();
                    //resultField.Text = op.Calculate(double.Parse(resultField.Text)).ToString();
                    _lastKeyInput = Keys.Sign;
                }
            };

            buttonSqrt.Click += (object sender, EventArgs e) => 
                {
                    if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign || _lastKeyInput == Keys.Equal)
                    {
                        resultField.Text = new Sqrt().Calculate(double.Parse(resultField.Text)).ToString();
                        _lastKeyInput = Keys.Operator;
                    }
                };

            buttonFraction.Click += (object sender, EventArgs e) =>
            {
                    if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign || _lastKeyInput == Keys.Equal)
                    {
                        resultField.Text = new Fraction().Calculate(double.Parse(resultField.Text)).ToString();
                    }
            };

            buttonBackspace.Click += (object sender, EventArgs e) =>
                {
                    if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Equal)
                    {
                        if (!(string.IsNullOrEmpty(resultField.Text)))
                            resultField.Text = resultField.Text.Substring(0, resultField.Text.Length-1);
                    }
                };

            buttonComma.Click += (object sender, EventArgs e) =>
            {
                //if (_lastKeyInput == Keys.Digit || _lastKeyInput == Keys.Sign || _lastKeyInput == Keys.Equal)
                //{
                    if (!(string.IsNullOrEmpty(resultField.Text)))
                    {
                        resultField.Text += '.';
                        //_lastKeyInput = Keys.DecimalPoint;
                    }
                    else
                        resultField.Text += "0.";
               // }
                _lastKeyInput = Keys.DecimalPoint;
            };

            buttonC.Click += (object sender, EventArgs e) =>
            {
                op = null;
                resultField.Text = string.Empty;
                txt.Text = string.Empty;
                parameters.Clear();
            };

            ViewSwitcher viewSwitcher = FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher);
            LinearLayout calcView1 = FindViewById<LinearLayout>(Resource.Id.calcView1);
            LinearLayout calcView2 = FindViewById<LinearLayout>(Resource.Id.calcView2);
            Button functions = FindViewById<Button>(Resource.Id.functions);
            Button standart = FindViewById<Button>(Resource.Id.standart);

            functions.Click += (object sender, EventArgs e) =>
            {
                if (viewSwitcher.CurrentView != calcView2)
                    viewSwitcher.ShowNext();
            };

            standart.Click += (object sender, EventArgs e) =>
            {
                if (viewSwitcher.CurrentView == calcView2)
                    viewSwitcher.ShowPrevious();

            };
		}

		public double[] Parse(List<string> parameters)
        {
            List<double> result = new List<double>();
            foreach (var p in parameters)
            {
                result.Add(double.Parse(p));
            }
            return result.ToArray();
        }
        
        public string ReplaceLastChar(string str, char ch)
        {
            StringBuilder sb = new StringBuilder(str);
            sb[sb.Length - 1] = ch;
            return sb.ToString();
        }
    }
}
