using UnityEngine;
using System.Collections;

namespace TopDown.CameraControl
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private float magnitude = 0.15f;
        [SerializeField] private float noiseFrequency = 20f;

        private CameraController camController;
        private Coroutine shakeRoutine;
        private Vector3 shakeOffset;

        private void Awake()
        {
            camController = GetComponent<CameraController>();
        }

        private void LateUpdate()
        {
            if (camController == null) return;

            Vector3 finalPos = camController.BasePosition + shakeOffset;
            finalPos.z = -10f;
            transform.position = finalPos;
        }

        public void Shake(float customDuration = -1f, float customMagnitude = -1f)
        {
            if (shakeRoutine != null)
                StopCoroutine(shakeRoutine);

            shakeRoutine = StartCoroutine(ShakeRoutine(
                customDuration < 0 ? duration : customDuration,
                customMagnitude < 0 ? magnitude : customMagnitude
            ));
        }

        private IEnumerator ShakeRoutine(float dur, float mag)
        {
            float timer = 0f;
            float seed = Random.value * 100f;

            while (timer < dur)
            {
                float progress = timer / dur;
                float strength = mag * (1f - progress);

                float x = (Mathf.PerlinNoise(seed, timer * noiseFrequency) - 0.5f) * 2f;
                float y = (Mathf.PerlinNoise(seed + 100f, timer * noiseFrequency) - 0.5f) * 2f;

                shakeOffset = new Vector3(x, y, 0) * strength;

                timer += Time.deltaTime;
                yield return null;
            }

            shakeOffset = Vector3.zero;
        }
    }
}