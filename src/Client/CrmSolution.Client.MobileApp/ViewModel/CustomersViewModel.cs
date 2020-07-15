using Bit.ViewModel;
using CrmSolution.Shared.Dto;
using Prism.Navigation;
using Simple.OData.Client;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.StateSquid;

namespace CrmSolution.Client.MobileApp.ViewModel
{
    public class CustomersViewModel : BitViewModelBase
    {
        public IODataClient ODataClient { get; set; }
        public ObservableCollection<CustomerDto> Customers { get; set; } = new ObservableCollection<CustomerDto>();

        public BitDelegateCommand<CustomerDto> CustomerDetailCommand { get; set; }
        public BitDelegateCommand NewCustomerCommand { get; set; }
        public BitDelegateCommand<CustomerDto> DeleteCustomerCommand { get; set; }

        public State LoadingState { get; set; }

        public CustomersViewModel()
        {
            CustomerDetailCommand = new BitDelegateCommand<CustomerDto>(CustomerDetail);
            NewCustomerCommand = new BitDelegateCommand(NewCustomer);
            DeleteCustomerCommand = new BitDelegateCommand<CustomerDto>(DeleteItem);
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            try
            {
                await base.OnNavigatedToAsync(parameters);
                if (parameters.GetNavigationMode() == NavigationMode.New)
                {
                    LoadingState = State.Loading;
                }
                Customers = new ObservableCollection<CustomerDto>((await ODataClient.Customers().FindEntriesAsync()).ToArray());
            }
            finally
            {
                LoadingState = State.None;
            }

        }

        private async Task DeleteItem(CustomerDto customer)
        {
            Customers.Remove(customer);
            await ODataClient.Customers()
                .Key(customer.Id)
                .Set(customer)
                .DeleteEntryAsync();
        }

        public async Task CustomerDetail(CustomerDto customerDetail)
        {
            await NavigationService.NavigateAsync("CustomerDetail", ("customerDetail", customerDetail));
        }

        public async Task NewCustomer()
        {
            await NavigationService.NavigateAsync("CustomerDetail", ("customers", Customers));
        }
    }
}
