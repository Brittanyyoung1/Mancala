using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class PitCounter : MonoBehaviour
{
    public TMP_Text counterText;
    private int currentCount;

    public void UpdateCount(int count)
    {
        currentCount = count;
        counterText.text = count.ToString();
    }
}
