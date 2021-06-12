using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] CharacterController playerWhite;
    [SerializeField] CharacterController playerBlack;
    [SerializeField] float moveSpeed = 100;

    bool isPlayerWhiteSlow = false;
    bool isPlayerBlackSlow = false;

    bool canSlow = true;

    [SerializeField] LayerMask clickDetection;
    [SerializeField] float slowSpeedMultiplicator = 0.25f;
    [SerializeField] float slowTime = 5.0f;

    [SerializeField] GameObject playerWhiteBullet;
    [SerializeField] GameObject playerBlackBullet;

    [SerializeField] float distanceSpawnBullet = 2;

    bool canPlayerWhiteShoot = true;
    bool canPlayerBlackShoot = true;

    [SerializeField] float shootReloadTime = 1.0f;

    [SerializeField] float bulletSpeed = 15.0f;


    void Start()
    {
        
    }

    void Update()
    {
        MovePlayers();
        SlowPlayer();
        PlayerShoot();
    }

    void PlayerShoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "BlackZone" && canPlayerWhiteShoot)
                {
                    Vector3 playerPos = playerWhite.transform.position;
                    Vector3 hitPos = hit.point;
                    Vector3 dir = hitPos - playerPos;
                    dir.z = 0;
                    dir.Normalize();
                    dir *= distanceSpawnBullet;

                    Vector3 bulletPos = playerPos + dir;
                    GameObject bulletObject = Instantiate(playerWhiteBullet, bulletPos, new Quaternion(0, 0, 0, 0));
                    Bullet bullet = bulletObject.GetComponent<Bullet>();

                    if (bullet)
                    {
                        dir.Normalize();
                        bullet.SetDir(dir);
                        bullet.SetMoveSpeed(bulletSpeed);

                        if (isPlayerWhiteSlow)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    canPlayerWhiteShoot = false;
                    StartCoroutine(ResetCanPlayerShoot(true));
                }
                else if (hit.collider.gameObject.tag == "WhiteZone" && canPlayerBlackShoot)
                {
                    Vector3 playerPos = playerBlack.transform.position;
                    Vector3 hitPos = hit.point;
                    Vector3 dir = hitPos - playerPos;
                    dir.z = 0;
                    dir.Normalize();
                    dir *= distanceSpawnBullet;

                    Vector3 bulletPos = playerPos + dir;
                    GameObject bulletObject = Instantiate(playerBlackBullet, bulletPos, new Quaternion(0, 0, 0, 0));
                    Bullet bullet = bulletObject.GetComponent<Bullet>();

                    if(bullet)
                    {
                        dir.Normalize();
                        bullet.SetDir(dir);
                        bullet.SetMoveSpeed(bulletSpeed);

                        if (isPlayerBlackSlow)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    canPlayerBlackShoot = false;
                    StartCoroutine(ResetCanPlayerShoot(false));
                }
            }
        }
    }

    void SlowPlayer()
    {
        if (Input.GetButtonDown("SlowMotion") && canSlow)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "BlackZone")
                {
                    isPlayerWhiteSlow = true;

                    GameObject[] PlayerBullets = GameObject.FindGameObjectsWithTag("PlayerWhiteBullet");
                    foreach(GameObject gm in PlayerBullets)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if(bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyWhiteBullet");
                    foreach (GameObject gm in enemyBullets)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if (bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyWhite");
                    foreach (GameObject gm in enemies)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if (bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }
                }
                else if (hit.collider.gameObject.tag == "WhiteZone")
                {
                    isPlayerBlackSlow = true;

                    GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBlackBullet");
                    foreach (GameObject gm in playerBullets)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if (bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBlackBullet");
                    foreach (GameObject gm in enemyBullets)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if (bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }

                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBlack");
                    foreach (GameObject gm in enemies)
                    {
                        Bullet bullet = gm.GetComponent<Bullet>();
                        if (bullet)
                        {
                            bullet.SetIsSlow(true);
                        }
                    }
                }
                else
                {
                    return;
                }

                canSlow = false;
                StartCoroutine(ResetIsPlayerSlow());
            }
        }
    }

    void MovePlayers()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        movement.Normalize();
        movement = movement * moveSpeed * Time.deltaTime;

        if(isPlayerBlackSlow)
        {
            playerBlack.Move(movement * slowSpeedMultiplicator);
        }
        else
        {
            playerBlack.Move(movement);
        }

        if (isPlayerWhiteSlow)
        {
            playerWhite.Move(movement * slowSpeedMultiplicator);
        }
        else
        {
            playerWhite.Move(movement);
        }

    }

    IEnumerator ResetIsPlayerSlow()
    {
        yield return new WaitForSeconds(slowTime);

        if(isPlayerBlackSlow)
        {
            isPlayerBlackSlow = false;

            GameObject[] playerBullets = GameObject.FindGameObjectsWithTag("PlayerBlackBullet");
            foreach (GameObject gm in playerBullets)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBlackBullet");
            foreach (GameObject gm in enemyBullets)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyBlack");
            foreach (GameObject gm in enemies)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }
        }

        if (isPlayerWhiteSlow)
        {
            isPlayerWhiteSlow = false;

            GameObject[] PlayerBullets = GameObject.FindGameObjectsWithTag("PlayerWhiteBullet");
            foreach (GameObject gm in PlayerBullets)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }

            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyWhiteBullet");
            foreach (GameObject gm in enemyBullets)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemyWhite");
            foreach (GameObject gm in enemies)
            {
                Bullet bullet = gm.GetComponent<Bullet>();
                if (bullet)
                {
                    bullet.SetIsSlow(false);
                }
            }
        }
    }

    IEnumerator ResetCanPlayerShoot(bool itsPlayerWhite)
    {
        yield return new WaitForSeconds(shootReloadTime);

        if(itsPlayerWhite)
        {
            canPlayerWhiteShoot = true;
        }
        else
        {
            canPlayerBlackShoot = true;
        }
    }
}
