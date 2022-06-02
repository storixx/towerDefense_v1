using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverScene;

    public CameraController cameraController;

    GameObject[] gameObjects;

    private void Start()
    {
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver)
        {
            return;
        }

        if(Input.GetKeyDown("e"))
        {
            endGame();
        }
        
        if (PlayerStats.playerLives <= 0)
        {
            endGame();
        }
    }

    void endGame()
    {
        gameIsOver = true;

        gameOverScene.SetActive(true);

        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject gameObject in gameObjects)
            Destroy(gameObject);

        cameraController.allowMovement = false;

        Time.timeScale = 0;
    }
}
