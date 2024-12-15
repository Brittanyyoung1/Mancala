using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;





using System;
using UnityEngine.UI;




[Serializable]
public class Game
{
    public string displayName;
    public string id;
    public string player1Id;
    public string player2Id;
    public bool isGameOver;
    public string status;
    public string currentTurn;
    public string dateCreated;
    public List<Pit> pits;
    public List<Move> moves;
}

[Serializable]
public class Pit
{
    public string id;
    public string gameId;
    public string playerId;
    public int stones;
    public bool isMancala;
    public int pitPosition;
}

[Serializable]
public class Move
{
    public string id;
    public string gameId;
    public string playerId;
    public int sourcePit;
    public int stonesMoved;
    public string timeStamp;
    public bool isExtraTurn;
    public int moveNo;
}



[System.Serializable]
public class GameResponse
{
    public Game game;
}

public class MancalaGameManager : MonoBehaviour
{
    private readonly string API_BASE = "https://bigprojectapi-500122975.azurewebsites.net/api/Game";


    [Header("Stone Settings")]
    public Vector3 stoneScale = new Vector3(0.1f, 0.1f, 0.1f);


    [Header("UI Elements")]
    public TMP_Text statusLabel;
    public TMP_Text gameStateLabel;
    public TMP_Text player1ScoreLabel;
    public TMP_Text computerScoreLabel;

    [Header("Board Elements")]
    public GameObject stonePrefab;
    public Material defaultPitMaterial;
    public Material highlightPitMaterial;
    public Material disabledPitMaterial;
    public List<GameObject> pits;

    [Header("Stone Materials")]
    [SerializeField]
    public List<Material> stoneMaterials;

    [Header("Chat UI Elements")]
    [SerializeField]
    private TMP_InputField chatInput;
    [SerializeField]
    private TMP_Text chatOutput;
    [SerializeField]
    private GameObject chatPanel;
    [SerializeField]
    private Button sendButton;
    [SerializeField]
    private ScrollRect chatScrollRect;

    [Header("Pit Counter Settings")]
    public GameObject pitCounterPrefab;
    public Vector3 counterOffset = new Vector3(0f, 1.5f, 0f);
    public float counterTextSize = 3f;
    private List<PitCounter> pitCounters = new List<PitCounter>();


    private string _gameId;
    private Game _currentGame;
    private MancalaSignalRClient _signalRClient;


    public Game CurrentGame => _currentGame;

    public Dictionary<GameObject, List<GameObject>> pitstones = new Dictionary<GameObject, List<GameObject>>();

    void Start()
    {
        SetupPitCounters();
        if (chatInput != null && chatOutput != null && chatPanel != null)
        {
       //     InitializeChat();
        }
        StartCoroutine(StartNewGame());
    }
    //private void InitializeChat()
    //{

    //    if (_signalRClient == null)
    //    {
    //        _signalRClient = gameObject.AddComponent<MancalaSignalRClient>();
    //    }


    //    if (chatInput != null)
    //    {
    //        _signalRClient.chatInput = chatInput;

    //        chatInput.onEndEdit.AddListener((message) => {
    //            if (!string.IsNullOrEmpty(message) && Input.GetKeyDown(KeyCode.Return))
    //            {
    //                _signalRClient.OnSendButtonClick();
    //            }
    //        });
    //    }

    //    if (chatOutput != null)
    //    {
    //        _signalRClient.chatOutput = chatOutput;
    //    }

    //    if (chatPanel != null)
    //    {
    //        _signalRClient.chatPanel = chatPanel;
    //    }

    //    if (sendButton != null)
    //    {

    //        sendButton.onClick.RemoveAllListeners();
    //        sendButton.onClick.AddListener(() => _signalRClient.OnSendButtonClick());
    //    }


    //    if (chatScrollRect != null && chatOutput != null)
    //    {
    //        chatOutput.text = "Chat initialized...\n";
    //        Canvas.ForceUpdateCanvases();
    //        chatScrollRect.verticalNormalizedPosition = 0f;
    //    }
    //}
    //public void ToggleChat()
    //{
    //    if (chatPanel != null)
    //    {
    //        chatPanel.SetActive(!chatPanel.activeSelf);
    //    }
    //}
    //private void SendGameMessage(string message)
    //{
    //    if (_signalRClient != null)
    //    {
    //        try
    //        {
    //            _signalRClient.SendChatMessage(message);
    //        }
    //        catch (Exception ex)
    //        {
    //            Debug.LogError($"Failed to send game message: {ex.Message}");
    //        }
    //    }
    //}

    //private void OnDisable()
    //{
    //    if (_signalRClient != null)
    //    {
    //        _signalRClient.SendChatMessage("Player disconnected");
    //    }
    //}
    //private void OnDestroy()
    //{
    //    if (chatInput != null)
    //    {
    //        chatInput.onEndEdit.RemoveAllListeners();
    //    }

    //    if (sendButton != null)
    //    {
    //        sendButton.onClick.RemoveAllListeners();
    //    }
    //}

    private void SetupPitCounters()
    {

        foreach (var counter in pitCounters)
        {
            if (counter != null)
            {
                Destroy(counter.gameObject);
            }
        }
        pitCounters.Clear();


        foreach (var pit in pits)
        {
            GameObject counterObj = Instantiate(pitCounterPrefab, pit.transform.position + counterOffset, Quaternion.identity);
            counterObj.transform.SetParent(pit.transform);


            counterObj.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

            PitCounter counter = counterObj.GetComponent<PitCounter>();
            if (counter != null)
            {
                counter.counterText.fontSize = counterTextSize;
                pitCounters.Add(counter);
            }
        }
    }



    private IEnumerator StartNewGame()
    {
        Debug.Log("Starting a new game...");
        UnityWebRequest request = UnityWebRequest.PostWwwForm($"{API_BASE}/start", "");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log($"Game started: {response}");

            if (!HandleStartGameResponse(response))
            {
                statusLabel.text = "Error starting game!";
                yield break;
            }

            yield return RefreshGameState();
        }
        else
        {
            Debug.LogError($"Failed to start game: {request.error}");
            statusLabel.text = "Failed to start game!";
        }
    }

    private bool HandleStartGameResponse(string response)
    {
        try
        {

            _currentGame = JsonUtility.FromJson<Game>(response);

            if (_currentGame == null || string.IsNullOrEmpty(_currentGame.id))
            {
                throw new Exception("Failed to parse game data or game ID is null.");
            }

            _gameId = _currentGame.id;
            Debug.Log($"Game ID set: {_gameId}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error parsing game data: {ex.Message}");
            return false;
        }
    }


    public IEnumerator RefreshGameState()
    {
        if (string.IsNullOrEmpty(_gameId))
        {
            Debug.LogError("Game ID is missing. Cannot refresh game state.");
            statusLabel.text = "Game ID is missing!";
            yield break;
        }

        Debug.Log($"Refreshing game state for Game ID: {_gameId}...");
        UnityWebRequest request = UnityWebRequest.Get($"{API_BASE}/{_gameId}/full");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            Debug.Log($"Game state fetched: {response}");

            if (HandleGameStateResponse(response))
            {
                if (_currentGame.currentTurn == _currentGame.player2Id && !_currentGame.isGameOver)
                {
                    Debug.Log("Computer's turn detected after refresh.");
                    StartCoroutine(HandleComputerTurn());
                }
            }
            else
            {
                statusLabel.text = "Failed to parse game state!";
            }
        }
        else
        {
            Debug.LogError($"Failed to fetch game state: {request.error}");
            statusLabel.text = "Failed to fetch game state!";
        }
    }


    private bool HandleGameStateResponse(string response)
    {
        try
        {

            _currentGame = JsonUtility.FromJson<Game>(response);

            if (_currentGame == null || _currentGame.pits == null)
            {
                throw new Exception("Invalid game state response.");
            }

            UpdateGameLabels();
            UpdateBoard();
            StartCoroutine(CheckAndHandleGameOver());
            return true;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error parsing game state: {ex.Message}");
            return false;
        }
    }


    private void UpdateBoard()
    {
        Debug.Log("Updating board visuals...");

        if (_currentGame.pits.Count != pits.Count)
        {
            Debug.LogError($"Mismatch between pits in game data ({_currentGame.pits.Count}) and Unity objects ({pits.Count}).");
            return;
        }

        for (int i = 0; i < pits.Count; i++)
        {
            var pitObject = pits[i];
            var pitData = _currentGame.pits[i];

            Debug.Log($"Pit {i} - IsMancala: {pitData.isMancala}, Stones: {pitData.stones}, PlayerId: {pitData.playerId}");

            UpdatePitVisuals(pitObject, pitData);
            UpdatePitStones(pitObject, pitData.stones);
        }


        if (_currentGame.currentTurn == _currentGame.player2Id && !_currentGame.isGameOver)
        {
            Debug.Log("Computer's turn initiated from UpdateBoard.");
            StartCoroutine(HandleComputerTurn());
        }
    }


    private void UpdatePitVisuals(GameObject pit, Pit pitData)
    {
        var renderer = pit.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.material = defaultPitMaterial;
        }


        int pitIndex = pits.IndexOf(pit);
        if (pitIndex >= 0 && pitIndex < pitCounters.Count)
        {
            pitCounters[pitIndex].UpdateCount(pitData.stones);
        }
    }


    private void UpdatePitStones(GameObject pit, int count)
    {

        foreach (Transform child in pit.transform)
        {
            Destroy(child.gameObject);
        }
        int pitIndex = pits.IndexOf(pit);
        bool isMancala = _currentGame.pits[pitIndex].isMancala;


        if (!isMancala)
        {
            GameObject textObj = new GameObject("StoneCounter");
            textObj.transform.SetParent(pit.transform);
            textObj.transform.localPosition = new Vector3(0f, 0.75f, 0f);
            textObj.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

            TextMeshPro counterText = textObj.AddComponent<TextMeshPro>();
            counterText.text = count.ToString();
            counterText.fontSize = 4;
            counterText.alignment = TextAlignmentOptions.Center;
            counterText.color = Color.white;
        }

        if (stoneMaterials == null || stoneMaterials.Count == 0)
        {
            Debug.LogWarning("No stone materials available to assign.");
            return;
        }

        float baseRadius = 0.2f;
        float baseHeight = 0.05f;
        Vector3 pitCenter = pit.transform.position;

        if (count <= 4)
        {
            float radius = baseRadius * 0.8f;
            for (int i = 0; i < count; i++)
            {
                float angle = (360f / count) * i;
                float radians = angle * Mathf.Deg2Rad;
                Vector3 offset = new Vector3(
                    Mathf.Cos(radians) * radius,
                    baseHeight,
                    Mathf.Sin(radians) * radius
                );
                CreateStone(pit, pitCenter + offset);
            }
        }
        else if (count <= 8)
        {
            int innerCount = count / 2;
            int outerCount = count - innerCount;

            float innerRadius = baseRadius * 0.5f;
            for (int i = 0; i < innerCount; i++)
            {
                float angle = (360f / innerCount) * i + (180f / innerCount);
                float radians = angle * Mathf.Deg2Rad;
                Vector3 offset = new Vector3(
                    Mathf.Cos(radians) * innerRadius,
                    baseHeight,
                    Mathf.Sin(radians) * innerRadius
                );
                CreateStone(pit, pitCenter + offset);
            }

            float outerRadius = baseRadius * 0.9f;
            for (int i = 0; i < outerCount; i++)
            {
                float angle = (360f / outerCount) * i;
                float radians = angle * Mathf.Deg2Rad;
                Vector3 offset = new Vector3(
                    Mathf.Cos(radians) * outerRadius,
                    baseHeight,
                    Mathf.Sin(radians) * outerRadius
                );
                CreateStone(pit, pitCenter + offset);
            }
        }
        else
        {
            int ringCount = 3;
            int stonesPerRing = count / ringCount;
            int remainingStones = count % ringCount;

            for (int ring = 0; ring < ringCount; ring++)
            {
                int stonesToPlace = stonesPerRing + (ring < remainingStones ? 1 : 0);
                float ringRadius = baseRadius * (0.45f + (ring * 0.32f));

                for (int i = 0; i < stonesToPlace; i++)
                {
                    float angle = (360f / stonesToPlace) * i + (ring * 30f);
                    float radians = angle * Mathf.Deg2Rad;
                    Vector3 offset = new Vector3(
                        Mathf.Cos(radians) * ringRadius,
                        baseHeight + (ring * 0.03f),
                        Mathf.Sin(radians) * ringRadius
                    );
                    CreateStone(pit, pitCenter + offset);
                }
            }
        }
    }

    private void CreateStone(GameObject pit, Vector3 position)
    {
        GameObject stone = Instantiate(stonePrefab, position, Quaternion.identity, pit.transform);
        stone.transform.localScale = stoneScale;

        var renderer = stone.GetComponent<Renderer>();
        if (renderer != null && stoneMaterials.Count > 0)
        {
            renderer.material = stoneMaterials[UnityEngine.Random.Range(0, stoneMaterials.Count)];
        }
    }






    private void UpdateGameLabels()
    {
        if (_currentGame == null) return;

        int player1Mancala = _currentGame.pits.First(p => p.isMancala && p.playerId == _currentGame.player1Id).stones;
        int player2Mancala = _currentGame.pits.First(p => p.isMancala && p.playerId == _currentGame.player2Id).stones;

        player1ScoreLabel.text = $"Player Score: {player1Mancala}";
        computerScoreLabel.text = $"Computer Score: {player2Mancala}";

        gameStateLabel.text = _currentGame.isGameOver
            ? "Game Over"
            : (_currentGame.currentTurn == _currentGame.player1Id ? "Your Turn" : "Computer's Turn");

        statusLabel.text = _currentGame.isGameOver ? "Game Over" : "Game in Progress";
    }

    private IEnumerator CheckAndHandleGameOver()
    {
        if (_currentGame.isGameOver)
        {
            int player1Score = _currentGame.pits.First(p => p.isMancala && p.playerId == _currentGame.player1Id).stones;
            int player2Score = _currentGame.pits.First(p => p.isMancala && p.playerId == _currentGame.player2Id).stones;

            string winner = player1Score > player2Score
                ? "You Win!"
                : player2Score > player1Score
                ? "Computer Wins!"
                : "It's a Tie!";

            statusLabel.text = $"Game Over! {winner}";
         //   SendGameMessage($"Game Over! {winner}");
            yield return null;
        }
    }



    private IEnumerator HandleComputerTurn()
    {
        if (_currentGame.currentTurn == _currentGame.player2Id)
        {
            Debug.Log("Computer's turn...");


            statusLabel.text = "Computer's Turn...";
            gameStateLabel.text = "Computer's Turn";
            yield return new WaitForSeconds(2.0f); // Show "Computer's Turn" for 2 seconds

            while (_currentGame.currentTurn == _currentGame.player2Id && !_currentGame.isGameOver)
            {
                UnityWebRequest request = UnityWebRequest.PostWwwForm(
                    $"{API_BASE}/{_gameId}/computer-play",
                    ""
                );
                request.SetRequestHeader("Content-Type", "application/json");


                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Computer move executed successfully.");


                    yield return RefreshGameState();
                  //  SendGameMessage("Computer made a move!");
                    if (_currentGame.isGameOver)
                    {
                        Debug.Log("Game is over. Checking winner...");
                        yield return CheckAndHandleGameOver();
                    }
                    else if (_currentGame.currentTurn == _currentGame.player1Id)
                    {
                        Debug.Log("Turn switched back to Player 1.");
                        statusLabel.text = "Game in Progress";
                        gameStateLabel.text = "Your Turn";
                        break;
                    }
                    else
                    {
                        Debug.Log("Computer gets an extra turn.");
                        yield return new WaitForSeconds(0.7f);
                    }
                }
                else
                {
                    Debug.LogError($"Failed to execute computer move: {request.error}");
                    break;
                }
            }
        }
    }
    private IEnumerator DistributeStonesAcrossPits(int startPitIndex, int stonesToDistribute)
    {
        int currentPitIndex = startPitIndex;

        for (int i = 0; i < stonesToDistribute; i++)
        {

            currentPitIndex = (currentPitIndex + 1) % pits.Count;
            GameObject currentPit = pits[currentPitIndex];


            GameObject stone = Instantiate(stonePrefab);
            stone.transform.SetParent(null);
            stone.transform.position = pits[startPitIndex].transform.position;
            stone.transform.localScale = stoneScale;

            var renderer = stone.GetComponent<Renderer>();
            if (renderer != null && stoneMaterials.Count > 0)
            {
                renderer.material = stoneMaterials[UnityEngine.Random.Range(0, stoneMaterials.Count)];
            }


            Vector3 targetPosition = currentPit.transform.position + new Vector3(
                UnityEngine.Random.Range(-0.05f, 0.05f),
                0.05f,
                UnityEngine.Random.Range(-0.05f, 0.05f)
            );

            // Move the stone
            yield return MoveStone(stone, targetPosition, 0.8f);


            stone.transform.SetParent(currentPit.transform);
            yield return null;
            stone.transform.localScale = stoneScale;
        }
    }

    private IEnumerator MoveStone(GameObject stone, Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        Vector3 startPosition = stone.transform.position;

        float distance = Vector3.Distance(startPosition, targetPosition);
        float arcHeight = distance * 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = elapsed / duration;


            Vector3 currentPosition = Vector3.Lerp(startPosition, targetPosition, normalizedTime);


            float arcOffset = Mathf.Sin(normalizedTime * Mathf.PI) * arcHeight;
            currentPosition.y += arcOffset;

            stone.transform.position = currentPosition;
            stone.transform.localScale = stoneScale;

            yield return null;
        }

        stone.transform.position = targetPosition;
        stone.transform.localScale = stoneScale;
    }

    public IEnumerator MakeMove(int pitPosition)
    {
        Debug.Log($"Making move at pit position: {pitPosition}");


        int stones = _currentGame.pits[pitPosition].stones;

        if (stones <= 0)
        {
            Debug.LogError("No stones to move!");
            yield break;
        }


        _currentGame.pits[pitPosition].stones = 0;


        yield return DistributeStonesAcrossPits(pitPosition, stones);


        yield return SendMoveToServer(pitPosition);
     //   SendGameMessage($"Player moved from pit {pitPosition}");


        yield return RefreshGameState();

    }
    private IEnumerator SendMoveToServer(int pitPosition)
    {
        UnityWebRequest request = UnityWebRequest.PostWwwForm($"{API_BASE}/{_gameId}/move/{pitPosition}", "");
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Move successfully sent to server.");
        }
        else
        {
            Debug.LogError($"Failed to send move: {request.error}");
            statusLabel.text = "Move Failed!";
        }
    }





    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var pitIndex = pits.IndexOf(hit.collider.gameObject);
            if (pitIndex != -1 && _currentGame.pits[pitIndex].playerId == _currentGame.currentTurn)
            {
                StartCoroutine(MakeMove(pitIndex));
            }
        }
    }
}
