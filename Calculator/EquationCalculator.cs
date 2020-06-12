using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static System.String;

namespace BlazorClient.Calculator
{
	public class EquationCalculator
	{
		private const string OverflowMessage = "The calculation result is outside of the range that can be represented";

		public string Equation { get; set; }

		public string ErrorMessage { get; set; }

		public double CalculationResult { get; set; }
		public List<double> CalculationHistory { get; set; } = new List<double>();
		

		public EquationCalculator(string equation = null)
		{
			Equation = equation;
		}

		public string Calculate()
		{
			try
			{
				double result = 0;
				var equationString = Equation
					.Replace('<', ' ')
					.Replace('>', '|')
					.Trim(' ');

				var trimmed = Concat(equationString.Where(c => !Char.IsWhiteSpace(c)));
				var equationItems = trimmed.Split('|').ToList();

				equationItems.RemoveAt(equationItems.Count-1);

				if (equationItems.Count % 2 == 0) //the total number of nodes should be odd
				{
					//Error
					return "Error: The equation is improperly formatted";
				}

				for (var i = 0; i < equationItems.Count - 1; i++)
				{
					if (i == 0)//start
					{
						var isParsed = double.TryParse(equationItems[0], out result);
						if (!isParsed)
						{
							return "Error: The equation is improperly formatted";
						}
					}

					if (i % 2 == 0) continue;
					{
						var isParsed = double.TryParse(equationItems[i + 1], out var nextOperand);

						if (!isParsed)
						{
							var errorNode = i + 2;
							return $"Error: The number value in <node> {errorNode} is invalid";
						}
						else
						{
							switch (equationItems[i])
							{
								case "+":
									{
										result += nextOperand;
									}
									break;
								case "-":
									{
										result -= nextOperand;
									}
									break;
								case "*":
									{
										result *= nextOperand;
									}
									break;
								case "/":
									{
										result /= nextOperand;
									}
									break;
								default:
									{
										return $"Error: Operator at <node> {i+1} is invalid";
									}
							}
						}
					}
				}

				CalculationResult = result;
				CalculationHistory.Add(result);
				return result.ToString(CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				CalculationResult = 0;
				ErrorMessage = ex.Message;
				return "0";
			}
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
