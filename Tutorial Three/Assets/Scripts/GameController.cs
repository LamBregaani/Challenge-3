using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject [] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text gameoverText;
    public Text restartText;

    private bool gameover;
    private bool restart;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        gameover = false;
        restart = false;
        restartText.text = "";
        gameoverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves ());
    }


        void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

       IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards [Random.Range (0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);


            if (gameover)
            {
                restartText.text = "Press 'T' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "POINTS: " + score;

        if (score >= 100)
        {
            gameoverText.text = "You win!\n Game created by Randall Forehand!";
            gameover = true;
            restart = true;
        }
    }
    public void Gameover()
    {
        gameoverText.text = "Game Over!";
        gameover = true;
    }
}
