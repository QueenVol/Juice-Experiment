using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
