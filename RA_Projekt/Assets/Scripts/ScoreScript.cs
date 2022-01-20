using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] Text scoreText;

    void Start()
    {
        scoreText.text = "Score: "+Score.score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
