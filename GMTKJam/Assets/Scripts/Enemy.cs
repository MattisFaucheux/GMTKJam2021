using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isSlow = false;
    [SerializeField] float slowSpeedMultiplicator = 0.25f;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float timeBeforeShoot = 1.5f;

    [SerializeField] bool targetPlayer = false;

    [SerializeField] float distanceSpawnBullet = 2;
    [SerializeField] float shootReloadTime = 1.0f;
    [SerializeField] float bulletSpeed = 15.0f;
    [SerializeField] int numberBullets = 5;

    [SerializeField] GameObject enemyWhiteBullet;
    [SerializeField] GameObject enemyBlackBullet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeShoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlow)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * slowSpeedMultiplicator);
        }
        else
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetIsSlow(bool isEnemySlow)
    {
        isSlow = isEnemySlow;
    }

    IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {

        if (targetPlayer)
        {
            InstantiateTargetBullet();
        }
        else
        {
            InstatiateRandomBullets();
        }


        yield return new WaitForSeconds(shootReloadTime);
        StartCoroutine(Shoot());
    }

    void InstatiateRandomBullets()
    {
        float pi = Mathf.PI;
        float angle = (pi * 2.0f) / (float)numberBullets;

        float additiveAngle = transform.rotation.eulerAngles.z * (pi / 180.0f);

        for (int i = 0; i < numberBullets; i++)
        {
            Vector3 dir, bulletPos;
            GameObject bulletObject;

            float cosX = (angle * (i + 1)) + additiveAngle;
            float cosY = (angle * (i + 1)) + additiveAngle;

            while(cosX > 360)
            {
                cosX -= 360;
            }

            while (cosY > 360)
            {
                cosY -= 360;
            }


            dir.x = Mathf.Cos(cosX);
            dir.y = Mathf.Sin(cosY);
            dir.z = 0;
            dir.Normalize();
            dir *= distanceSpawnBullet;

            bulletPos = transform.position + dir;

            if (tag == "EnemyWhite")
            {
                bulletObject = Instantiate(enemyWhiteBullet, bulletPos, new Quaternion(0, 0, 0, 0));
            }
            else if (tag == "EnemyBlack")
            {
                bulletObject = Instantiate(enemyBlackBullet, bulletPos, new Quaternion(0, 0, 0, 0));
            }
            else
            {
                bulletObject = Instantiate(enemyWhiteBullet, bulletPos, new Quaternion(0, 0, 0, 0));
            }

            Bullet bullet = bulletObject.GetComponent<Bullet>();

            if (bullet)
            {
                dir.Normalize();
                bullet.SetDir(dir);
                bullet.SetMoveSpeed(bulletSpeed);

                if (isSlow)
                {
                    bullet.SetIsSlow(true);
                }
            }
        }
    }

    void InstantiateTargetBullet()
    {
        Vector3 dir, bulletPos;
        GameObject bulletObject;

        GameObject player;
        if (tag == "EnemyWhite")
        {
            player = GameObject.FindGameObjectWithTag("PlayerWhite");
        }
        else if (tag == "EnemyBlack")
        {
            player = GameObject.FindGameObjectWithTag("PlayerBlack");
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("PlayerWhite");
        }

        dir = player.transform.position - transform.position;
        dir.z = 0;
        dir.Normalize();
        dir *= distanceSpawnBullet;

        bulletPos = transform.position + dir;

        if (tag == "EnemyWhite")
        {
            bulletObject = Instantiate(enemyWhiteBullet, bulletPos, new Quaternion(0, 0, 0, 0));
        }
        else if (tag == "EnemyBlack")
        {
            bulletObject = Instantiate(enemyBlackBullet, bulletPos, new Quaternion(0, 0, 0, 0));
        }
        else
        {
            bulletObject = Instantiate(enemyWhiteBullet, bulletPos, new Quaternion(0, 0, 0, 0));
        }

        Bullet bullet = bulletObject.GetComponent<Bullet>();

        if (bullet)
        {
            dir.Normalize();
            bullet.SetDir(dir);
            bullet.SetMoveSpeed(bulletSpeed);

            if (isSlow)
            {
                bullet.SetIsSlow(true);
            }
        }
    }
}
