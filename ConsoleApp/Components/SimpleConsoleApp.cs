using System;
using System.Threading.Tasks;
using ConsoleApp.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Loan.Core.Model;
using Loan.Core.Calculators;
using Loan.Core.Interfaces;

namespace ConsoleApp.Components
{
	public sealed class ConsoleApp : IConsoleApp, IDisposable
	{
		#region Private Fields
		private readonly ServiceProvider _serviceProvider;
		private readonly ILogger _logger;
		private readonly IConsoleController _controller;
		#endregion

		public ConsoleApp()
		{
			_serviceProvider = new ServiceCollection()
				.AddLogging()
				.AddSingleton<IConsoleView, SimpleConsoleView>()
				.AddSingleton<IConsoleController, SimpleConsoleController>()
				.AddSingleton<ICalculator<LoanInfo, LoanPaymentsInfo>, LoanPaymentsCalculator>()
				.BuildServiceProvider();

			_serviceProvider.GetService<ILoggerFactory>()
				.AddConsole(LogLevel.Debug);

			_logger = _serviceProvider.GetService<ILoggerFactory>()
				.CreateLogger<ConsoleApp>();

			_controller = _serviceProvider.GetService<IConsoleController>();
		}

		#region Public API
		public async Task Run()
		{
			_controller.Initialize();
			_logger.LogDebug("App was initialized.");
			try
			{
				while (_controller.NeedRepeat)
				{
					await _controller.Iterate();
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Fatal Error. Please Contact Your System Administrator.");
				_controller.OnError(ex);
			}
		}

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			_serviceProvider.Dispose();
		}
		#endregion
	}
}