using System;
using ConsoleApp.Interfaces;
using static System.Console;

namespace ConsoleApp.Components
{
	internal sealed class SimpleConsoleView : IConsoleView
	{
		// todo move all string messages to separeted resource file
		#region Public API
		public void Initialize()
		{
			WriteLine("========== Loan payment calculator ==========");
			ClearLines();
		}

		public void Write(params string[] lines)
		{
			foreach (var line in lines)
			{
				WriteLine(line);
			}
			ClearLines();
		}

		public void OnError()
		{
			WriteLine("Fatal Error! Press any key to Quit.");
			ClearLines();
			ReadLine();
		}

		public bool NeedRepeat()
		{
			ClearLines();
			WriteLine("To repeat calculation - press ENTER, to quit - any else.");
			ClearLines();
			return ReadKey().Key == ConsoleKey.Enter;
		}

		public string GetUserInput()
		{
			return ReadLine();
		}
		#endregion

		#region Private methods
		private void ClearLines(int times = 2)
		{
			for (var i = 0; i < times; i++)
			{
				WriteLine();
			}
		}
		#endregion
	}
}