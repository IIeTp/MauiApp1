using Microsoft.Maui.Controls;
using MauiApp1.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MauiApp1 {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void OnSearchClicked(object sender, System.EventArgs e) {
            string searchTerm = searchEntry.Text;
            if (!string.IsNullOrEmpty(searchTerm)) {
                var results = await Requests.IviSearch<List<SearchResult>>(searchTerm);
                foreach (var result in results) {
                    var button = new Button {
                        Text = result.Title
                    };
                    button.Clicked += async (s, a) => {
                        var episodes = await Requests.IviTvshow<dynamic>(result.Id.ToString());
                        var episode = episodes.FirstOrDefault()?.FirstOrDefault()?.FirstOrDefault();
                        if (episode != null) {
                            var episodeResult = await Requests.IviEpisode<dynamic>(episode.Id);
                            resultLabel.Text = JsonSerializer.Serialize(episodeResult);
                        }
                    };
                    stackLayout.Children.Add(button);
                }
            }
        }
    }
}
