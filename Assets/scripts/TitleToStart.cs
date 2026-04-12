using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleToStart : MonoBehaviour
{
    public float start_time = 0;
    public void StartButtonOnClick()
    {
        // game start
        SceneManager.LoadScene("10_scene1-1");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
