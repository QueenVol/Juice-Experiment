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

        private bool isHitFlash;
        [SerializeField] private float hitFlashDuration = 0.1f;

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
                StartCoroutine(HitFlashRoutine());
            }
        }

        private IEnumerator HitFlashRoutine()
        {
            if (isHitFlash) yield break;
            isHitFlash = true;

            Color originalColor = sr.color;

            sr.color = new Color(1.5f, 0.3f, 0.3f, originalColor.a);

            yield return new WaitForSeconds(hitFlashDuration);

            sr.color = originalColor;

            isHitFlash = false;
        }

        private IEnumerator DieRoutine()
        {
            isKnockback = true;
            rb.velocity = Vector2.zero;

            if (deathSFX != null)
                deathSFX.Play();

            float timer = 0f;
            Color originalColor = sr.color;

            while (timer < deathFlashDuration)
            {
                sr.color = originalColor * 1.5f;
                sr.enabled = true;

                yield return new WaitForSeconds(flashInterval * 0.5f);

                sr.color = originalColor;
                sr.enabled = false;

                yield return new WaitForSeconds(flashInterval * 0.5f);

                timer += flashInterval;
            }

            sr.enabled = true;
            sr.color = originalColor;

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
