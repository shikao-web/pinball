using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class titleLogo : MonoBehaviour
{
    // define
    private float titleLogo_timer;
    private int color_type = 0;
    
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        titleLogo_timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        titleLogo_timer += Time.deltaTime;
        if (titleLogo_timer > 0.4)
        {
            if (color_type == 0)
            {
                text.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                color_type = 1;
                Debug.Log("マゼンタ");
            }
            else if (color_type == 1)
            {
                text.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
                color_type = 2;
                Debug.Log("イエロー");
            }
            else if (color_type == 2)
            {
                text.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
                color_type = 3;
                Debug.Log("シアン");
            }
            else if (color_type == 3)
            {
                text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                color_type = 0;
                Debug.Log("white");
            }

            titleLogo_timer = 0;
        }
    }
}
