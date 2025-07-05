using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRatAttack : Enemy, IDamagable
{
    private bool canHit = true;

    [SerializeField] private float attackCooldown = 1f; // Tiempo Cooldown del ataque
    [SerializeField] private int ratCoins = 20; // Cantidad de monedas que suelta el enemigo al morir
    [SerializeField] private Transform playerTransform; 
    [SerializeField] private float rotationSpeed = 3f;

    SfxManager SfxManager;

    private void Awake()
    {
        SfxManager = FindObjectOfType<SfxManager>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (playerTransform != null)
        {
            LookAtPlayer();
        }
    }
    private void LookAtPlayer()
    {
        // Calcula la dirección hacia el jugador
        Vector3 direction = (playerTransform.position - transform.position).normalized;

        // Calcula la rotación hacia el jugador
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        // Aplica la rotación suavemente
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

private IEnumerator AttackCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(attackCooldown);
        canHit = true;
    }
    public int EnemyDamage
    {
        get { return EnDamage; }
        set { EnDamage = value; }
    }

    public int EnemyHealth
    {
        get { return EnHealth; }
        set { EnHealth = value; }
    }
    //public bool IsAttacking => isAttacking;

    public void OnCollisionEnter(Collision collision)
    {
        if (canHit)
        {
            IDamagable target = collision.gameObject.GetComponent<IDamagable>();
            PlayerAttack playerAttack = collision.gameObject.GetComponent<PlayerAttack>();


            if (target != null)
            {
                if (collision.gameObject.CompareTag("Player") && !playerAttack.PlayerIsAttacking) // Verifica si el jugador no está atacando
                {
                    target.TakeDamage(enDamage); // el jugador toma daño si no esta atacando
                    Debug.Log("Player tomo daño.");
                }
                else
                {

                }

                
                StartCoroutine(AttackCooldown());
            }
        else
            {
            }
        }
    }
    void IDamagable.TakeDamage(int damage)
    {
        enHealth -= damage;
        Debug.Log("Enemy daño " + damage);
        SfxManager.PlaySFX(SfxManager.ratHurt); // Reproduce el sonido de daño al enemigo
        if (enHealth <= 0)
        {
            CoinManager.instance.AddCoins(ratCoins); // Aumenta el puntaje al eliminar el enemigo
            SfxManager.PlaySFX(SfxManager.coin);
            Destroy(gameObject);
            
            Debug.Log("Enemy destruido");
        }
    }
    private void OnDestroy()
    {
    }
}
    

