using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown.Movement
{
    public class Rotation : MonoBehaviour
    {
        [SerializeField] private Transform rotateTarget;

        protected virtual void Awake()
        {
            if (rotateTarget == null)
                rotateTarget = transform;
        }

        protected void LookAt(Vector3 target)
        {
            if (rotateTarget == null) return;

            float lookAngle = AngleBetweenTwoPoints(rotateTarget.position, target) + 180f;
            rotateTarget.eulerAngles = new Vector3(0, 0, lookAngle);
        }

        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}
