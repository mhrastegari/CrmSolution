using Bit.Model.Contracts;

namespace CrmSolution.Shared.Dto
{
    public partial class CustomerDto : IDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
