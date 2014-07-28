using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.Collections.Generic;

namespace Calc
{
    [Activity(Label = "Calc", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

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

			TextView resultField = FindViewById<TextView>(Resource.Id.ResultField);
			TextView txt = FindViewById<TextView>(Resource.Id.TextView);
			resultField.Enabled = false;

			List<string> parameters = new List<string>();

			#region button[0-9] handlers
			button0.Click += (object sender, EventArgs e) => {
				resultField.Text += 0;
			};

			button1.Click += (object sender, EventArgs e) => {
				resultField.Text += 1;
			};

			button2.Click += (object sender, EventArgs e) => {
				resultField.Text += 2;
			};

			button3.Click += (object sender, EventArgs e) => {
				resultField.Text += 3;
			};

			button4.Click += (object sender, EventArgs e) => {
				resultField.Text += 4;
			};

			button5.Click += (object sender, EventArgs e) => {
				resultField.Text += 5;
			};

			button6.Click += (object sender, EventArgs e) => {
				resultField.Text += 6;
			};

			button7.Click += (object sender, EventArgs e) => {
				resultField.Text += 7;    
			};

			button8.Click += (object sender, EventArgs e) => {
				resultField.Text += 8;
			};

			button9.Click += (object sender, EventArgs e) => {
				resultField.Text += 9;
			};

			#endregion

			IOperation op = null;

			buttonAddition.Click += (object sender, EventArgs e) =>
			{
				if (parameters.Count == 0 && string.IsNullOrEmpty(resultField.Text))
					return;
				if (op == null)
				{
					op = new Addition();
					parameters.Add(resultField.Text);
					txt.Text += resultField.Text + "+";
					resultField.Text = string.Empty;
				}
				else if (!(op.GetType() == typeof(Addition)))
				{
					op = new Addition();
					StringBuilder sb = new StringBuilder();
					sb.Append(txt.Text);
					sb[sb.Length - 1] = '+';
					txt.Text = sb.ToString();
				}
			};

			buttonSubstraction.Click += (object sender, EventArgs e) =>
			{
				if (parameters.Count == 0 && string.IsNullOrEmpty(resultField.Text))
					return;
				if (op == null)
				{
					op = new Subtraction();
					parameters.Add(resultField.Text);
					txt.Text += resultField.Text + "-";
					resultField.Text = string.Empty;
				}
				else if (!(op.GetType() == typeof(Subtraction)))
				{
					op = new Subtraction();
					StringBuilder sb = new StringBuilder();
					sb.Append(txt.Text);
					sb[sb.Length - 1] = '-';
					txt.Text = sb.ToString();
				}
			};

			buttonEqual.Click += (object sender, EventArgs e) =>
			{

				parameters.Add(resultField.Text);
				txt.Text = string.Empty;
				//txt.Text = Eval(op, this.Parse(parameters)).ToString();
				resultField.Text = Eval(op, this.Parse(parameters)).ToString();
				parameters.Clear();

			};

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
	}
}
