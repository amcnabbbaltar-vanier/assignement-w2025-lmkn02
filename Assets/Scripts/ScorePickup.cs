using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [Header("Hover & Rotate Settings")]
    [SerializeField] private float rotateSpeed = 60f;
    [SerializeField] private float hoverAmplitude = 0.2f;
    [SerializeField] private float hoverFrequency = 2f;

    private Vector3 startPos;
    private float hoverTimer;

    [Header("Score Awarded")]
    [SerializeField] private int scoreAmount = 50; 

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
         
            GameManager.instance.AddScore(scoreAmount);

           
            if (collectionEffect != null)
            {
                GameObject fx = Instantiate(collectionEffect, transform.position, Quaternion.identity);
                Destroy(fx, 2f);
            }

            
            Destroy(gameObject);
        }
    }
}
