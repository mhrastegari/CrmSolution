using Bit.ViewModel;
using CrmSolution.Client.MobileApp.Contracts;
using CrmSolution.Shared.Dto;
using Prism.Navigation;
using Simple.OData.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms.StateSquid;

namespace CrmSolution.Client.MobileApp.ViewModel
{
    public class CustomerDetailViewModel : BitViewModelBase
    {
        public IODataClient ODataClient { get; set; }

        public BitDelegateCommand CancelCommand { get; set; }
        public BitDelegateCommand SaveCustomerCommand { get; set; }

        public CustomerDto Customer { get; set; }
        public ObservableCollection<CustomerDto> Customers { get; set; }

        public State LoadingState { get; set; }
        public State SaveState { get; set; }
        public OperationKind OperationKind { get; set; } = OperationKind.Edit;

        public CustomerDetailViewModel()
        {
            CancelCommand = new BitDelegateCommand(Cancel);
            SaveCustomerCommand = new BitDelegateCommand(SaveCustomer);
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            await base.OnNavigatedToAsync(parameters);

            Customer = parameters.GetValue<CustomerDto>("customerDetail");

            Customers = parameters.GetValue<ObservableCollection<CustomerDto>>("customers");

            if (Customer == null)
            {
                Customer = new CustomerDto { };
                OperationKind = OperationKind.Add;
            }
        }

        public async Task SaveCustomer()
        {
            try
            {
                SaveState = State.Loading;
                if (OperationKind == OperationKind.Edit)
                {
                    await ODataClient.Customers()
                         .Key(Customer.Id)
                         .Set(Customer)
                         .UpdateEntryAsync();
                }
                else
                {
                    Customers.Add(await ODataClient.Customers().Set(Customer).InsertEntryAsync());
                }

                await NavigationService.GoBackAsync();
            }
            finally
            {
                SaveState = State.None;
            }
        }

        public async Task Cancel()
        {
            await base.NavigationService.GoBackAsync();
        }
    }
}
