using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Rotate(new Vector3(0, speed*10 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Rotate(new Vector3(0, -speed*10 * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
}
