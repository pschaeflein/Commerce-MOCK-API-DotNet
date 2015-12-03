/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using MSCorp.CrestMockWebApi.Constants;
using MSCorp.CrestMockWebApi.Interfaces;
using MSCorp.CrestMockWebApi.Models.Tokens;
using MSCorp.CrestMockWebApi.Services;

namespace MSCorp.CrestMockWebApi.Repository
{
	public class InMemoryTokenRepository : ITokenRepository
	{
		private readonly TokenReponseData _testData;

		public InMemoryTokenRepository()
		{
			_testData = GetExampleSalesAgentTokenReponseData();
		}

		public TokenReponseData GetToken(string ADToken)
		{
			// override SD token with AD token to get around auth issues in Owin
			_testData.access_token = ADToken;
			return _testData;
		}

		private static TokenReponseData GetExampleSalesAgentTokenReponseData()
		{
			return JsonObjectExtractorService.ExtractJsonObjectFromFile<TokenReponseData>(
					ResourcePathConstant.GetTokenResponseDataPath);
		}
	}
}