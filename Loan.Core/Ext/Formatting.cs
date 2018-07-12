using Loan.Core.JsonConverters;
using Loan.Core.Model;
using Newtonsoft.Json;

namespace Loan.Core.Ext
{
	public static class Formatting
	{
		public static string JsonStringify(this LoanPaymentsInfo payments)
		{
			return JsonConvert.SerializeObject(payments, Newtonsoft.Json.Formatting.Indented,
				new JsonSerializerSettings
				{
					Converters = new[] { new DecimalConverter() }
				});
		}

	}
}
