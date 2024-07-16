using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public Transform spawnPoint;
    int score = 0;
    int bestScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public GameObject playButton;
    public GameObject player;
    public GameObject TittleGame;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestScoreText.text = "Best Score: " + bestScore.ToString();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float waitTime = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
        }
    }

    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameStart()
    {
        player.SetActive(true);
        playButton.SetActive(false);
        TittleGame.SetActive(false);
        score = 0;
        scoreText.text = score.ToString();
        StartCoroutine("SpawnObstacles");
        InvokeRepeating("ScoreUp", 2f, 1f);
        audioManager.PlayMusic();
    }

    public void GameOver()
    {
        StopCoroutine("SpawnObstacles");
        CancelInvoke("ScoreUp");
        CheckBestScore();
        playButton.SetActive(true);
        TittleGame.SetActive(true);
        player.SetActive(false);
        audioManager.StopMusic();
        StartCoroutine(RestartGameAfterDelay(0.5f)); // Add a delay to allow the death sound to play
    }

    void CheckBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            bestScoreText.text = "Best Score: " + bestScore.ToString();
        }
    }

    private IEnumerator RestartGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Box Jump");
    }
}
