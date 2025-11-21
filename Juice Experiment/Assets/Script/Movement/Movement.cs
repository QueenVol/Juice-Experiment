using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private Rigidbody2D rb;
        protected Vector3 currentInput;
        private Animator animator;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            rb.velocity = movementSpeed * currentInput;

            bool isMoving = currentInput.sqrMagnitude > 0.01f;
            animator.SetBool("isMoving", isMoving);
        }
    }
}