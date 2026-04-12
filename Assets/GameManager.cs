using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (DestroyAllEnemies() && SceneManager.GetActiveScene().name == "10_scene1-1" )
        {
            Debug.Log( "ゲームクリア" );
            SceneManager.LoadScene("30_GameClear");
        }
    }

    private bool DestroyAllEnemies()
    {
        foreach (var item in enemies)
        {
            if (item != null)
            {
                return false;
            }
        }
        return true;
    }

    public void GameOver()
    {
        Debug.Log("ゲームオーバー");
        SceneManager.LoadScene("20_GameOver");
    }
}
