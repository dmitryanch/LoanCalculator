using System;
using System.Linq;
using System.Threading.Tasks;
using ConsoleApp.Interfaces;
using Loan.Core.Ext;
using Loan.Core.Interfaces;
using Loan.Core.Model;
using Loan.Core.Model.DTO;
using Microsoft.Extensions.Logging;

namespace ConsoleApp.Components
{
	internal sealed class SimpleConsoleController : IConsoleController
	{
		#region Private Fields
		IConsoleView _view;
		private readonly ILogger _logger;
		private readonly ICalculator<LoanInfo, LoanPaymentsInfo> _calculator;
		private bool _needRepeat = true;
		#endregion

		public SimpleConsoleController(IConsoleView view, ILoggerFactory loggerFactory, 
			ICalculator<LoanInfo, LoanPaymentsInfo> calculator)
		{
			_view = view;
			_logger = loggerFactory.CreateLogger<SimpleConsoleController>();
			_calculator = calculator;
		}

		#region Public API
		public void Initialize()
		{
			_logger.LogDebug("Controller initializing");
			_view.Initialize();
			_logger.LogDebug("Controller initialized");
		}

		public Task Iterate()
		{
			_logger.LogDebug("Iteration started");
			var loan = GetLoanInfo();

			_logger.LogDebug("Calculation starting");
			var payments = _calculator.Calculate(loan);
			_logger.LogDebug("Calculation finished");

			_view.Write("Calculated payments:");
			_view.Write(payments?.JsonStringify() ?? "Incorrect input parameters (amount, downpayment).");

			_needRepeat = _view.NeedRepeat();
			_logger.LogDebug("Iteration finished");
			return Task.CompletedTask;
		}

		public void OnError(Exception ex)
		{
			_logger.LogError(ex, "Fatal Error");
			_view.OnError();
		}

		public bool NeedRepeat => _needRepeat;
		#endregion

		#region Private Methods
		private LoanInfo GetLoanInfo()
		{
			string input;
			var loanDTO = new LoanInfoDTO();
			_view.Write("Please enter loan parameters:");
			startInput:
			while (!string.IsNullOrEmpty(input = _view.GetUserInput()))
			{
				loanDTO.TryPopulate(input);
			}
			var loan = loanDTO.TryParse(out string[] errors);
			if (errors != null && errors.Any())
			{
				_view.Write(errors);
				_view.Write("Please correct input parameters.");
				goto startInput;
			}
			return loan;
		}
		#endregion
	}
}
