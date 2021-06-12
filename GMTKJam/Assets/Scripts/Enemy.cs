using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool isSlow;
    [SerializeField] float slowSpeedMultiplicator = 0.25f;

    [SerializeField] float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
