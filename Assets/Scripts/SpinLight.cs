using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Potentially refactor this to be a reusable RotateOverTime script?
public class SpinLight : MonoBehaviour
{
    private float speed;

    public float minSpeedLightSpeed = -45f;
    public float maxSpeedLightSpeed = 45f;

    public float muffinPulseAmount = 0.2f;
    public float muffinPulseSpeed = 1f;

    void Start()
    {
        // Generate some random speeds for the spinlights
        speed = Random.Range(minSpeedLightSpeed, maxSpeedLightSpeed);
    }

    void Update()
    {
        // Rotate the spin light over time
        transform.Rotate(0f, 0f, speed * Time.deltaTime);

        // Pulse the scale of the spin light
        transform.localScale = Vector3.one + (Vector3.one * muffinPulseAmount * Mathf.Sin(Time.time * muffinPulseSpeed * 0.267f * speed));
    }
}
