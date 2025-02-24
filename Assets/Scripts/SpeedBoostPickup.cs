using UnityEngine;
using System.Collections;

public class SpeedBoostPickup : MonoBehaviour
{
    [Header("Hover & Rotate Settings")]
    [SerializeField] private float rotateSpeed = 60f;
    [SerializeField] private float hoverAmplitude = 0.2f;
    [SerializeField] private float hoverFrequency = 2f;

    private Vector3 startPos;
    private float hoverTimer;

    [Header("Speed Multiplier")]
    [SerializeField] private float speedMultiplierValue = 2f; 

    [Header("Particle Effect on Pickup (Optional)")]
    [SerializeField] private GameObject collectionEffect;

    private void Start()
    {
       
        startPos = transform.position;
        hoverTimer = 0f;
    }

    private void Update()
    {
        HoverAndRotate();
    }

   
    private void HoverAndRotate()
    {
       
        transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime), Space.World);

      
        hoverTimer += Time.deltaTime * hoverFrequency;
        float newY = startPos.y + Mathf.Sin(hoverTimer) * hoverAmplitude;
        Vector3 pos = transform.position;
        pos.y = newY;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterMovement movement = other.GetComponent<CharacterMovement>();
            if (movement != null)
            {
                
                movement.StartSpeedBoost(2f, 5f);
            }

        
            Destroy(gameObject);
        }
    }

   
    private IEnumerator EnableSpeedBoostForFiveSeconds(CharacterMovement movement)
    {
        float originalMultiplier = movement.speedMultiplier;
        Debug.Log("BOOST START: from " + originalMultiplier + " to " + speedMultiplierValue);

        movement.speedMultiplier = speedMultiplierValue;

        yield return new WaitForSeconds(5f);

        movement.speedMultiplier = originalMultiplier;
        Debug.Log("BOOST END: Reverted to " + originalMultiplier);
    }

}
