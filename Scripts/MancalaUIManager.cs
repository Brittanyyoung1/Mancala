//using UnityEngine;
//using TMPro;

//public class MancalaUIManager : MonoBehaviour
//{
//    [Header("3D Text Labels")]
//    public TextMeshPro currentTurnLabel;   
//    public TextMeshPro gameStatusLabel;   
//    public TextMeshPro player1ScoreLabel;   
//    public TextMeshPro computerScoreLabel;
//    private MancalaGameManager gameManager;


//    void Start()
//    {
//        gameManager = FindObjectOfType<MancalaGameManager>();
//        if (gameManager == null)
//        {
//            Debug.LogError("Could not find MancalaGameManager");
//            return;
//        }

      
//        if (currentTurnLabel) currentTurnLabel.text = "Waiting for game...";
//        if (gameStatusLabel) gameStatusLabel.text = "Game starting...";
//        if (player1ScoreLabel) player1ScoreLabel.text = "Player: 0";
//        if (computerScoreLabel) computerScoreLabel.text = "Computer: 0";

     
//        SetupAllTextStyles();
//    }

//    private void SetupAllTextStyles()
//    {
//        TextMeshPro[] allTexts = { currentTurnLabel, gameStatusLabel,
//                                  player1ScoreLabel, computerScoreLabel };
//        foreach (var text in allTexts)
//        {
//            if (text != null) SetupTextStyle(text);
//        }
//    }

//    private void SetupTextStyle(TextMeshPro text)
//    {
//        text.fontSize = 8;                   
//        text.alignment = TextAlignmentOptions.Center;
//        text.color = Color.white;



//        text.outlineWidth = 0.2f;
//        text.outlineColor = Color.black;
//    }

//    public void UpdateUI(Game gameState)
//    {
//        if (gameState == null) return;

//        if (currentTurnLabel)
//        {
//            currentTurnLabel.text = gameState.CurrentTurn == gameState.Player1Id
//                ? "Your Turn"
//                : "Computer's Turn";
//        }


//        if (player1ScoreLabel && gameState.Pits != null && gameState.Pits.Count > 6)
//        {
//            player1ScoreLabel.text = $"Player: {gameState.Pits[6].Stones}";
//        }

//        if (computerScoreLabel && gameState.Pits != null && gameState.Pits.Count > 13)
//        {
//            computerScoreLabel.text = $"Computer: {gameState.Pits[13].Stones}";
//        }

  
//        if (gameStatusLabel)
//        {
//            if (gameState.IsGameOver)
//            {
//                int player1Score = gameState.Pits[6].Stones;
//                int computerScore = gameState.Pits[13].Stones;

//                if (player1Score > computerScore)
//                    gameStatusLabel.text = "Game Over - You Win!";
//                else if (computerScore > player1Score)
//                    gameStatusLabel.text = "Game Over - Computer Wins!";
//                else
//                    gameStatusLabel.text = "Game Over - It's a Tie!";
//            }
//            else
//            {
//                gameStatusLabel.text = "Game in Progress";
//            }
//        }
//    }
//}