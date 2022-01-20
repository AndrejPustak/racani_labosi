using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float w = 9;
    private float h = 5;

    public float horizontalSpeed = 2f;
    public float verticalSpeed = 1f;

    public Vector3 vertical;
    public Vector3 horizontal;

    private GameObject game;

    public int group;

    // Start is called before the first frame update
    void Start()
    {

        game = GameObject.FindGameObjectWithTag("GameController");

        group = game.GetComponent<Game>().currentGroup;

        vertical = new Vector3(0f, -1f, 0f);
        horizontal = new Vector3(-1f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += horizontal * horizontalSpeed * Time.deltaTime;
        transform.position += vertical * verticalSpeed * Time.deltaTime;

    }

    public void flip()
    {
        vertical.y = -vertical.y;
    }
}
