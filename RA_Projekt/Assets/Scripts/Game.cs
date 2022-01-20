using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Transform pfEnemy;
    [SerializeField] private Transform pfEnemy2;
    [SerializeField] private Text killCounter;
    List<Transform> enemies;

    public int enemiesPerRow = 5;
    private float w = 9; 
    private float h = 5;

    public float enemyWaveFreq = 1f;
    private float timeCounter = 0f;

    public int currentGroup = 0;

    private int enemiesPasses = 5;
    // Start is called before the first frame update
    void Start()
    {

        w = Screen.width / 100 / 2;
        h = Screen.height / 100 / 2;
        createEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime;
        if(timeCounter > enemyWaveFreq)
        {
            enemiesPerRow = Random.Range(3, 6);
            createEnemies();
            timeCounter -= enemyWaveFreq;
        }

        checkForCollision();
    }

    void createEnemies()
    {
        float d = h * 2 / (enemiesPerRow + 1);
        for (int i = 1; i < enemiesPerRow + 1; i++)
        {
            if(Random.Range(0f,1f) < 0.9)
                Instantiate(pfEnemy, new Vector3(w - 1f, h - d * i, 0f), Quaternion.identity);
            else
                Instantiate(pfEnemy2, new Vector3(w - 1f, h - d * i, 0f), Quaternion.identity);
        }

        if (Random.Range(0f, 1f) > 0.5) {
            flipGroup(currentGroup);
        }

        currentGroup++;
    }

    void checkForCollision()
    {

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < bullets.Length; i++)
        {
            if (playerEnemyCollision(player, bullets[i]))
            {
                Destroy(player);
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            
            if (playerEnemyCollision(player, enemies[i]))
            {
                print(enemies[i].GetComponent<Renderer>().bounds);
                Destroy(player);
                Destroy(enemies[i]);
            }
            if (enemies[i].transform.position.x < -w)
            {
                Destroy(enemies[i]);
                enemiesPasses--;
            }

            if(enemiesPasses <= 0)
            {
                Destroy(player);
            }

            if(enemies[i].transform.position.y > h || enemies[i].transform.position.y < -h)
            {
                flipGroup(enemies[i].GetComponent<EnemyMovement>().group);
            }
        }
    }

    bool playerEnemyCollision(GameObject player, GameObject enemy)
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

    void flipGroup(int group)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i].GetComponent<EnemyMovement>().group == group)
            {
                enemies[i].GetComponent<EnemyMovement>().flip();
            }
        }
    }

}
