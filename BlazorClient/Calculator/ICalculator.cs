namespace BlazorClient.Calculator
{
	interface ICalculator
	{
		/// <summary>
		/// This method assigns the left side of the equation
		/// </summary>
		/// <param name="number"></param>
		void SetLeft(string number);
		
		/// <summary>
		/// This method assigns the right side of the equation
		/// </summary>
		/// <param name="number"></param>
		void SetRight(string number);

		/// <summary>
		/// This method assigns the math operator
		/// </summary>
		/// <param name="number"></param>
		void SetOperator(string number);

	}

}
