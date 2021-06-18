using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int levelCounter = 0;
    private int deathCounter = 0;
    private bool isTimerStart = false;
    private float timer = 0.0f;

    public int LevelCounter { get { return levelCounter; } }
    public int DeathCounter { get { return deathCounter; } }
    public float Timer { get { return timer; } }

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isTimerStart)
        {
            timer += Time.deltaTime;

            if (SceneManager.GetActiveScene().name != "EndLevel" && SceneManager.GetActiveScene().name != "EndLevelTest")
            {
                GameObject[] enemiesWhite = GameObject.FindGameObjectsWithTag("EnemyWhite");
                GameObject[] enemiesBlack = GameObject.FindGameObjectsWithTag("EnemyBlack");

                int totalEnemies = enemiesWhite.Length + enemiesBlack.Length;

                if (totalEnemies <= 0)
                {
                    LoadNextLevel();
                }
            }
        }
    }

    public void StartGame()
    {
        levelCounter += 1;
        isTimerStart = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    private void LoadNextLevel()
    {
        levelCounter += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void AddDeath()
    {
        deathCounter += 1;
    }

    public void LoadMainMenu()
    {
        levelCounter = 0;
        deathCounter = 0;
        timer = 0;
        isTimerStart = false;
        SceneManager.LoadScene(0);
    }
}
