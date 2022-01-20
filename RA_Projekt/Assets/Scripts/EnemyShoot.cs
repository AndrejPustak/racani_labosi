using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Sprite[] sprites;
    [SerializeField] private Transform pfEnemyBullet;
    [SerializeField] private Transform gunPoint;

    public int enemyType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0f,1f) < 0.0004)
        {
            Instantiate(pfEnemyBullet, gunPoint.position, Quaternion.identity);
        }
    }

    public void gotHit()
    {   
        if(enemyType == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            enemyType = 1;
        }
        else if(enemyType == 1)
        {
            Destroy(gameObject);
        }
        
    }
}
