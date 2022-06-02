using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives_UI : MonoBehaviour
{
    public Text livesCount;

    private void Update()
    {
        livesCount.text = PlayerStats.playerLives + " Lives";
    }
}
