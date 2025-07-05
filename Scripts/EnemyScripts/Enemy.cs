using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : Character
    {
        [SerializeField] protected int enHealth;
        [SerializeField] protected int enDamage;
        private int playDamage;
        protected bool isAttacking = false;

        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }
        public int EnDamage
        {
            get { return enDamage; }
            set { enDamage = value; }
        }
        public int EnHealth
        {
            get { return enHealth; }
            set { enHealth = value; }
        }

    }
}

