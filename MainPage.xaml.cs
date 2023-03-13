using MauiApp1.Common;
using Microsoft.Maui.Controls;
using System.Text.Json;
using System;

namespace MauiApp1 {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void OnSearchClicked(object sender, EventArgs e) {
            string searchTerm = searchEntry.Text;
            if (!string.IsNullOrEmpty(searchTerm)) {
                var results = await Requests.IviSearch<dynamic>(searchTerm);
                resultLabel.Text = JsonSerializer.Serialize(results);
            }
        }
    }
}
