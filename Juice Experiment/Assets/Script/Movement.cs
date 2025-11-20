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

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            rb.velocity = movementSpeed * currentInput * Time.fixedDeltaTime;
        }
    }
}