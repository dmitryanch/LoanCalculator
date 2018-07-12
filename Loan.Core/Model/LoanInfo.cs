namespace Loan.Core.Model
{
	public class LoanInfo
	{
		public decimal  Amount { get; set; }
		public decimal Interest { get; set; }
		public decimal Downpayment { get; set; }
		public int Term { get; set; }
	}
}
