using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }

    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemyObject in enemyObjects)
        {
            Enemy enemy = enemyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemies.Add(enemy);
            }
        }
    }

    private void Update()
    {
        CheckEnemies();
    }

    void CheckEnemies()
    {
        if (enemies.Count == 0)  
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
