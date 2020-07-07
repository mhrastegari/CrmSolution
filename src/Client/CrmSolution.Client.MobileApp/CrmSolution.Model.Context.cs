
namespace Simple.OData.Client
{
	[System.CodeDom.Compiler.GeneratedCode("BitCodeGenerator", "1.0.0.0")]
    public static class CrmSolutionV1ContextExt
    {
		
			public static IBoundClient<CrmSolution.Shared.Dto.CustomerDto> Customers(this IODataClient odataClient)
			{
				return odataClient.For<CrmSolution.Shared.Dto.CustomerDto>("Customers");
			}

			
				public static IBoundClient<CrmSolution.Shared.Dto.CustomerDto> Sum(this IBoundClient<CrmSolution.Shared.Dto.CustomerDto> customersController,int n1,int n2)
				{
					return customersController.Function("Sum").Set(new 
					{ 
						n1,n2
					});
				}

			

		    }
}
