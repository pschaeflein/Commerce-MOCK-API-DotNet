/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Web.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using MSCorp.CrestMockWebApi.Models.Logging;

namespace MSCorp.CrestMockWebApi.Services
{
	public static class TableLoggingService
	{
		private static readonly StorageCredentials Creds = new StorageCredentials(WebConfigurationManager.AppSettings["StorageName"], WebConfigurationManager.AppSettings["StorageKey"]);
		private static readonly CloudStorageAccount Account = new CloudStorageAccount(Creds, true);
		private static readonly CloudTableClient Client = Account.CreateCloudTableClient();
		private static readonly CloudTable Table = Client.GetTableReference(WebConfigurationManager.AppSettings["TableName"]);

		public static void LogInformational(TrackingLogEntity entity)
		{
			try
			{
				var insertOperation = TableOperation.Insert(entity);
				Table.Execute(insertOperation);
			}
			catch
			{
				// nothing
			}
		}

	}



}