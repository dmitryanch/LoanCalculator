using ConsoleApp.Interfaces;
using Loan.Core.Ext;
using Loan.Core.Interfaces;
using Loan.Core.Model;
using Loan.Core.Model.DTO;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Components
{
	public class InMemoryConsoleController : IConsoleController
	{
		#region Private Fields and Properties
		private readonly ICalculator<LoanInfo, LoanPaymentsInfo> _calculator;
		private int _counter;
		#endregion

		public InMemoryConsoleController(ICalculator<LoanInfo, LoanPaymentsInfo> calculator)
		{
			_calculator = calculator;
		}

		#region Public API
		public string[] Buffer { get; set; }
		public string Output { get; set; }
		public bool NeedRepeat => throw new NotImplementedException();

		public void Initialize()
		{
			throw new NotImplementedException();
		}

		public Task Iterate()
		{
			var loan = GetLoanInfo();
			var payments = _calculator.Calculate(loan);
			Output = payments?.JsonStringify();
			return Task.CompletedTask;
		}

		public void OnError(Exception ex)
		{
			throw new NotImplementedException();
		}
		#endregion

		#region Private Methods
		private LoanInfo GetLoanInfo()
		{
			string input;
			var loanDTO = new LoanInfoDTO();
			while (_counter < Buffer.Length)
			{
				input = Buffer[_counter++];
				loanDTO.TryPopulate(input);
			}
			var loan = loanDTO.TryParse(out string[] errors);
			return loan;
		}
		#endregion
	}
}
