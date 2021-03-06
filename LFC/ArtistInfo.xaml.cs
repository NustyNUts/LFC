﻿using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using LFC.Client;
using LFC.Models;
using System.Collections.Generic;

namespace LFC
{
    public partial class ArtistInfo : PhoneApplicationPage
    {
        private LFCArtist ar;
        private LFCAuth auth;
        private Client.Client client;
        public ArtistInfo()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }

            ar = NavigationService.GetNavigationData().ElementAt(1) as LFCArtist;
            auth = NavigationService.GetNavigationData().ElementAt(0) as LFCAuth;
            client = new Client.Client(auth); // понадобится позже

            EventInfoGrid.ItemsSource = new List<LFCArtist>() {ar};
        }
    }
}