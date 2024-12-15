using Microsoft.AspNetCore.SignalR.Client;
using SS.Mancala.BL.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace SS.Mancala.MAUI
{
    public partial class GamePage : ContentPage
    {
        private readonly HttpClient _httpClient;
        private readonly Guid _gameId;
        private Game _currentGame;
        //   private const string API_BASE = "http://localhost:5052/api/Game";
        private const string API_BASE = "https://bigprojectapi-500122975.azurewebsites.net/api/Game";

        private HubConnection _hubConnection;
        private ObservableCollection<string> _chatMessages;


        public GamePage(Guid gameId, Guid player1Id, Guid player2Id, HttpClient httpClient)
        {
            InitializeComponent();
            _chatMessages = new ObservableCollection<string>();
            ChatMessagesList.ItemsSource = _chatMessages;

            InitializeSignalR();
            _gameId = gameId;
            _httpClient = httpClient;
            _currentGame = new Game
            {
                Id = gameId,
                Player1Id = player1Id,
                Player2Id = player2Id,
                CurrentTurn = player1Id,
                Pits = new List<Pit>()
            };
           // GameIdLabel.Text = "Loading...";
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeGameState();
        }


        private void InitializeSignalR()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://bigprojectapi-500122975.azurewebsites.net/MancalaHub") 
                .WithAutomaticReconnect()
                .Build();

            // Event: ReceiveMessage
            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _chatMessages.Add($"{user}: {message}");
                });
            });

            // Event: GameStarted
            _hubConnection.On<string>("GameStarted", (gameId) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Console.WriteLine($"Game started with ID: {gameId}");
                });
            });

            // Event: MoveMade
            _hubConnection.On<string, int>("MoveMade", (user, pitPosition) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Console.WriteLine($"{user} made a move on pit {pitPosition}");
                });
            });

            ConnectToHub();
        }
        
        private async void ConnectToHub()
        {
            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("SignalR connected.");

                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    Console.WriteLine("Connection State: Connected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalR connection failed: {ex.Message}");
            }
        }
        private async void SendChatMessage(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ChatInput.Text))
            {
                try
                {
                    await _hubConnection.InvokeAsync("SendMessage", "Player", ChatInput.Text);
                    ChatInput.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send message: {ex.Message}");
                }
            }
        }


        private async void DisconnectHub()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                Console.WriteLine("SignalR disconnected.");
            }
        }


        private async Task InitializeGameState()
        {
            try
            {
                Console.WriteLine($"Initializing game state for game {_gameId}...");
                var response = await _httpClient.GetFromJsonAsync<Game>($"{API_BASE}/{_gameId}/full");

                if (response != null)
                {
                    Console.WriteLine($"Received game data: Player1={response.Player1Id}, Player2={response.Player2Id}, Pits={response.Pits?.Count ?? 0}");
                    _currentGame = response;
                    DebugPitOwnership();

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        UpdateGameLabels();
                        if (_currentGame.Pits?.Count > 0)
                        {
                            UpdateBoard();
                        }
                    });
                }
                else
                {
                    await DisplayAlert("Error", "Could not load game data", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing game state: {ex}");
                await DisplayAlert("Error", $"Failed to initialize game: {ex.Message}", "OK");
            }
        }

        private void UpdateGameLabels()
        {
         //   GameIdLabel.Text = $"Game: {_gameId}";
         //   Player1IdLabel.Text = $"Player 1: {_currentGame.Player1Id}";
         //   Player2IdLabel.Text = $"Player 2: {_currentGame.Player2Id}";
           // PitCountLabel.Text = $"Pits: {_currentGame.Pits?.Count ?? 0}";
        }
        private void UpdateBoard()
        {
            if (_currentGame?.Pits == null || _currentGame.Pits.Count < 14)
            {
                Console.WriteLine($"Invalid pits data. Count: {_currentGame?.Pits?.Count ?? 0}");
                return;
            }

            try
            {
                Console.WriteLine("Beginning board update...");

                // Clear and rebuild all UI elements
           //     PitListDisplay.Children.Clear();
                GameBoardGrid.Children.Clear();

                RenderBoard();

                // Force update status
                CurrentTurnLabel.Text = _currentGame.CurrentTurn == _currentGame.Player1Id
                    ? "Your Turn"
                    : "Computer's Turn";
                GameStatusLabel.Text = _currentGame.IsGameOver
                    ? "Game Over!"
                    : "Game in Progress";

                Console.WriteLine("Board update completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateBoard: {ex}");
            }
        }




        private void RenderBoard()
        {
            ClearBoard();
            SetupGridDefinitions();
            RenderPlayerLabels();
            RenderPits();
            UpdatePitList();
        }

        private void ClearBoard()
        {
          //  PitListDisplay.Children.Clear();
            GameBoardGrid.Children.Clear();
            GameBoardGrid.RowDefinitions.Clear();
            GameBoardGrid.ColumnDefinitions.Clear();
        }

        private void SetupGridDefinitions()
        {
            // Rows: Player2 Label, Player2 Pits, Player1 Pits, Player1 Label
            GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });
            GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            GameBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30) });

            // Columns: Mancalas on ends, pits in between
            for (int i = 0; i < 8; i++)
            {
                GameBoardGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = i == 0 || i == 7 ? new GridLength(80) : new GridLength(120)
                });
            }
        }

        private void RenderPlayerLabels()
        {
            var player2Label = CreatePlayerLabel("Computer (Player 2)", Colors.DarkRed);
            Grid.SetRow(player2Label, 0);
            Grid.SetColumn(player2Label, 1);
            Grid.SetColumnSpan(player2Label, 6);
            GameBoardGrid.Children.Add(player2Label);

            var player1Label = CreatePlayerLabel("Player 1", Colors.DarkBlue);
            Grid.SetRow(player1Label, 3);
            Grid.SetColumn(player1Label, 1);
            Grid.SetColumnSpan(player1Label, 6);
            GameBoardGrid.Children.Add(player1Label);
        }

        private Label CreatePlayerLabel(string text, Color color)
        {
            return new Label
            {
                Text = text,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = color,
                FontAttributes = FontAttributes.Bold
            };
        }

        private void RenderPits()
        {
            
            for (int i = 12; i >= 7; i--)
            {
                var pit = _currentGame.Pits.First(p => p.PitPosition == i);
                var button = CreatePitButton(pit);
                Grid.SetRow(button, 1);
                Grid.SetColumn(button, 12 - i + 1); 
                GameBoardGrid.Children.Add(button);
            }

   
            for (int i = 0; i < 6; i++)
            {
                var pit = _currentGame.Pits.First(p => p.PitPosition == i);
                var button = CreatePitButton(pit);
                Grid.SetRow(button, 2);
                Grid.SetColumn(button, i + 1);
                GameBoardGrid.Children.Add(button);
            }

            // Add Mancalas
            RenderMancalas();
        }


        private void RenderMancalas()
        {
            var p1Mancala = CreatePitButton(_currentGame.Pits.First(p => p.PitPosition == 6), true);
            Grid.SetRow(p1Mancala, 1);
            Grid.SetRowSpan(p1Mancala, 2);
            Grid.SetColumn(p1Mancala, 7);
            GameBoardGrid.Children.Add(p1Mancala);

            var p2Mancala = CreatePitButton(_currentGame.Pits.First(p => p.PitPosition == 13), true);
            Grid.SetRow(p2Mancala, 1);
            Grid.SetRowSpan(p2Mancala, 2);
            Grid.SetColumn(p2Mancala, 0);
            GameBoardGrid.Children.Add(p2Mancala);
        }

        private Button CreatePitButton(Pit pit, bool isMancala = false)
        {
            var button = new Button
            {
                Text = $"{pit.Stones}\nPit {pit.PitPosition}",
                BackgroundColor = GetPitColor(pit),
                TextColor = Colors.Black,
                BorderColor = Colors.SaddleBrown,
                BorderWidth = 2,
                CornerRadius = isMancala ? 10 : 25,
                Margin = new Thickness(2),
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                LineBreakMode = LineBreakMode.WordWrap
            };

            button.IsEnabled = IsPitPlayable(pit);
            if (button.IsEnabled)
            {
                button.Clicked += async (s, e) => await HandlePitClick(pit.PitPosition);
            }

            return button;
        }

        private Color GetPitColor(Pit pit)
        {
            if (pit.IsMancala)
                return Colors.SandyBrown;
            if (pit.PlayerId == _currentGame.Player1Id && _currentGame.CurrentTurn == _currentGame.Player1Id && pit.Stones > 0)
                return Colors.LightGreen;
            return Colors.Bisque;
        }

        private bool IsPitPlayable(Pit pit)
        {
            return !pit.IsMancala &&
                   pit.Stones > 0 &&
                   pit.PlayerId == _currentGame.Player1Id &&
                   _currentGame.CurrentTurn == _currentGame.Player1Id &&
                   !_currentGame.IsGameOver;
        }

        private void UpdatePitList()
        {
            //foreach (var pit in _currentGame.Pits.OrderBy(p => p.PitPosition))
            //{
            //    PitListDisplay.Children.Add(new Label
            //    {
            //        Text = $"Pit {pit.PitPosition}: Stones={pit.Stones}, PlayerId={pit.PlayerId}, IsMancala={pit.IsMancala}",
            //        FontSize = 12,
            //        TextColor = Colors.Black
            //    });
            //}
        }

        private void DebugPitOwnership()
        {
            Console.WriteLine("\n=== Pit Ownership Debug ===");
            Console.WriteLine($"Player 1 ID: {_currentGame.Player1Id}");
            Console.WriteLine($"Player 2 ID: {_currentGame.Player2Id}");
            Console.WriteLine($"Current Turn: {_currentGame.CurrentTurn}");

            foreach (var pit in _currentGame.Pits.OrderBy(p => p.PitPosition))
            {
                Console.WriteLine($"Pit {pit.PitPosition}: Owner={pit.PlayerId}, Stones={pit.Stones}, IsMancala={pit.IsMancala}");
            }
            Console.WriteLine("==========================\n");
        }

        private async Task HandlePitClick(int pitPosition)
        {
            try
            {
                Console.WriteLine($"Making move for pit {pitPosition}");
                var response = await _httpClient.PostAsync(
                    $"{API_BASE}/{_gameId}/move/{pitPosition}",
                    new StringContent("", System.Text.Encoding.UTF8, "application/json")
                );

                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Move response: {response.StatusCode}");
                Console.WriteLine($"Response content: {content}");

                if (response.IsSuccessStatusCode)
                {

                    await RefreshGameState();

                    if (_currentGame.CurrentTurn == _currentGame.Player2Id)
                    {
                        await Task.Delay(500);
                        await HandleComputerTurn();
                    }
                }
                else
                {
                    await DisplayAlert("Invalid Move", content, "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in HandlePitClick: {ex}");
                await DisplayAlert("Error", "Failed to make move", "OK");
            }
        }


        private async Task RefreshGameState()
        {
            try
            {
                Console.WriteLine("Refreshing game state...");
                var gameResponse = await _httpClient.GetFromJsonAsync<Game>($"{API_BASE}/{_gameId}/full");
                if (gameResponse != null)
                {
                    _currentGame = gameResponse;  
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        UpdateGameLabels();
                        UpdateBoard();
                        CurrentTurnLabel.Text = _currentGame.CurrentTurn == _currentGame.Player1Id
                            ? "Your Turn"
                            : "Computer's Turn";
                    });
                    Console.WriteLine($"Game state refreshed:");
                    Console.WriteLine($"Current Turn: {_currentGame.CurrentTurn}");
                    foreach (var pit in _currentGame.Pits)
                    {
                        Console.WriteLine($"Pit {pit.PitPosition}: Stones={pit.Stones}");
                    }
                }
                await CheckAndHandleGameOver();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error refreshing game state: {ex.Message}");
            }
        }



        private async Task HandleComputerTurn()
        {
            if (_currentGame.CurrentTurn == _currentGame.Player2Id)
            {
                await Task.Delay(100);
                while (_currentGame.CurrentTurn == _currentGame.Player2Id && !_currentGame.IsGameOver)
                {
                    try
                    {
                        var computerResponse = await _httpClient.PostAsync(
                            $"{API_BASE}/{_gameId}/computer-play",
                            new StringContent("", System.Text.Encoding.UTF8, "application/json")
                        );
                        if (computerResponse.IsSuccessStatusCode)
                        {
                            await RefreshGameState();
                            await CheckAndHandleGameOver();
                            await Task.Delay(100);
                        }
                        else
                        {
                            var error = await computerResponse.Content.ReadAsStringAsync();
                            Console.WriteLine($"Computer move failed: {error}");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during computer turn: {ex.Message}");
                        break;
                    }
                }
            }
        }

        private async Task CheckAndHandleGameOver()
        {
            bool noStonesInRegularPits = _currentGame.Pits
                .Where(p => !p.IsMancala)
                .All(p => p.Stones == 0);

            if (noStonesInRegularPits)
            {
                try
                {
                    int player1Score = _currentGame.Pits[6].Stones; // Player 1's Mancala
                    int player2Score = _currentGame.Pits[13].Stones; // Player 2's Mancala
                    string winner;

                    if (player1Score > player2Score)
                        winner = "Player 1 Wins!";
                    else if (player2Score > player1Score)
                        winner = "Computer Wins!";
                    else
                        winner = "It's a Tie!";

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        CurrentTurnLabel.Text = winner;
                        GameStatusLabel.Text = $"Game Over! Final Score - Player: {player1Score}, Computer: {player2Score}";
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error checking winner: {ex.Message}");
                }
            }
        }
    }
    }
    
