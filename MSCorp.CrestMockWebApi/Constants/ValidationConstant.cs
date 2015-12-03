/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Collections.Generic;
using MSCorp.CrestMockWebApi.Models.Common;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Constants
{
	public static class ValidationConstant
	{
		public static readonly List<UsaCity> UsaCitiesList  = JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<UsaCity>(ResourcePathConstant.GetCitiesDataPath);
		public static readonly List<UsaState> UsaStatesList = JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<UsaState>(ResourcePathConstant.GetStatesUsaDataPath);
		public static readonly List<string> UsaZipCodesList = JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<string>(ResourcePathConstant.GetZipCodesDataPath);
		public static readonly List<Country> CountriesList  = JsonObjectExtractorService.ExtractMultipleJsonObjectsFromFile<Country>(ResourcePathConstant.GetCountryCodesDataPath);
	}
}