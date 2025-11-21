using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.EnemyBehavior
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        [SerializeField] private int maxHealth = 5;
        private int currentHealth;

        [SerializeField] private AudioSource hitSFX;
        [SerializeField] private AudioSource deathSFX;

        [SerializeField] private float knockbackForce = 5f;
        [SerializeField] private float knockbackDuration = 0.1f;

        [SerializeField] private float deathFlashDuration = 0.4f;
        [SerializeField] private float flashInterval = 0.1f;

        private bool isKnockback;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage, Vector2 knockDir)
        {
            currentHealth -= damage;
            Knockback(knockDir);

            if (currentHealth <= 0)
            {
                StartCoroutine(DieRoutine());
            }
            else
            {
                if (hitSFX != null)
                    hitSFX.Play();
            }
        }

        private IEnumerator DieRoutine()
        {
            isKnockback = true;
            rb.velocity = Vector2.zero;

            if (deathSFX != null)
                deathSFX.Play();

            float timer = 0f;
            while (timer < deathFlashDuration)
            {
                sr.enabled = !sr.enabled;
                timer += flashInterval;
                yield return new WaitForSeconds(flashInterval);
            }

            sr.enabled = true;

            Destroy(gameObject);
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
