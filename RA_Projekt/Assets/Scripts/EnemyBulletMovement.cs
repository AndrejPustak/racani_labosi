using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    public float bulletSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-1f, 0f, 0f) * bulletSpeed * Time.deltaTime;
    }
}
