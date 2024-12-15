using UnityEngine;

public class HandPointer : MonoBehaviour
{
    public Camera mainCamera;
    public float handHeightOffset = 0.3f;
    private GameObject lastHoveredPit;
    private MancalaGameManager gameManager;
    private Material lastPitMaterial;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        gameManager = FindObjectOfType<MancalaGameManager>();
    }

    void Update()
    {
        FollowMouseCursor();
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void FollowMouseCursor()
    {
        if (mainCamera == null || gameManager == null)
        {
            Debug.LogError("Main Camera or MancalaGameManager is not set.");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Update hand pointer position
            Vector3 targetPosition = hit.point;
            targetPosition.y += handHeightOffset;
            transform.position = targetPosition;

            // Handle pit highlighting
            if (hit.collider.gameObject != lastHoveredPit)
            {
                ResetLastHoveredPit();

                lastHoveredPit = hit.collider.gameObject;

            }
        }
        else
        {
            ResetLastHoveredPit();
        }
    }

    private void ResetLastHoveredPit()
    {
        if (lastHoveredPit != null)
        {
            var renderer = lastHoveredPit.GetComponent<MeshRenderer>();
            if (renderer != null && lastPitMaterial != null)
            {
                renderer.material = lastPitMaterial;
            }
            lastHoveredPit = null;
        }
    }



    private void HandleClick()
    {
        if (gameManager == null || gameManager.CurrentGame == null)
        {
            Debug.LogError("GameManager or CurrentGame is null!");
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (gameManager.pits.Contains(hit.collider.gameObject))
            {
                int pitIndex = gameManager.pits.IndexOf(hit.collider.gameObject);

                Debug.Log($"Pit clicked: {pitIndex}");
                if (IsPitPlayable(hit.collider.gameObject))
                {
                    Debug.Log($"Making move on pit {pitIndex}");
                    StartCoroutine(gameManager.MakeMove(pitIndex));
                }
                else
                {
                    LogInvalidMoveReason(pitIndex);
                }
            }
        }
    }

    private bool IsPitPlayable(GameObject pit)
    {
        if (gameManager == null || gameManager.CurrentGame == null) return false;

        int pitIndex = gameManager.pits.IndexOf(pit);
        if (pitIndex == -1) return false;

        var gamePit = gameManager.CurrentGame.pits[pitIndex];
        return !gamePit.isMancala &&
               gamePit.stones > 0 &&
               gamePit.playerId == gameManager.CurrentGame.player1Id &&
               gameManager.CurrentGame.currentTurn == gameManager.CurrentGame.player1Id &&
               !gameManager.CurrentGame.isGameOver;
    }

    private void LogInvalidMoveReason(int pitIndex)
    {
        if (gameManager == null || gameManager.CurrentGame == null) return;

        var gamePit = gameManager.CurrentGame.pits[pitIndex];
        Debug.Log($"Pit is not playable:" +
            $"\nIsMancala: {gamePit.isMancala}" +
            $"\nStones: {gamePit.stones}" +
            $"\nPitPlayerId: {gamePit.playerId}" +
            $"\nCurrentTurn: {gameManager.CurrentGame.currentTurn}");
    }
}
