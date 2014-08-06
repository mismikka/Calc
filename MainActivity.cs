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
    [Activity(Label = "Calc", MainLauncher = true, Icon = "@drawable/icon")]
    internal sealed class MainActivity : Activity
    {
        private enum Keys
        { 
            Digit, 
            Operator, 
            Equal, 
            DecimalPoint,
            Sign
        }

        private Keys? _lastKeyInput = null;
        IOperation op = null;
        List<string> parameters = new List<string>();
        private double _total = 0;
        
        protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
            
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

			Button buttonC = FindViewById<Button>(Resource.Id.ButtonC);

            TextView resultField = FindViewById<TextView>(Resource.Id.textView1);
			TextView txt = FindViewById<TextView>(Resource.Id.TextView);

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
                if (_lastKeyInput == Keys.Digit)
                {
                    parameters.Add(resultField.Text);
                    txt.Text += resultField.Text + "+";
                    resultField.Text = "";
                        
                    if (op == null || op.GetType() != typeof(Addition))
                        op = new Addition();
                        
                    if (parameters.Count == 2)
                    {
                        double[] p = this.Parse(parameters);
                        double r = op.Calculate(p);
                        resultField.Text = r.ToString();
                        parameters.Clear();
                        parameters.Add(r.ToString());
                    }                        
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
                }           
            };
            
            buttonSubstraction.Click += (object sender, EventArgs e) =>
            {
                if (_lastKeyInput == Keys.Digit)
                {
                    parameters.Add(resultField.Text);
                    txt.Text += resultField.Text + "-";
                    resultField.Text = "";

                    if (op == null || op.GetType() != typeof(Subtraction))
                        op = new Subtraction();

                    if (parameters.Count == 2)
                    {
                        double[] p = this.Parse(parameters);
                        double r = op.Calculate(p);
                        resultField.Text = r.ToString();
                        parameters.Clear();
                        parameters.Add(r.ToString());
                        _total = op.Calculate(_total, r);
                        resultField.Text = _total.ToString();
                    }
                }

                if (_lastKeyInput == Keys.Operator)
                {
                    if (op == null || op.GetType() != typeof(Subtraction))
                    {
                        op = new Subtraction();
                        txt.Text = ReplaceLastChar(txt.Text, '-');              
                    }
                }
            };
            
            
            buttonEqual.Click += (object sender, EventArgs e) =>
            {
                if (_lastKeyInput == Keys.Digit)
                {
                    parameters.Add(resultField.Text);
                    txt.Text = string.Empty;
                    double r = Eval(op, this.Parse(parameters));
                    resultField.Text = r.ToString();
                }
                if (_lastKeyInput == Keys.Operator)
                {
                    txt.Text = "";
                    resultField.Text = op.Calculate(double.Parse(parameters[0]), double.Parse(resultField.Text)).ToString();
                    parameters.Clear();
                }
            };
            
            // Clear
            buttonC.Click += (object sender, EventArgs e) =>
            {
                op = null;
                resultField.Text = string.Empty;
                txt.Text = string.Empty;
                parameters.Clear();
            };
		}

		public double Eval(IOperation op, params double[] parameters)
		{
			return op.Calculate(parameters);
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