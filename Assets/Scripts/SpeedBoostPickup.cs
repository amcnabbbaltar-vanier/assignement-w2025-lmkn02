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

    [Header("Particle Effect on Pickup")]
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
                
                movement.StartSpeedBoost(speedMultiplierValue, 5f);
            }

          
            if (collectionEffect != null)
            {
                GameObject fx = Instantiate(collectionEffect, transform.position, Quaternion.identity);
                Destroy(fx, 2f); 
            }

          
            Destroy(gameObject);
        }
    }

   
}
