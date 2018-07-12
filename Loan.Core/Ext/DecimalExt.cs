namespace Loan.Core.Ext
{
    public static class DecimalExt
    {
		public static decimal Pow(decimal b, int e)
		{
			decimal multuple = b;
			for(var i = 0; i < e; i++)
			{
				multuple *= b;
			}
			return multuple;
		}
    }
}
