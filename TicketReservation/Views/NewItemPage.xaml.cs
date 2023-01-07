using System;
using System.Collections.Generic;
using System.ComponentModel;
using TicketReservation.Models;
using TicketReservation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicketReservation.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}