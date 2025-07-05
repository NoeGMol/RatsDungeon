using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int playerDamage = 10; // Daño del arma base
    [SerializeField] private float attackCooldown = 0.5f;
    private bool canHit = true;
    private bool playerIsAttacking = false;
    Animator _anim;

    SfxManager SfxManager;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        SfxManager = FindObjectOfType<SfxManager>();
    }
    private IEnumerator AttackCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(attackCooldown);
        canHit = true;
        playerIsAttacking = false;
    }
    public int PlayerDamage
    {
        get { return playerDamage; }
        set { playerDamage = value; }
    }

    public bool PlayerIsAttacking
    {
        get { return playerIsAttacking; }
        set { playerIsAttacking = value; }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!canHit || !collision.gameObject.CompareTag("Enemy"))
            return;
            {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();

            if (target != null && playerIsAttacking)
            {
                _anim.SetBool("IsPunch", true);
                target.TakeDamage(playerDamage);
                StartCoroutine(AttackCooldown());// Aca poner cooldown
            }
        }
    }
    public void Attack()
    {
        if (!playerIsAttacking)
        {
            playerIsAttacking = true; // Cambia el estado a true
            _anim.SetBool("IsPunch", true);
            SfxManager.PlaySFX(SfxManager.punch); // Reproduce el sonido del ataque
            StartCoroutine(ResetIsPunch()); // cooldown
        }
    }

    private IEnumerator ResetIsPunch()
    {
        yield return new WaitForSeconds(0.5f); // Duración de la animación de ataque
        _anim.SetBool("IsPunch", false); // Resetea el parámetro
        playerIsAttacking = false; // Cambia el estado a false
    }
}
