using ConsoleApp.Interfaces;
using System.Threading.Tasks;

namespace Client.NetCore
{
    class Program
    {
		private static readonly IConsoleApp _app = new ConsoleApp.Components.ConsoleApp();

		static async Task Main(string[] args)
		{
			await _app.Run();
		}
	}
}
