using System;
using System.Collections.Generic;
using UnityEngine;

public static class EnvLoader
{
    private static Dictionary<string, string> _envVars;

    /// <summary>
    /// Resources/env.txt を読み込んでメモリに保持する
    /// </summary>
    public static void Load()
    {
        if (_envVars != null) return; // 既に読み込み済みならスキップ

        _envVars = new Dictionary<string, string>();

        // Assets/Resources/env.txt を読み込む（拡張子 .txt は省略）
        TextAsset envFile = Resources.Load<TextAsset>("env");
        if (envFile == null)
        {
            Debug.LogWarning("Resources/env.txt が見つかりません。");
            return;
        }

        // 行ごとに分割して解析
        string[] lines = envFile.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        foreach (string line in lines)
        {
            string trimmed = line.Trim();

            // 空行やコメント（#で始まる行）はスキップ
            if (string.IsNullOrEmpty(trimmed) || trimmed.StartsWith("#"))
                continue;

            int equalIndex = trimmed.IndexOf('=');
            if (equalIndex > 0)
            {
                string key = trimmed.Substring(0, equalIndex).Trim();
                string value = trimmed.Substring(equalIndex + 1).Trim();

                // ダブルクォーテーション等で囲まれている場合の除去処理
                if ((value.StartsWith("\"") && value.EndsWith("\"")) ||
                    (value.StartsWith("'") && value.EndsWith("'")))
                {
                    value = value.Substring(1, value.Length - 2);
                }

                _envVars[key] = value;
            }
        }
    }

    /// <summary>
    /// キーに対応する値を取得する
    /// </summary>
    public static string Get(string key, string defaultValue = "")
    {
        Load();
        if (_envVars.TryGetValue(key, out string value))
        {
            return value;
        }
        return defaultValue;
    }
}