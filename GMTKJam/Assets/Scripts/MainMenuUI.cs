using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI bestScore;

    // Start is called before the first frame update
    void Start()
    {
        if (bestScore)
        {
            if (PlayerPrefs.HasKey("BestScore"))
            {
                bestScore.text += PlayerPrefs.GetInt("BestScore");
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

    public void StartGame()
    {
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers[0])
        {
            GameManager gm = gameManagers[0].GetComponent<GameManager>();
            if (gm)
            {
                gm.StartGame();
            }
        }
    }
}
