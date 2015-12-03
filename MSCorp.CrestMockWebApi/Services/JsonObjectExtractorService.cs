/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using MSCorp.CrestMockWebApi.Helpers;
using Newtonsoft.Json;

namespace MSCorp.CrestMockWebApi.Services
{
	public static class JsonObjectExtractorService
	{
		public static T ExtractJsonObjectFromFile<T>(string path)
		{
			Argument.CheckIfNullOrEmpty(path, "path");
			string data;

			using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile(path)))
			{
				data = sr.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<T>(data);
		}

		public static List<T> ExtractMultipleJsonObjectsFromFile<T>(string path)
		{
			Argument.CheckIfNullOrEmpty(path, "path");
			string data;

			using (StreamReader sr = new StreamReader(VirtualPathProvider.OpenFile(path)))
			{
				data = sr.ReadToEnd();
			}

			return JsonConvert.DeserializeObject<List<T>>(data);
		}
	}
}