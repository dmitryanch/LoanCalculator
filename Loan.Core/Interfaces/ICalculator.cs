namespace Loan.Core.Interfaces
{
	public interface ICalculator<Tin, Tout>
	{
		Tout Calculate(Tin info);
	}
}
