/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using MSCorp.CrestMockWebApi.Models.Customer;

namespace MSCorp.CrestMockWebApi.Interfaces
{
	public interface ICustomerRepository
	{
		CreateCustomerResponseData CreateCustomer(CreateCustomerRequestData createCustomerRequestData);
		Customer GetCustomerByIdentity(GetByIdentityRequest id);
	}
}
