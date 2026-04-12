using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popLeft : MonoBehaviour
{
    // speed
    private float speed = 1000;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -5f * speed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -30f);
        }
    }

    // define function


}

