using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform t;

    // Desired duration of the shake effect
    private float shakeDuration = 0f, _shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0f, _shakeMagnitude = 0.4f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    public static CameraShake instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        t = Camera.main.transform;
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeMagnitude = 0f;
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float duration = float.NaN, float magnitude = float.NaN)
    {
        if (float.IsNaN(magnitude)) magnitude = _shakeMagnitude;
        if (float.IsNaN(duration)) duration = _shakeDuration;
        if (magnitude > shakeMagnitude)
        {
            shakeDuration = duration;
            shakeMagnitude = magnitude;
        }
    }
}
