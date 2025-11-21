using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown.Movement
{
    public class PlayerRotation : Rotation
    {
        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private SpriteRenderer weaponSprite;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private Transform weaponVFX;

        private Camera cam;

        protected override void Awake()
        {
            base.Awake();
            cam = Camera.main;
        }

        private void OnLook(InputValue value)
        {
            Vector2 mouseScreen = value.Get<Vector2>();
            Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
            mouseWorld.z = 0f;

            LookAt(mouseWorld);

            HandleFlip(mouseWorld);
        }

        private void HandleFlip(Vector3 mouseWorld)
        {
            bool facingRight = mouseWorld.x >= transform.position.x;

            if (playerSprite != null)
                playerSprite.flipX = !facingRight;

            if (weaponSprite != null)
                weaponSprite.flipY = !facingRight;

            if (shootPoint != null)
                shootPoint.localPosition = facingRight ? new Vector2(shootPoint.localPosition.x, 0.02f) : new Vector2(shootPoint.localPosition.x, -0.02f);

            if (weaponVFX != null)
                weaponVFX.localPosition = facingRight ? new Vector2(weaponVFX.localPosition.x, 0.02f) : new Vector2(weaponVFX.localPosition.x, -0.02f);
        }
    }
}
