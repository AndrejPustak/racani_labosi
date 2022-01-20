using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounterScript : MonoBehaviour
{
    Text text;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        Score.score = counter;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + counter;
    }

    public void increaseCounter()
    {
        counter++;

        Score.score = counter;

    }
}
