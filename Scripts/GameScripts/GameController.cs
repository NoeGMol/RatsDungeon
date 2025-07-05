using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CoinManager coinManager;
    public CoinDisplay coinDisplay;

    [SerializeField] TextMeshProUGUI textLives;
    [SerializeField] TextMeshProUGUI textHealth;
    [SerializeField] GameObject player;

    Vector3 playerRespawn;
    [SerializeField] int remaningLives = 3;
    [SerializeField] private float respawnTime = 3f; // tiempo de respawn

    Player playerUI;
    PlayerController playerController;
    Animator anim;

    int initialPlayerHeaLth;
    int tempHealth;
    int tempLives;

    float respawnTimer;

    private void Awake()
    {
        anim = player.GetComponent<Animator>();
        playerUI = player.GetComponent<Player>();
        playerController = GetComponent<PlayerController>(); // tomo el spawn point
    }

    void Start()
    {

        playerRespawn = playerController.SpawnPoint; // asigno el spawn point al respawn del jugador
        tempHealth = playerUI.PlayerHealth;
        Debug.Log("Player Health: " + tempHealth);
        initialPlayerHeaLth = playerUI.PlayerHealth;
        Debug.Log("Initial Player Health: " + initialPlayerHeaLth);

        tempLives = remaningLives;

        textHealth.text = playerUI.PlayerHealth.ToString();
        textLives.text = remaningLives.ToString();
        Debug.Log("Vidas restantes : " + remaningLives);



        coinManager.RegisterObserver(coinDisplay);

    }

    void Update()
    {
        if (tempHealth != playerUI.PlayerHealth)
        {
            UpdateHealthUI();
        }

        if (tempLives != playerUI.PlayerHealth)
        {
            UpdateLivesUI();
        }

        if (!playerUI._IsAlive)
        {
            Debug.Log("RespawnTimer: " + respawnTimer);

            if (remaningLives < 1)
            {
                GameIsOver();
            }

            respawnTimer += Time.deltaTime;

            if (respawnTimer >= respawnTime)
            {
                Death();
                UpdateLivesUI();
                UpdateHealthUI();
            }
        }
    }

    void UpdateHealthUI()
    {
        textHealth.text = playerUI.PlayerHealth.ToString();
        tempHealth = playerUI.PlayerHealth;
    }

    void UpdateLivesUI()
    {
        textLives.text = remaningLives.ToString();
    }

    void Death()
    {
        remaningLives--;
        respawnTimer = 0f; // tiempo de respawn
        player.transform.position = playerRespawn; // respawnear al jugador
        playerUI.PlayerHealth = initialPlayerHeaLth;
        playerUI._IsAlive = true; // revivir al jugador
        anim.SetBool("IsDeadB", false);
        UpdateHealthUI();
    }
    void GameIsOver()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Defeat");// cargar escena
        Debug.Log("Game Over");
    }
}

