using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 3f;
    public Canvas canvas;

    private float w = 8;

    // Start is called before the first frame update
    void Start()
    {

        //float w = canvas.GetComponent<RectTransform>().rect.width;
        transform.position = new Vector3(-w, 0f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0f, 1f, 0f) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0f, -1f, 0f) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1f, 0f, 0f) * moveSpeed * Time.deltaTime;
        }




    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("END GAME");
    }
}
