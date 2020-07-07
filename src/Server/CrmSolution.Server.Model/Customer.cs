using Bit.Model.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmSolution.Server.Model
{
    [Table("Customers")]
    public class Customer : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
