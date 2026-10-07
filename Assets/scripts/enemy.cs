using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float difficulty_to_destroying = 0.0f;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        if(difficulty_to_destroying >= gameManager.GAME_DIFFICULTY)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
