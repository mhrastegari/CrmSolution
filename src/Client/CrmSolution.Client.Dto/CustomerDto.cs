using Bit.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmSolution.Shared.Dto
{
    public partial class CustomerDto : Bindable
    {
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
