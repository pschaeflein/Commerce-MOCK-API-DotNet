# Commerce-MOCK-API-DotNet

These are samples in C# to mock the commerce APIs for Microsoft Partner Center. 
These CREST APIs are documented at [https://msdn.microsoft.com/en-us/library/partnercenter/dn974944.aspx](https://msdn.microsoft.com/en-us/library/partnercenter/dn974944.aspx).


A public forum for discussing the APIs is available at 
[https://social.msdn.microsoft.com/Forums/en-US/home?forum=partnercenterapi](https://social.msdn.microsoft.com/Forums/en-US/home?forum=partnercenterapi).

This mock implementation includes a subset of the APIs:

- Create Customer
- Get Customer by ID
- Get Subscription by ID
- Get Order by ID
- Create Order

The project also includes a sample client application that calls these APIs. A sample that covers
the complete CREST API is available at [https://github.com/PartnerCenterSamples/Commerce-API-DotNet](https://github.com/PartnerCenterSamples/Commerce-API-DotNet)

## Running the sample

No configuration is necessary. Ensure that the Microsoft.Partner.CSP.Api.Sample project is set as the 
startup project. The press F5. The MOCK API service project will start automatically
