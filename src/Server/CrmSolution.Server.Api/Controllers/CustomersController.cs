using Bit.Core.Exceptions;
using Bit.OData.ODataControllers;
using CrmSolution.Server.Model;
using CrmSolution.Shared.Dto;

namespace CrmSolution.Server.Api.Controllers
{
    public class CustomersController : DtoSetController<CustomerDto, Customer, int>
    {
        [Function]
        public int Sum(int n1, int n2)
        {
            if (n1 < 0 || n2 < 0)
                throw new BadRequestException(")-:");

            return n1 + n2;
        }
    }
}
