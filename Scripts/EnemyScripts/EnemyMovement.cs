using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _distanceDetector = 5f;
    private GameObject player;

    AdaptiveMusic_Crossfade AdaptativeMusic_Crossfade;

    private void Awake()
    {
        AdaptativeMusic_Crossfade = FindObjectOfType<AdaptiveMusic_Crossfade>();
        if (AdaptativeMusic_Crossfade == null)
        {
            Debug.LogError("AdaptiveMusic_Crossfade not found in the scene!");
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < _distanceDetector)
        {
            MoveTowardsPlayer();
            AdaptativeMusic_Crossfade.SwitchToTrackB();
        }

        else
        {
            AdaptativeMusic_Crossfade.SwitchToTrackA();
        }
    }
}

