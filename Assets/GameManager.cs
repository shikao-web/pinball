using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float GAME_DIFFICULTY = 50.0f;
    public GameObject[] enemies;
    public ball ball;

    private void Update()
    {
        if (DestroyAllEnemies() && SceneManager.GetActiveScene().name == "10_scene1-1")
        {
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
        SceneManager.LoadScene("20_GameOver");
    }
}
