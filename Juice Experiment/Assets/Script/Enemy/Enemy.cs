using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.EnemyBehavior
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private int maxHealth = 5;
        private int currentHealth;

        [SerializeField] private float knockbackForce = 5f;
        [SerializeField] private float knockbackDuration = 0.1f;

        private bool isKnockback;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, Vector2 knockDir)
        {
            currentHealth -= damage;
            Knockback(knockDir);

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void Knockback(Vector2 direction)
        {
            if (isKnockback) return;
            isKnockback = true;

            rb.velocity = direction.normalized * knockbackForce;

            Invoke(nameof(StopKnockback), knockbackDuration);
        }

        private void StopKnockback()
        {
            rb.velocity = Vector2.zero;
            isKnockback = false;
        }
    }
}   
