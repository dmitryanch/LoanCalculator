using Newtonsoft.Json;

namespace Loan.Core.Model
{
	public class LoanPaymentsInfo
	{
		[JsonProperty("monthly payment")]
		public decimal MonthlyPayment { get; set; }
		[JsonProperty("total interest")]
		public decimal TotalInterest { get; set; }
		[JsonProperty("total payment")]
		public decimal TotalPayment { get; set; }
	}
}
