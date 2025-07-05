using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character, IDamagable
{
    Animator _anim;
    private int playerHealth = 100;
    private bool _isAlive;
    public bool _IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }
    public int PlayerHealth 
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    SfxManager SfxManager;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _isAlive = true;
        SfxManager = FindObjectOfType<SfxManager>();

    }

    void IDamagable.TakeDamage(int damage)
    {
        playerHealth -= damage;
        SfxManager.PlaySFX(SfxManager.hurt); // Reproduce el sonido del ataque

        if (playerHealth <= 0 && _isAlive)
        {
            Death();

        }
    }    
    void Death()
    {
        _anim.SetBool("IsDeadB", true);
        Debug.Log("Player is dead");
        _isAlive = false;
    }
}
    

