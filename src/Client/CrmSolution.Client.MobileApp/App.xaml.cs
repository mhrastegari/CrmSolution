using Autofac;
using Bit;
using Bit.Core.Contracts;
using Bit.Core.Implementations;
using Bit.View;
using CrmSolution.Client.MobileApp.View;
using CrmSolution.Client.MobileApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Prism;
using Prism.Ioc;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrmSolution.Client.MobileApp
{
    public partial class App
    {
        public static new App Current
        {
            get { return (App)Application.Current; }
        }

        public App()
            : this(null)
        {
            BitCSharpClientControls.XamlInit();
            BitApplication.XamlInit();
        }

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer)
        {
        }

        protected override async Task OnInitializedAsync()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/Nav/Customers");

            await base.OnInitializedAsync();
        }

        protected override void RegisterTypes(IDependencyManager dependencyManager, IContainerRegistry containerRegistry, ContainerBuilder containerBuilder, IServiceCollection services)
        {
            containerRegistry.RegisterForNav<NavigationPage>("Nav");
            containerRegistry.RegisterForNav<CustomersView, CustomersViewModel>("Customers");
            containerBuilder.Register<IClientAppProfile>(c => new DefaultClientAppProfile
            {
                AppName = "CrmSolution",
            }).SingleInstance();

            dependencyManager.RegisterRequiredServices();

            base.RegisterTypes(dependencyManager, containerRegistry, containerBuilder, services);
        }
    }
}
