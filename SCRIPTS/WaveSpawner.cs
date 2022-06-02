using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPref;

    public Transform spawnPoint;

    public float timeToNewWave = 5f;
    private float countdown = 2f;

    public Text waveCount_Text;

    public GameOver gameOver;
    
    private int waveIndex = 0;

    private void Update()
    {
        if(countdown <= 0f)
        {
            if (GameMananger.gameIsOver == true)
            {
                return;
            }

            StartCoroutine(spawnNewWave());
            countdown = timeToNewWave;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCount_Text.text = string.Format("{0:00}", countdown);
    }

    IEnumerator spawnNewWave()
    {      
        //Debug.Log("NEW WAVE");
        
        waveIndex++;
        PlayerStats.levelWavesSurvived++;

        for(int i = 0; i < waveIndex; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void spawnEnemy()
    {
        Instantiate(enemyPref, spawnPoint.position, spawnPoint.rotation);
    }
}
