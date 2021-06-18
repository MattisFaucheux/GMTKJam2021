using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    { 
        if (hit.gameObject.tag == "EnemyWhite" || hit.gameObject.tag == "EnemyBlack")
        {
            GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
            if (gameManagers[0])
            {
                GameManager gm = gameManagers[0].GetComponent<GameManager>();
                if (gm)
                {
                    gm.AddDeath();
                }
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
    }
}
