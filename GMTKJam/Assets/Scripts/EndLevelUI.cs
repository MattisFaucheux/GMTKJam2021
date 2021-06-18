using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevelUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI death;

    [SerializeField]
    TextMeshProUGUI timer;

    [SerializeField]
    TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if(gameManagers[0])
        {
            GameManager gm = gameManagers[0].GetComponent<GameManager>();
            if(gm)
            {
                death.text += gm.DeathCounter;
                timer.text += (int)gm.Timer;
                int scoreInt = (int)gm.Timer + (gm.DeathCounter * 10);

                if(PlayerPrefs.HasKey("BestScore"))
                {
                    int savedScore = PlayerPrefs.GetInt("BestScore");
                    if(savedScore > scoreInt)
                    {
                        PlayerPrefs.SetInt("BestScore", scoreInt);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("BestScore", scoreInt);
                }

                score.text += scoreInt;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers[0])
        {
            GameManager gm = gameManagers[0].GetComponent<GameManager>();
            if (gm)
            {
                gm.Quit();
            }
        }
    }

    public void LoadMainMenu()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers[0])
        {
            GameManager gm = gameManagers[0].GetComponent<GameManager>();
            if (gm)
            {
                gm.LoadMainMenu();
            }
        }
    }
}
