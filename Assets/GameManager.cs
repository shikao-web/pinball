using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Text;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public ball ball;

    // Log Data Export
    private string log_result_path;

    [Serializable]
    public class SampleLogData
    {
        public int hp;
    }


    void Awake()
    {
        if (Application.isEditor)
        {
            log_result_path = Path.Combine(Application.dataPath, "data", "log_result.csv");
        }
        else
        {
            log_result_path = Path.Combine(Application.persistentDataPath, "log_lesult.csv");
        }
    }

    public void LogResult(int hp)
    {

        SampleLogData log = new SampleLogData { 
            hp = hp,
        };

        StartCoroutine(SendLogCoroutine(log));

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

    public IEnumerator SendLogCoroutine(SampleLogData log)
    {
        // .env から設定値を取得
        string supabaseUrl = EnvLoader.Get("SUPABASE_URL");
        string supabaseKey = EnvLoader.Get("SUPABASE_KEY");
        string tableName   = EnvLoader.Get("TABLE_NAME", "sample_logs"); // デフォルト値指定も可

        if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseKey))
        {
            Debug.LogError("SUPABASE_URL または SUPABASE_KEY が .env に設定されていません。");
            yield break;
        }

        string endpoint = $"{supabaseUrl}/rest/v1/{tableName}";
        string jsonPayload = JsonUtility.ToJson(log);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(endpoint, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("apikey", supabaseKey);
            request.SetRequestHeader("Authorization", $"Bearer {supabaseKey}");
            request.SetRequestHeader("Prefer", "return=minimal");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Supabaseへログ送信完了！");
            }
            else
            {
                Debug.LogError($"送信エラー: {request.error} | {request.downloadHandler.text}");
            }
        }
    }
}
