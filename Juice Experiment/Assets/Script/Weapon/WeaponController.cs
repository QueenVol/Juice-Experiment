using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Shooting
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private float cooldown = 0.25f;
        private float cooldownTimer;
        private bool isShooting;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Animator bulletVFX;

        private void Update()
        {
            cooldownTimer += Time.deltaTime;

            if (isShooting && cooldownTimer >= cooldown)
            {
                Shoot();
            }
        }

        private void OnShoot(InputValue value)
        {
            if (value.isPressed)
            {
                Shoot();
                isShooting = true;
            }
            else
            {
                isShooting = false;
            }
        }

        private void Shoot()
        {
            cooldownTimer = 0;

            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            bullet.GetComponent<Bullet>().ShootBullet(shootPoint);

            bulletVFX.SetTrigger("shoot");
        }
    }
}
