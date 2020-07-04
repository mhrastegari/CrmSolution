using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Bit.Android;
using Bit.ViewModel;
using Bit.ViewModel.Implementations;
using CrmSolution.Client.MobileApp.Impl;
using Prism.Autofac;
using Prism.Ioc;

namespace CrmSolution.Client.MobileApp.Droid
{
    [Activity(Label = "CrmSolution.Client.MobileApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : BitFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            LocalTelemetryService.Current.Init();

            BitExceptionHandler.Current = new CrmSolutionExceptionHandler();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            UseDefaultConfiguration(savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(new CrmSolutionAndroidPlatformInitializer(this)));
        }
    }

    public class CrmSolutionAndroidPlatformInitializer : BitPlatformInitializer
    {
        public CrmSolutionAndroidPlatformInitializer(Activity activity)
            : base(activity)
        {
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ContainerBuilder containerBuilder = containerRegistry.GetBuilder();

            base.RegisterTypes(containerRegistry);
        }
    }
}