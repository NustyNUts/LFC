﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LFC.Models;
using LFC.Client;

namespace LFC
{
    public partial class Library : PhoneApplicationPage
    {
        private LFCTrack track = new LFCTrack();
        private List<LFCTrack> tracks = new List<LFCTrack>();
        private List<LFCArtist> artists = new List<LFCArtist>();
        private LFCAuth auth;
        private Client.Client client;
        public Library()
        {
            InitializeComponent();
            LibraryPanorama.SelectionChanged += Library_SelectionChanged;
        }

        private async void Library_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            switch (LibraryPanorama.SelectedIndex)
            {
                case 0: // рекомендации
                    yourRecomPB.IsIndeterminate = true;
                    artists = await client.userGetRecommendedArtists(auth.UserName);
                    yourRecomList.ItemsSource = artists;
                    yourRecomPB.IsIndeterminate = false;
                    break;
                case 1: // музыка
                    yourMusicPB.IsIndeterminate = true;
                    tracks = await client.libraryGetTracks(auth.UserName);
                    yourMusicList.ItemsSource = tracks;
                    yourMusicPB.IsIndeterminate = false;
                    break;
                case 2: // исполнители
                    artistPB.IsIndeterminate = true;
                    artists = await client.libraryGetArtists(auth.UserName);
                    artistList.ItemsSource = artists;
                    artistPB.IsIndeterminate = false;
                    break;
                case 4: // недавние
                    recentPlayLPB.IsIndeterminate = true;
                    tracks = await client.userGetRecentTracks(auth.UserName);
                    recentPlayLList.ItemsSource = tracks;
                    recentPlayLPB.IsIndeterminate = false;
                    break;
            }

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            auth = NavigationService.GetNavigationData().ElementAt(0) as LFCAuth;
            client = new Client.Client(auth);
            LibraryPanorama.SetValue(Panorama.SelectedItemProperty, LibraryPanorama.Items[0]);
        }

        private async void linkToArtistInfo_Click(object sender, RoutedEventArgs e)
        {
            var link = sender as System.Windows.Documents.Hyperlink;
            var runText = link.Inlines.ElementAt(0) as System.Windows.Documents.Run;
            var str = runText.Text;
            LFCArtist ar = new LFCArtist();
            ar =  await client.artistGetInfo(str);
            List<object> objList = new List<object>();
            objList.Add(auth);
            objList.Add(ar);
            NavigationService.Navigate(new Uri("/ArtistInfo.xaml", UriKind.Relative), objList);
                
            
        }
    }
}