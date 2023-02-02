using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // PLAYER STATS
    private int currentScore = 0;
    private int lives = 3;    
    private int maxBombs = 1;
    private int explodeRange = 1;
    private float moveSpeed = 4f;
    private int speedCounter = 1;
    

    [SerializeField] private int bombLimit = 6;
    [SerializeField] private int explodeLimit = 5;
    [SerializeField] private int speedLimit = 5;
    private float speedIncrease = .4f;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParentTransform;

    private PlayerController currentPlayer;

    [SerializeField] private float delayToSpawnPlayer = 1f;

    [SerializeField] private CameraController myCamera;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI maxBombsText;
    [SerializeField] private TextMeshProUGUI explodeRangeText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI levelNameText;

    private bool isPaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winGamePanel;

    private int enemiesThisLevel = 0;

    [SerializeField] private bool isLastLevel = false;

    // Const string variables for our PlayerPref Keys
    const string CurrentScoreKey = "CurrentScore";
    const string LivesKey = "Lives";
    const string MaxBombsKey = "MaxBombs";
    const string ExplodeRangeKey = "ExplodeRange";
    const string MoveSpeedKey = "MoveSpeed";


    private void Awake()
    {
        LoadPlayerPrefs();        
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScore(0);
        UpdateLivesText();
        UpdateMaxBombsText();
        UpdateExplodeRangeText();
        UpdateSpeedText();
        UpdateLevelText();
        SpawnPlayer();

        enemiesThisLevel = GetEnemyCount();        
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseButton();
        }
    }

    public void PlayerDied()
    {
        if (lives > 1)
        {            
            lives--;
            
            // Spawn new player
            Invoke("SpawnPlayer", currentPlayer.GetDestroyDelayTime() + delayToSpawnPlayer);
        }
        else
        {
            Invoke("GameOver", 3f);            
        }
        
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity, playerParentTransform);
        currentPlayer = player.GetComponent<PlayerController>();
        currentPlayer.InitializePlayer(maxBombs, moveSpeed);
        myCamera.SetPlayer(player);
        UpdateLivesText();
    }

    public void UpdateScore(int scoreToAdd)
    {        
        currentScore += scoreToAdd;
        scoreText.text = "Score: " + currentScore.ToString("D6");     
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString("D2");
    }

    private void UpdateMaxBombsText()
    {
        maxBombsText.text = maxBombs.ToString("D2");
    }

    private void UpdateExplodeRangeText()
    {
        explodeRangeText.text = explodeRange.ToString("D2");
    }

    private void UpdateSpeedText()
    {
        speedText.text = speedCounter.ToString("D2");
    }

    private void UpdateLevelText()
    {
        levelNameText.text = SceneManager.GetActiveScene().name;
    }





    public void PauseButton()
    {        
        if (isPaused)
        {
            pausePanel.SetActive(false);
            currentPlayer.SetPaused(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            pausePanel.SetActive(true);
            currentPlayer.SetPaused(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        
    }


    private void GameOver()
    {
        gameOverPanel.SetActive(true);
    }    

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private int GetEnemyCount()
    {
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        return count;
    }

    public void EnemyHasDied()
    {
        enemiesThisLevel--;
        
        if (enemiesThisLevel <= 0)
        {
            if (isLastLevel)
            {
                // Tell the player to play his victory animation
                currentPlayer.PlayVictory();

                // Display the win game panel after a delay
                Invoke("DisplayWinPanel", 3f);                
            }
            else
            {
                // Tell the player to play his victory animation
                currentPlayer.PlayVictory();
                
                // Save all of the player data to PlayerPrefs so that it can be loaded in the next level
                SavePlayerData();

                Invoke("LoadNextLevel", 3f);
            }            
        }
    }

    private void DisplayWinPanel()
    {
        winGamePanel.SetActive(true);
    }


    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


    public void IncreaseMaxBombs()
    {
        maxBombs++;
        maxBombs = Mathf.Clamp(maxBombs, 1, bombLimit);
        UpdateMaxBombsText();
        currentPlayer.InitializePlayer(maxBombs, moveSpeed);
    }

    public void IncreaseSpeed()
    {
        if (speedCounter < speedLimit)
        {
            moveSpeed += speedIncrease;
            //moveSpeed = Mathf.Clamp(moveSpeed, 4f, speedLimit);
            speedCounter++;
            UpdateSpeedText();
            currentPlayer.InitializePlayer(maxBombs, moveSpeed);
        }        
    }

    // Bombs can call this method to know how much they should set the explode range to
    public int GetExplodeRange()
    {
        return explodeRange;
    }

    public void IncreaseExplodeRange()
    {
        explodeRange++;
        explodeRange = Mathf.Clamp(explodeRange, 1, explodeLimit);
        UpdateExplodeRangeText();
    }


    private void SavePlayerData()
    {
        PlayerPrefs.SetInt(CurrentScoreKey, currentScore);
        PlayerPrefs.SetInt(LivesKey, lives);
        PlayerPrefs.SetInt(MaxBombsKey, maxBombs);
        PlayerPrefs.SetInt(ExplodeRangeKey, explodeRange);
        PlayerPrefs.SetFloat(MoveSpeedKey, moveSpeed);
    }

    private void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(CurrentScoreKey))
        {
            currentScore = PlayerPrefs.GetInt(CurrentScoreKey);
        }

        if (PlayerPrefs.HasKey(LivesKey))
        {
            lives = PlayerPrefs.GetInt(LivesKey);
        }

        if (PlayerPrefs.HasKey(MaxBombsKey))
        {
            maxBombs = PlayerPrefs.GetInt(MaxBombsKey);
        }

        if (PlayerPrefs.HasKey(ExplodeRangeKey))
        {
            explodeRange = PlayerPrefs.GetInt(ExplodeRangeKey);
        }

        if (PlayerPrefs.HasKey(MoveSpeedKey))
        {
            moveSpeed = PlayerPrefs.GetFloat(MoveSpeedKey);
        }
    }
}
