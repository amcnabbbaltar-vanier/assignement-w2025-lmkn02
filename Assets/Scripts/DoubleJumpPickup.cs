using UnityEngine;
using System.Collections;

public class DoubleJumpPickup : MonoBehaviour
{
    [Header("Hover & Rotate Settings")]
    [SerializeField] private float rotateSpeed = 60f;        // Degrees per second
    [SerializeField] private float hoverAmplitude = 0.2f;    // How high/low it bobs
    [SerializeField] private float hoverFrequency = 2f;      // Bobbing speed

    private Vector3 startPos;
    private float hoverTimer;

    [Header("Power-Up Duration (seconds)")]
    [SerializeField] private float powerUpDuration = 30f;  // 30s of double jump

    [Header("Particle Effect on Pickup")]
    [SerializeField] private GameObject collectionEffect;   // Particle prefab

    private void Start()
    {
        startPos = transform.position;
        hoverTimer = 0f;
    }

    private void Update()
    {
        HoverAndRotate();
    }

    /// <summary>
    /// Makes the sphere rotate & hover up/down using a sine wave.
    /// </summary>
    private void HoverAndRotate()
    {
        // 1) Rotate around Y-axis
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime), Space.World);

        // 2) Hover using sine wave
        hoverTimer += Time.deltaTime * hoverFrequency;
        float newY = startPos.y + Mathf.Sin(hoverTimer) * hoverAmplitude;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Only react if it's the Player
        if (other.CompareTag("Player"))
        {
            // Attempt to enable double jump on the Player
            CharacterMovement movement = other.GetComponent<CharacterMovement>();
            if (movement != null)
            {
                // Start a coroutine to enable double jump for 'powerUpDuration' seconds
                StartCoroutine(EnableDoubleJumpTemporarily(movement));
            }

            // Trigger a particle effect at pickup location
            if (collectionEffect != null)
            {
                // Instantiate the effect
                GameObject effectInstance = Instantiate(collectionEffect, transform.position, Quaternion.identity);
                // Destroy the particle effect after 2 seconds
                Destroy(effectInstance, 2f);
            }

            // Destroy this pickup so it disappears right away
            Destroy(gameObject);
        }
    }

    private IEnumerator EnableDoubleJumpTemporarily(CharacterMovement movement)
    {
        // Enable double jump
        movement.canDoubleJump = true;
        Debug.Log("Double jump ENABLED for " + powerUpDuration + " seconds!");

        // Wait for the specified duration
        yield return new WaitForSeconds(powerUpDuration);

        // Disable double jump again
        movement.canDoubleJump = false;
        Debug.Log("Double jump DISABLED after " + powerUpDuration + " seconds.");
    }
}
