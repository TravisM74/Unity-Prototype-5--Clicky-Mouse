using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button resetButton;
    public GameObject titleScreen;
    private int score;
    private float spawnRate = 1f;
    public int startLives = 3;
    public int lives;
    public bool isGameActive;
    public GameObject pauseScreen;
    private bool paused;


    // Start is called before the first frame update
    void Start()
    {
       
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)){
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget(){
        while (isGameActive){
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0 , targets.Count);
            Instantiate(targets[index]);

        }

    }



    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.text = "Score : " + score;
        
    }
    public void UpdateLives(int newLives){
        lives = newLives;
        livesText.text = "Lives :" + lives;
    }

    public void GameOver(){
          gameOverText.gameObject.SetActive(true);
          resetButton.gameObject.SetActive(true);
          isGameActive = false;
    }
     public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty){
        isGameActive = true;
        score = 0;
        
        UpdateLives(startLives);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    void ChangePaused(){
        if(!paused){
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        } else {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
