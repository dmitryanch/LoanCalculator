using Loan.Core.Interfaces;
using Loan.Core.Model;
using static Loan.Core.Ext.DecimalExt;

namespace Loan.Core.Calculators
{
	public sealed class LoanPaymentsCalculator : ICalculator<LoanInfo, LoanPaymentsInfo>
	{
		/// <summary>
		/// Calculate loan payments for given info
		/// </summary>
		/// <param name="loan">Loan Info (Amount, Interest, etc.)</param>
		/// <returns>Payments Info object</returns>
		public LoanPaymentsInfo Calculate(LoanInfo loan)
		{
			var loanAmount = loan.Amount - loan.Downpayment;
			if(loanAmount <= 0)
			{
				return null;
			}
			var monthlyInterest = loan.Interest / 12;
			var months = loan.Term * 12;

			var monthlyPayment = 
				loanAmount * monthlyInterest * Pow(1 + monthlyInterest, months) 
				/ (Pow(1 + monthlyInterest, months) - 1);

			var totalPayment = monthlyPayment * months;

			var remainder = loanAmount;
			var totalInterest = 0m;
			var montlyInterestCoefficient = 12m * (365 * 4 + 1) / (48 * 365.25m);	// average days per month and days per year are presented here
			for (var j = 0; j < months; j++)
			{
				var monthlyInterestPayment = remainder * monthlyInterest * montlyInterestCoefficient;
				remainder += monthlyInterestPayment - monthlyPayment;
				totalInterest += monthlyInterestPayment;
			}

			return new LoanPaymentsInfo
			{
				MonthlyPayment = monthlyPayment,
				TotalInterest = totalInterest,
				TotalPayment = totalPayment
			};
		}
	}
}
