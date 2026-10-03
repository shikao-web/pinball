using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public ball ball;

    // ログ出力先
    private string log_result_path;
    public readonly List<BlockDamageData> destroyedBlocks = new List<BlockDamageData>();

    [Serializable]
    public class BlockDamageData
    {
        public string object_name;
        public int hp;
    }

    [Serializable]
    public class SampleLogData
    {
        public string datetime;
        public int hp_of_final;
        public List<BlockDamageData> hp_of_destroyed_per_block = new List<BlockDamageData>();
    }

    private void Awake()
    {
        if (Application.isEditor)
        {
            // Assets/data フォルダーを作成してログを保存する
            string logDir = Path.Combine(Application.dataPath, "data");
            if (!Directory.Exists(logDir))
            {
                Directory.CreateDirectory(logDir);
            }

            log_result_path = Path.Combine(logDir, "log_result.jsonl");
        }
        else
        {
            // ビルド版では永続データフォルダーに保存する
            log_result_path = Path.Combine(Application.persistentDataPath, "log_result.jsonl");
        }
    }

    public void LogResult(int hp)
    {
        SampleLogData log = new SampleLogData
        {
            datetime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            hp_of_final = hp
        };

        // ゲーム中に記録した破壊済み敵の名前と、その時点のボールのHPを追加する
        log.hp_of_destroyed_per_block.AddRange(destroyedBlocks);

        if (Application.isEditor)
        {
            SaveLogToLocalJson(log);
        }
    }

    // ログをJSON Lines形式でローカルファイルに追記する。
    public void RecordEnemyDestroyed(string objectName)
    {
        destroyedBlocks.Add(new BlockDamageData
        {
            object_name = objectName,
            hp = ball != null ? ball.heart : 0
        });
    }

    private void SaveLogToLocalJson(SampleLogData logData)
    {
        try
        {
            string jsonString = JsonUtility.ToJson(logData);
            File.AppendAllText(log_result_path, jsonString + Environment.NewLine, Encoding.UTF8);
            Debug.Log($"ローカルログを保存しました: {log_result_path}");
        }
        catch (Exception e)
        {
            Debug.LogError($"ローカルログの保存に失敗しました: {e.Message}");
        }
    }

    private void Update()
    {
        if (DestroyAllEnemies() && SceneManager.GetActiveScene().name == "10_scene1-1")
        {
            Debug.Log("ゲームクリア");
            LogResult(ball.heart);
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
