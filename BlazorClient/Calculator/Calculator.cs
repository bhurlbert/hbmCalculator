using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlazorClient.Calculator
{
	public class Calculator : ICalculator
	{
		private const string OverflowMessage = "The calculation result is outside of the range that can be represented";

		
		public double Left { get; set; }
		public bool LeftIsaNumber { get; set; }
		public bool RightIsaNumber { get; set; }
		public double Right { get; set; }
		public MathOperator Operator { get; set; }
		public double CalculationResult { get; set; }
		public List<double> CalculationHistory { get; set; } = new List<double>();
		public string ErrorMessage { get; set; }
		
		
		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <param name="mathOperator"></param>
		public Calculator(string left = null, string right = null, string mathOperator = null)
		{
			SetLeft(left);
			SetRight(right);
			SetOperator(mathOperator);
		}

		/// <summary>
		/// Sets the LEFT side of the equation
		/// </summary>
		/// <param name="number"></param>
		public void SetLeft(string number)
		{
			bool isParsed = double.TryParse(number, out var result);

			if (isParsed)
			{
				Left = result;
				LeftIsaNumber = true;
			}
			else
			{
				LeftIsaNumber = false;
			}
		}

		/// <summary>
		/// Sets the RIGHT side of the equation
		/// </summary>
		/// <param name="number"></param>
		public void SetRight(string number)
		{
			var isParsed = double.TryParse(number, out var result);
			if (isParsed)
			{
				Right = result;
				RightIsaNumber = true;
			}
			else
			{
				RightIsaNumber = false;
			}
		}

		public void SetOperator(string mathOperator)
		{
			var acceptableChars = new[] { "+", "-", "*", "/" };
			if (Array.IndexOf(acceptableChars, mathOperator) > -1)
			{
				Operator = mathOperator switch
				{
					"+" => MathOperator.Add,
					"-" => MathOperator.Subtract,
					"*" => MathOperator.Multiply,
					"/" => MathOperator.Divide,
					_ => Operator
				};
			}
			else
			{
				Operator = MathOperator.None;
			}
		}

		/// <summary>
		/// Calculates the result from the number values
		/// </summary>
		/// <returns></returns>
		public string Calculate()
		{

			//Check for left and right characters that are not numbers

			var theOperator = Operator;
			double result = 0;

			try
			{
				switch (theOperator)
				{
					case MathOperator.Add:
						{
							result = Left + Right;
						}
						break;
					case MathOperator.Subtract:
						{
							result = Left - Right;
						}
						break;
					case MathOperator.Multiply:
						{
							result = Left * Right;
							if (result >= double.MaxValue)
							{
								result = 0;
								ErrorMessage = OverflowMessage;

							}
						}
						break;
					case MathOperator.Divide:
						{
							result = Right.Equals(0) ? 0 : Left / Right;
							if (result >= double.MaxValue)
							{
								result = 0;
								ErrorMessage = OverflowMessage;

							}
						}
						break;
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			CalculationResult = result;
			CalculationHistory.Add(result);
			return result.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Gets the current calculation result
		/// </summary>
		/// <returns></returns>
		public string GetResult()
		{
			return CalculationResult.ToString(CultureInfo.InvariantCulture);
		}
	}
}
