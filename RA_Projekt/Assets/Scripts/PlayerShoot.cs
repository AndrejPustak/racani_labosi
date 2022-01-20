using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    [SerializeField] private Transform pfBullet;
    [SerializeField] private Transform gunPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(pfBullet, gunPoint.position, Quaternion.identity);

        }
    }
}
