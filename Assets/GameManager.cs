using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public ball ball;

    // Log Data Export
    private string log_result_path;

    void Awake()
    {
        log_result_path = Path.Combine(Application.dataPath, "data", "log_result.csv");
    }

    public void LogResult(int hp)
    {

        if (!File.Exists(log_result_path))
        {
            File.WriteAllText(log_result_path, "timestamp,hp,result\n");
        }

        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        int result;
        if (hp == 0)
        {
            result = 0;
        }
        else
        {
            result = 1;
        }

        string line = $"{timestamp},{hp},{result}\n";

        File.AppendAllText(
            log_result_path,
            line
        );
    }

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
            LogResult( ball.heart );
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
        LogResult(0);
        SceneManager.LoadScene("20_GameOver");
    }
}
