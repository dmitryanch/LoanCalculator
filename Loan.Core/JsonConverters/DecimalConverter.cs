﻿using Newtonsoft.Json;
using System;
using System.Globalization;

namespace Loan.Core.JsonConverters
{
	public sealed class DecimalConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(decimal);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteRawValue(((decimal)value).ToString("F2", CultureInfo.InvariantCulture));
		}
	}
}
