using Loan.Core.Model;
using Loan.Core.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Loan.Core.Ext
{
	public static class ParsingExt
	{
		/// <summary>
		/// Populate properties of Loan Info object from user console input
		/// </summary>
		/// <param name="loan">Loan info object</param>
		/// <param name="userInput">Console input</param>
		/// <returns>Is Populated</returns>
		public static bool TryPopulate(this LoanInfoDTO loan, string userInput)
		{
			var tokens = userInput.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
				.Select(s => s.Trim()).ToArray();
			if (tokens.Length != 2)
			{
				return false;
			}
			var property = tokens[0].ToLower();
			var value = tokens[1]
				.Replace(" ", "").Replace("'", "").Replace(",", ""); // replace some possible thousand separatore
			switch (property)
			{
				case "amount":
					loan.Amount = value;
					break;
				case "interest":
					loan.Interest = value;
					break;
				case "downpayment":
					loan.Downpayment = value;
					break;
				case "term":
					loan.Term = value;
					break;
				default:
					return false;
			}
			return true;
		}

		/// <summary>
		/// Parses Loan info properties
		/// </summary>
		/// <param name="loan">Initial Loan info object</param>
		/// <param name="errors">Output error messages after parsing</param>
		/// <returns>Parsed Loan Info object</returns>
		public static LoanInfo TryParse(this LoanInfoDTO loan, out string[] errors)
		{
			var errorMessages = new List<string>();
			LoanInfo parsedLoan = new LoanInfo();
			if (string.IsNullOrEmpty(loan.Amount) || !decimal.TryParse(loan.Amount, out var amount))
			{
				errorMessages.Add("The Loan Amount has incorrect format.");
			}
			else
			{
				parsedLoan.Amount = amount;
			}
			if (string.IsNullOrEmpty(loan.Downpayment) || !decimal.TryParse(loan.Downpayment, out var downpayment))
			{
				errorMessages.Add("The Loan Downpayment has incorrect format.");
			}
			else
			{
				parsedLoan.Downpayment = downpayment;
			}
			if (string.IsNullOrEmpty(loan.Interest)
				|| !decimal.TryParse(loan.Interest.Replace("%", "").Trim(), out var interest))
			{
				errorMessages.Add("The Loan Interest has incorrect format.");
			}
			else
			{
				parsedLoan.Interest = loan.Interest.Contains("%") ? interest / 100 : interest;
			}
			if (string.IsNullOrEmpty(loan.Term) || !int.TryParse(loan.Term, out var term))
			{
				errorMessages.Add("The Loan Term has incorrect format.");
			}
			else
			{
				parsedLoan.Term = term;
			}
			errors = errorMessages.ToArray();
			return errorMessages.Any() ? null : parsedLoan;
		}
	}
}
