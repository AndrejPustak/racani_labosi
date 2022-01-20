using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{   
    public Camera mainCamera;

    public int lifeTime = 1000;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 vec1 = mainCamera.transform.forward;
        print(vec1);
        Vector3 vec2 = this.gameObject.transform.forward;

        float yAngle = Vector3.Angle(vec1, vec2);
        this.gameObject.transform.Rotate(0.0f, yAngle, 0.0f, Space.Self);
        this.gameObject.transform.Translate(0.0f, 0.1f, 0.0f, Space.Self);

        lifeTime--;
        if(lifeTime == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
