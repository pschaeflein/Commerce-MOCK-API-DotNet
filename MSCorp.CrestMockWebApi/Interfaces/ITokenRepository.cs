/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using MSCorp.CrestMockWebApi.Models.Tokens;

namespace MSCorp.CrestMockWebApi.Interfaces
{
	public interface ITokenRepository
	{
		TokenReponseData GetToken(string ADToken);
	}
}
