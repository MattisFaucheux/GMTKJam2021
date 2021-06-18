using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    float moveSpeed = 1.0f;
    Vector3 dir = new Vector3(-1, 0, 0);
    bool isSlow;
    [SerializeField] float slowSpeedMultiplicator = 0.25f;

    [SerializeField] int nbRebounds = 0;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller)
        {
            if (isSlow)
            {
                controller.Move(dir * moveSpeed * Time.deltaTime * slowSpeedMultiplicator);
            }
            else
            {
                controller.Move(dir * moveSpeed * Time.deltaTime);
            }
        }


    }

    public void SetDir(Vector3 direction)
    {
        dir = direction;
    }

    public void SetMoveSpeed(float movementSpeed)
    {
        moveSpeed = movementSpeed;
    }

    public void SetIsSlow(bool isBulletSlow)
    {
        isSlow = isBulletSlow;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (tag == "PlayerWhiteBullet")
        {
            if (hit.collider.tag == "EnemyWhite" || hit.collider.tag == "EnemyWhiteBullet")
            {
                Destroy(hit.collider.gameObject, 0);

                Destroy(gameObject, 0);
                return;
            }
        }
        else if (tag == "PlayerBlackBullet")
        {
            if (hit.collider.tag == "EnemyBlack" || hit.collider.tag == "EnemyBlackBullet")
            {
                Destroy(hit.collider.gameObject, 0);

                Destroy(gameObject, 0);
                return;
            }
        }
        else if (tag == "EnemyWhiteBullet")
        {
            if (hit.collider.tag == "PlayerWhiteBullet")
            {
                Destroy(hit.collider.gameObject, 0);

                Destroy(gameObject, 0);
                return;
            }
            else if(hit.collider.tag == "PlayerWhite")
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
        else if (tag == "EnemyBlackBullet")
        {
            if (hit.collider.tag == "PlayerBlackBullet")
            {
                Destroy(hit.collider.gameObject, 0);

                Destroy(gameObject, 0);
                return;
            }
            else if (hit.collider.tag == "PlayerBlack")
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

        if (nbRebounds > 0)
        {

            dir = hit.normal;
            nbRebounds -= 1;
            return;
        }

        if (tag != hit.transform.tag)
        {
            Destroy(gameObject, 0);
        }

    }
 }
