using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletMovement : MonoBehaviour
{

    public float bulletSpeed = 20f;
    private GameObject[] enemies;

    private Vector3 direction;

    [SerializeField] private GameObject killCounter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        killCounter = GameObject.FindGameObjectWithTag("KillCounter");

        direction = new Vector3(1f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        transform.position += direction * bulletSpeed * Time.deltaTime;

        for(int i = 0; i < enemies.Length; i++)
        {
            if (bulletEnemyCollision(gameObject, enemies[i]))
            {
                int enemyType = enemies[i].GetComponent<EnemyShoot>().enemyType;
                if (enemyType == 2)
                {
                    direction.x = -1f;
                }
                else if(enemyType == 1)
                {
                    Destroy(gameObject);
                    killCounter.GetComponent<KillCounterScript>().increaseCounter();
                }
                enemies[i].GetComponent<EnemyShoot>().gotHit();
                
            }
        }

        if(Vector3.Distance(transform.position, player.transform.position) < 0.3)
        {
            Destroy(player);
            Destroy(gameObject);
        }
        
    }

    bool bulletEnemyCollision(GameObject player, GameObject enemy)
    {
        bool collision = false;

        Vector3 playerSize = player.GetComponent<SpriteRenderer>().bounds.size;
        Vector3 enemySize = enemy.GetComponent<SpriteRenderer>().bounds.size;

        Vector3 playerPos = player.transform.position;
        Vector3 enemyPos = enemy.transform.position;
        Rect r1 = new Rect(playerPos.x - playerSize.x / 2, playerPos.y - playerSize.y / 2, playerSize.x, playerSize.y);
        Rect r2 = new Rect(enemyPos.x - enemySize.x / 2, enemyPos.y - enemySize.y / 2, enemySize.x, enemySize.y);

        collision = rectCollision(r1, r2);

        return collision;
    }

    bool rectCollision(Rect r1, Rect r2)
    {
        bool collision = false;

        if (r1.x + r1.width >= r2.x &&
            r1.x <= r2.x + r2.width &&
            r1.y + r1.height >= r2.y &&
            r1.y <= r2.y + r2.height)
            collision = true;

        return collision;
    }


}
