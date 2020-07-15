using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrmSolution.Client.MobileApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailView : ContentPage
    {
        public CustomerDetailView()
        {
            InitializeComponent();
        }

        public class StateToBooleanConverter : ValueConverter<State, bool>
        {
            protected override bool Convert(State value, Type targetType, object parameter, CultureInfo culture)
            {
                return value == State.Loading ? true : false;
            }
        }
    }
}