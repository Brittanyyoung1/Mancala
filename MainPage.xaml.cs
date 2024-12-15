using SS.Mancala.BL.Models;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Net.Http.Json;

namespace SS.Mancala.MAUI
{
    public partial class MainPage : ContentPage
    {
        //private readonly string apiBaseAddress = "http://localhost:5052/api";
        private const string apiBaseAddress = "https://bigprojectapi-500122975.azurewebsites.net/api";

        public ObservableCollection<Game> Games { get; } = new ObservableCollection<Game>();
        private readonly HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            _httpClient = new HttpClient();
            LoadGames();
        }


        private async void OnStartNewGameClicked(object sender, EventArgs e)
        {
            var player1Id = Guid.NewGuid();
            var player2Id = Guid.NewGuid();
            var response = await StartNewGame(player1Id, player2Id);
            if (response != null)
            {
                await Navigation.PushAsync(new GamePage(response.Id, player1Id, player2Id, _httpClient));
                LoadGames();
            }
            else
            {
                await DisplayAlert("Error", "Unable to start new game.", "OK");
            }
        }


        private async void LoadGames()
        {
            try
            {
                var games = await _httpClient.GetFromJsonAsync<Game[]>($"{apiBaseAddress}/game");
                Games.Clear();
                if (games != null && games.Length > 0)
                {
                    var lastGame = games[^1]; 
                    lastGame.DisplayName = "Last Game"; 
                    Games.Add(lastGame);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading games: {ex.Message}");
            }
        }


        private async Task<Game> StartNewGame(Guid player1Id, Guid player2Id)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{apiBaseAddress}/game/start", new { player1Id, player2Id });
                if (response.IsSuccessStatusCode)
                {
                    var game = await response.Content.ReadFromJsonAsync<Game>();
                    return game;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting game: {ex.Message}");
                return null;
            }
        }


        private async void OnLoadGameClicked(object sender, EventArgs e)
        {
            if (Games.Count > 0)
            {
               
                var lastGame = Games[^1]; 


                await Navigation.PushAsync(new GamePage(lastGame.Id, lastGame.Player1Id, lastGame.Player2Id, _httpClient));
            }
            else
            {
            
                await DisplayAlert("Error", "No games available to load.", "OK");
            }
        }


    }
}