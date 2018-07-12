namespace ConsoleApp.Interfaces
{
	public interface IConsoleView
	{
		void Initialize();
		void Write(params string[] lines);
		string GetUserInput();
		void OnError();
		bool OnSuccess();
	}
}