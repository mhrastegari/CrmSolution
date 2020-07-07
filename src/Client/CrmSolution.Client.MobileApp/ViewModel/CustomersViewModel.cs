using Bit.ViewModel;
using CrmSolution.Shared.Dto;
using Prism.Navigation;
using Simple.OData.Client;
using System.Linq;
using System.Threading.Tasks;

namespace CrmSolution.Client.MobileApp.ViewModel
{
    public class CustomersViewModel : BitViewModelBase
    {
        public IODataClient ODataClient { get; set; }

        public async override Task OnInitializeAsync(INavigationParameters parameters)
        {
            /*CustomerDto customer1 = await ODataClient.Customers()
                .Set(new CustomerDto { FirstName = "Ali", LastName = "Ahmadi" })
                .InsertEntryAsync();

            CustomerDto customer2 = await ODataClient.Customers()
                .Set(new CustomerDto { FirstName = "Reza", LastName = "Ahmadi" })
                .InsertEntryAsync();

            CustomerDto customer3 = await ODataClient.Customers()
                .Set(new CustomerDto { FirstName = "Zahra", LastName = "Akbari" })
                .InsertEntryAsync();

            customer1.LastName = "Mohammadi";

            await ODataClient.Customers()
                .Key(customer1.Id)
                .Set(customer1)
                .UpdateEntryAsync();

            CustomerDto[] customers = (await ODataClient.Customers()
                .FindEntriesAsync()).ToArray();

            CustomerDto[] customers2 = (await ODataClient.Customers()
                .Where(c => c.FirstName.Contains("ali") || c.LastName.Contains("ali"))
                .FindEntriesAsync()).ToArray();*/

            int result = await ODataClient.Customers()
                .Sum(1, 2)
                .ExecuteAsScalarAsync<int>();

            await base.OnInitializeAsync(parameters);
        }
    }
}
