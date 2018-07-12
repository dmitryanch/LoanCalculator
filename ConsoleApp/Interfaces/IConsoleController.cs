using System;
using System.Threading.Tasks;

namespace ConsoleApp.Interfaces
{
	public interface IConsoleController
	{
		void Initialize();
		Task Iterate();
		bool NeedRepeat { get; }
		void OnError(Exception ex);
	}
}