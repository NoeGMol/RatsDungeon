using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int _damage = 10; // Daño del arma base
        private bool canHit = true; 
        public void OnCollisionEnter(Collision collision)
        {
            if (canHit)
            {
                IDamagable target = collision.gameObject.GetComponent<IDamagable>();

                if (target != null)
                {
                    target.TakeDamage(_damage);
                    canHit = false;
                     // Aca poner cooldown
                }
            }
        }
    }
}