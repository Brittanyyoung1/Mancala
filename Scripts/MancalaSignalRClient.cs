using UnityEngine;
using TMPro;
using System;
using System.Collections;
//using Microsoft.AspNet.SignalR.Client;
using System.Collections.Generic;


using System.Net;

public class MancalaSignalRClient : MonoBehaviour
{
//    private IHubProxy _hubProxy;
//    private HubConnection _connection;
//    private readonly string _hubUrl = "https://bigprojectapi-500122975.azurewebsites.net/MancalaHub";

//    [Header("UI References")]
//    public TMP_InputField chatInput;
//    public TMP_Text chatOutput;
//    public GameObject chatPanel;

//    private string _username;
//    private bool _isConnected;
//    public bool IsConnected => _isConnected;

//    private static UnityMainThreadDispatcher _dispatcher;

//    private void Awake()
//    {
//        if (!FindObjectOfType<UnityMainThreadDispatcher>())
//        {
//            var go = new GameObject("UnityMainThreadDispatcher");
//            go.AddComponent<UnityMainThreadDispatcher>();
//            DontDestroyOnLoad(go);
//        }
//    }

//    private void Start()
//    {
//        _username = "Player";
//        InitializeSignalR();
//    }

//    private void InitializeSignalR()
//    {
//        try
//        {
//            Debug.Log("Initializing SignalR...");
//            _connection = new HubConnection(_hubUrl);
//            Debug.Log($"Hub connection created with URL: {_hubUrl}");

//            _hubProxy = _connection.CreateHubProxy("MancalaHub");
//            Debug.Log("Hub proxy created for MancalaHub");


//            _connection.StateChanged += (stateChange) =>
//            {
//                try
//                {
//                    Debug.Log($"Connection state changed from {stateChange.OldState} to {stateChange.NewState}");

//                    if (stateChange.NewState == ConnectionState.Disconnected)
//                    {
//                        Debug.LogWarning("Disconnected from SignalR hub. Attempting to reconnect...");
//                        StartCoroutine(ReconnectAfterDelay());
//                    }
//                    else if (stateChange.NewState == ConnectionState.Connected)
//                    {
//                        Debug.Log("Successfully connected to the SignalR hub.");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Debug.LogError($"Exception in StateChanged handler: {ex.Message}\nStackTrace: {ex.StackTrace}");
//                }
//            };

//            _connection.Error += (error) =>
//            {
//                try
//                {
//                    if (error != null)
//                    {
//                        Debug.LogError($"SignalR error occurred: {error.Message}\nStackTrace: {error.StackTrace}");
//                    }
//                    else
//                    {
//                        Debug.LogError("SignalR error handler invoked, but error is null.");
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Debug.LogError($"Exception in Error handler: {ex.Message}\nStackTrace: {ex.StackTrace}");
//                }
//            };


//            _connection.Closed += () =>
//            {
//                try
//                {
//                    Debug.LogWarning("Connection closed.");
//                    _isConnected = false;
//                    StartCoroutine(ReconnectAfterDelay());
//                }
//                catch (Exception ex)
//                {
//                    Debug.LogError($"Exception in Closed handler: {ex.Message}\nStackTrace: {ex.StackTrace}");
//                }
//            };

//            _hubProxy.On<string, string>("ReceiveMessage", (user, message) =>
//            {
//                UnityMainThreadDispatcher.Instance().Enqueue(() =>
//                {
//                    HandleReceivedMessage(user, message);
//                });
//            });

//            StartCoroutine(ConnectToHub());
//            Debug.Log("SignalR connection initialization completed.");
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError($"SignalR Initialization Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
//        }
//    }


//    private IEnumerator ReconnectAfterDelay()
//    {
//        yield return new WaitForSeconds(5f);
//        if (!_isConnected)
//        {
//            Debug.Log("Attempting to reconnect...");
//            StartCoroutine(ConnectToHub());
//        }
//    }


//    private IEnumerator ConnectToHub()
//    {
//        if (_connection == null)
//        {
//            Debug.LogError("Connection not initialized");
//            yield break;
//        }

//        Debug.Log("Attempting to connect to SignalR hub...");

//        _connection.Start();
//        float elapsed = 0f;
//        float timeout = 30f;

//        while (elapsed < timeout && _connection.State != ConnectionState.Connected)
//        {
//            Debug.Log($"Current connection state: {_connection.State}");
//            elapsed += Time.deltaTime;
//            yield return new WaitForSeconds(0.5f);
//        }

//        if (_connection.State == ConnectionState.Connected)
//        {
//            _isConnected = true;
//            Debug.Log("SignalR Connected Successfully");
//            yield return new WaitForSeconds(0.5f);

//            try
//            {
//                SendSystemMessage($"{_username} connected");
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError($"Error sending initial message: {ex.Message}");
//            }
//        }
//        else
//        {
//            Debug.LogError($"Connection failed. Current state: {_connection.State}");
//            StartCoroutine(ReconnectAfterDelay());
//        }
//    }



//    private void HandleReceivedMessage(string user, string message)
//    {
//        if (chatOutput != null)
//        {
//            string formattedMessage = $"{user}: {message}\n";
//            chatOutput.text += formattedMessage;

//            Canvas.ForceUpdateCanvases();
//            if (chatOutput.rectTransform.parent is RectTransform scrollViewContent)
//            {
//                scrollViewContent.anchoredPosition = new Vector2(0, float.MaxValue);
//            }
//        }
//    }

//    public void SendChatMessage(string message)
//    {
//        if (!_isConnected || string.IsNullOrEmpty(message))
//            return;

//        try
//        {
//            _hubProxy.Invoke("SendMessage", _username, message);

//            if (chatInput != null)
//            {
//                chatInput.text = string.Empty;
//                chatInput.ActivateInputField();
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.LogError($"Error sending message: {ex.Message}");
//        }
//    }

//    private void SendSystemMessage(string message)
//    {
//        if (_isConnected)
//        {
//            try
//            {
//                _hubProxy.Invoke("SendMessage", "System", message);
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError($"Error sending system message: {ex.Message}");
//            }
//        }
//    }

//    public void OnSendButtonClick()
//    {
//        if (chatInput != null && !string.IsNullOrEmpty(chatInput.text))
//        {
//            SendChatMessage(chatInput.text);
//        }
//    }

//    private void OnDestroy()
//    {
//        if (_connection != null && _isConnected)
//        {
//            try
//            {
//                SendSystemMessage($"{_username} disconnected");
//                _connection.Stop();
//                _connection.Dispose();
//            }
//            catch (Exception ex)
//            {
//                Debug.LogError($"Error during cleanup: {ex.Message}");
//            }
//        }
//    }
//}

//public class UnityMainThreadDispatcher : MonoBehaviour
//{
//    private static UnityMainThreadDispatcher _instance;
//    private readonly Queue<Action> _executionQueue = new Queue<Action>();

//    public static UnityMainThreadDispatcher Instance()
//    {
//        if (!_instance)
//        {
//            _instance = FindObjectOfType<UnityMainThreadDispatcher>();
//            if (!_instance)
//            {
//                var go = new GameObject("UnityMainThreadDispatcher");
//                _instance = go.AddComponent<UnityMainThreadDispatcher>();
//                DontDestroyOnLoad(go);
//            }
//        }
//        return _instance;
//    }

//    public void Enqueue(Action action)
//    {
//        lock (_executionQueue)
//        {
//            _executionQueue.Enqueue(action);
//        }
//    }

//    private void Update()
//    {
//        while (_executionQueue.Count > 0)
//        {
//            Action action;
//            lock (_executionQueue)
//            {
//                action = _executionQueue.Dequeue();
//            }
//            action?.Invoke();
//        }
//    }
}

//using Microsoft.AspNetCore.SignalR.Client;
//using UnityEngine;
//using TMPro;
//using System.Threading.Tasks;
//using System;

//public class MancalaSignalRClient : MonoBehaviour
//{
//    private HubConnection _hubConnection;
//    private readonly string _hubUrl = "https://bigprojectapi-500122975.azurewebsites.net/MancalaHub";

//    [Header("UI References")]
//    public TMP_InputField chatInput;
//    public TMP_Text chatOutput;

//    private string _username = "Player";
//    private bool _isConnected;

//    public bool IsConnected => _isConnected;

//    private async void Start()
//    {
//       await InitializeSignalR();
//    }

//    private async Task InitializeSignalR()
//    {
//        Debug.Log("Initializing SignalR...");

//        _hubConnection = new HubConnectionBuilder()
//            .WithUrl(_hubUrl) 
//            .WithAutomaticReconnect()
//            .Build();

//        _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
//        {
//            Debug.Log($"Message from {user}: {message}");
//            UpdateChatOutput($"{user}: {message}");
//        });

//        await ConnectToHub();
//    }

//    private async Task ConnectToHub()
//    {
//        try
//        {
//            await _hubConnection.StartAsync();
//            _isConnected = true;
//            Debug.Log("Connected to SignalR hub!");
//        }
//        catch (System.Exception ex)
//        {
//            Debug.LogError($"Error connecting to SignalR: {ex.Message}");
//            await ReconnectWithDelay();
//        }
//    }

//    private async Task ReconnectWithDelay()
//    {
//        const int reconnectDelay = 5;

//        while (!_isConnected)
//        {
//            Debug.Log($"Reconnecting to SignalR in {reconnectDelay} seconds...");
//            await Task.Delay(reconnectDelay * 1000);

//            try
//            {
//                await ConnectToHub();
//                if (_isConnected)
//                {
//                    Debug.Log("Reconnected to SignalR hub.");
//                }
//            }
//            catch (System.Exception ex)
//            {
//                Debug.LogError($"Reconnection attempt failed: {ex.Message}");
//            }
//        }
//    }

//    public async void SendChatMessage()
//    {
//        if (!_isConnected || string.IsNullOrEmpty(chatInput.text))
//        {
//            Debug.LogWarning("Cannot send message. Not connected or message is empty.");
//            return;
//        }

//        try
//        {
//            await _hubConnection.InvokeAsync("SendMessage", _username, chatInput.text);
//            chatInput.text = string.Empty;
//            Debug.Log($"Sent message: {_username}: {chatInput.text}");
//        }
//        catch (System.Exception ex)
//        {
//            Debug.LogError($"Error sending message: {ex.Message}");
//        }
//    }

//    private void UpdateChatOutput(string message)
//    {
//        if (chatOutput != null)
//        {
//            chatOutput.text += $"{message}\n";
//        }
//    }

//    private async void OnDestroy()
//    {
//        if (_hubConnection != null)
//        {
//            await _hubConnection.StopAsync();
//            await _hubConnection.DisposeAsync();
//            Debug.Log("SignalR connection stopped and disposed.");
//        }
//    }
//}
