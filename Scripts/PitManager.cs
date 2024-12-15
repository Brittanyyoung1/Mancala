//using UnityEngine;

//public class PitManager : MonoBehaviour
//{
//    public int stoneCount = 4;
//    public GameObject stonePrefab;
//    private MancalaGameManager mancalaGameManager;
//    private Renderer pitRenderer;
//    public Material defaultMaterial;
//    public Material highlightMaterial;
//    public Material disabledMaterial;

//    void Awake()
//    {
   
//        mancalaGameManager = FindObjectOfType<MancalaGameManager>();
//        if (mancalaGameManager == null)
//        {
//            Debug.LogError($"MancalaGameManager not found for pit: {gameObject.name}");
//        }
//    }
//    void Start()
//    {
        
//        if (mancalaGameManager == null)
//        {
//            mancalaGameManager = FindObjectOfType<MancalaGameManager>();
//        }

//        pitRenderer = GetComponentInChildren<Renderer>();
//        if (pitRenderer != null)
//        {
//            pitRenderer.material = defaultMaterial;
//        }
//        else
//        {
//            Debug.LogError($"No renderer found for pit: {gameObject.name}");
//        }
//    }


//    public void UpdateStoneCount(int newCount)
//    {
//        if (newCount == stoneCount) return;
//        RemoveStones();
//        InitializeStones(newCount);
//        UpdateVisuals();
//    }

//    public void HandleClick()
//    {
//        Debug.Log($"Clicked pit: {gameObject.name}");
//        if (mancalaGameManager == null)
//        {
//            Debug.LogError("No MancalaGameManager found!");

//            mancalaGameManager = FindObjectOfType<MancalaGameManager>();
//        }

//        if (mancalaGameManager != null)
//        {
//            Debug.Log("Forwarding click to MancalaGameManager");
//            mancalaGameManager.HandlePitClick(this);
//        }
//    }

//    private void UpdateVisuals()
//    {
//        if (pitRenderer == null || mancalaGameManager == null || mancalaGameManager._currentGame == null) return;

//        bool isCurrentPlayersPit = IsCurrentPlayersPit();
//        bool canPlay = isCurrentPlayersPit && stoneCount > 0 && !IsMancala();

//        pitRenderer.material = canPlay ?
//            highlightMaterial :
//            (IsMancala() ? disabledMaterial : defaultMaterial);
//    }

//    private bool IsCurrentPlayersPit()
//    {
//        if (mancalaGameManager == null || mancalaGameManager._currentGame == null) return false;

//        int pitIndex = transform.GetSiblingIndex();
//        var game = mancalaGameManager._currentGame;

//        if (game.CurrentTurn == game.Player1Id && pitIndex < 6) return true;
//        if (game.CurrentTurn == game.Player2Id && pitIndex >= 6 && pitIndex < 12) return true;

//        return false;
//    }

//    private bool IsMancala()
//    {
//        return gameObject.name.Contains("Mancala");
//    }

//    public void InitializeStones(int count)
//    {
//        stoneCount = count;
//        RemoveStones();

//        for (int i = 0; i < count; i++)
//        {
//            Vector3 randomOffset = new Vector3(
//                Random.Range(-0.1f, 0.1f),
//                0.1f + (i * 0.02f),
//                Random.Range(-0.1f, 0.1f)
//            );
//            var stone = Instantiate(stonePrefab, transform.position + randomOffset, Random.rotation, transform);
//            stone.transform.rotation = Quaternion.Euler(
//                Random.Range(-30f, 30f),
//                Random.Range(0f, 360f),
//                Random.Range(-30f, 30f)
//            );
//        }
//    }

//    public void AddStone()
//    {
//        stoneCount++;
//        Vector3 randomOffset = new Vector3(
//            Random.Range(-0.1f, 0.1f),
//            0.1f + (stoneCount * 0.02f),
//            Random.Range(-0.1f, 0.1f)
//        );
//        var stone = Instantiate(stonePrefab, transform.position + randomOffset, Random.rotation, transform);
//        stone.transform.rotation = Quaternion.Euler(
//            Random.Range(-30f, 30f),
//            Random.Range(0f, 360f),
//            Random.Range(-30f, 30f)
//        );
//    }

//    public void RemoveStones()
//    {
//        stoneCount = 0;
//        foreach (Transform child in transform)
//        {
//            Destroy(child.gameObject);
//        }
//    }

// public void OnMouseEnter()
//    {
//        if (pitRenderer != null && IsCurrentPlayersPit() && stoneCount > 0 && !IsMancala())
//        {
//            pitRenderer.material = highlightMaterial;
//        }
//    }

//   public void OnMouseExit()
//    {
//        if (pitRenderer != null)
//        {
//            UpdateVisuals();
//        }
//    }
//}