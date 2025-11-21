using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BulletDestroy : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.3f;
    [SerializeField] private AudioSource explodeSFX;
    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        explodeSFX.Play();
        Destroy(sprite, 0.3f);
        Destroy(gameObject, explodeSFX.clip.length);
    }
}
