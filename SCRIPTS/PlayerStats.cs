using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int playerMoney;
    public int levelStartMoney = 500;

    public static int playerLives;
    public int levelStartLives = 3;

    public static int levelWavesSurvived;

    private void Start()
    {
        playerMoney = levelStartMoney;
        playerLives = levelStartLives;

        levelWavesSurvived = 0;
    }
}
