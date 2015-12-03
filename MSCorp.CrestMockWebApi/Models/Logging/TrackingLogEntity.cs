/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MSCorp.CrestMockWebApi.Models.Logging
{
	public class TrackingLogEntity : TableEntity
	{
		public TrackingLogEntity()
				: base(GeneratePartitionKey(), GenerateRowKey("Tracking"))
		{ }

		public string CorrelationId { get; set; }
		public string SessionId { get; set; }
		public string TrackingId { get; set; }
		public string MessageBody { get; set; }
		public string Uri { get; set; }

		private static string GeneratePartitionKey()
		{
			var now = DateTime.UtcNow;
			var nowToTheHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Utc);
			var partitionKey = string.Format("{0:D19}", (DateTime.MaxValue - nowToTheHour).Ticks);
			return partitionKey;
		}

		private static string GenerateRowKey(string type)
		{
			var now = DateTime.UtcNow;
			var rowKey = string.Format("{0}-{1:D19}-{2}", type, (DateTime.MaxValue - now).Ticks, Guid.NewGuid());
			return rowKey;
		}
	}
}