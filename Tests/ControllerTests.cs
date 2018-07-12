using ConsoleApp.Components;
using ConsoleApp.Interfaces;
using Loan.Core.Calculators;
using Loan.Core.Interfaces;
using Loan.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;
using Unity.Lifetime;

namespace Tests
{
	[TestClass]
	public class ControllerTests
	{
		private InMemoryConsoleController _controller;
		private static bool EqualElementWise<T>(T[] seq1, T[] seq2)
		{
			if (seq1 == null || seq2 == null) return false;
			if (seq1.Length != seq2.Length) return false;
			for (var i = 0; i < seq1.Length; i++)
			{
				if (!seq1[i].Equals(seq2[i]))
				{
					return false;
				}
			}
			return true;
		}

		[TestInitialize]
		public void Setup()
		{
			var container = new UnityContainer();
			container.RegisterType<IConsoleController, InMemoryConsoleController>(new ContainerControlledLifetimeManager());
			container.RegisterType<ICalculator<LoanInfo, LoanPaymentsInfo>, LoanPaymentsCalculator>(new ContainerControlledLifetimeManager());

			_controller = (InMemoryConsoleController)container.Resolve<IConsoleController>();
		}

		[TestMethod]
		public void TestMethod1()
		{
			_controller.Buffer = new[]
			{
				"amount: 100000",
				"interest: 5.5 %",
				"downpayment: 20000",
				"term: 30"
			};
			_controller.Iterate();
			var answer =
@"{
  ""monthly payment"": 453.74,
  ""total interest"": 83796.92,
  ""total payment"": 163345.26
}";
			Assert.IsTrue(string.Equals(_controller.Output, answer));
		}
	}
}
