using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class go_commentForm : MonoBehaviour
{
    public void FormButtonOnClick()
    {
        // go link
        Application.OpenURL("http://127.0.0.1:8000/");
        // close game
        Application.Quit();
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
