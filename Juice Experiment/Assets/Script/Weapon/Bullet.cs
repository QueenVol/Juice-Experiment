using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField] private float speed;
        [SerializeField] private float maxDistance;
        [SerializeField] private GameObject destroyVFX;

        private Vector3 startPos;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            startPos = transform.position;
            rb.velocity = Vector2.zero;
        }

        private void Update()
        {
            if (Vector3.Distance(startPos, transform.position) > maxDistance)
            {
                SpawnDestroyVFX();
                Destroy(gameObject);
            }
        }

        public void ShootBullet(Transform shootPoint)
        {
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;

            rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Wall"))
            {
                SpawnDestroyVFX();
                Destroy(gameObject);
            }
        }

        private void SpawnDestroyVFX()
        {
            if (destroyVFX == null) return;
            Instantiate(destroyVFX, transform.position, transform.rotation);
        }
    }
}
