using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedP : TabbedPage
    {
        MoviesListP _ml;
        public TabbedP(string userId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = _ml = new MoviesListP(userId);
        }
    }
}