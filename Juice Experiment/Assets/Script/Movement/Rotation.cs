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
            if (!rotateTarget) return;

            float angle = Mathf.Atan2(target.y - rotateTarget.position.y, target.x - rotateTarget.position.x) * Mathf.Rad2Deg;

            rotateTarget.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
