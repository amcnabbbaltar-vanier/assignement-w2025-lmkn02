using UnityEngine;

public class SpinInPlace : MonoBehaviour
{
    [Tooltip("How fast the trap rotates in degrees/second.")]
    public float spinSpeed = 60f;

    [Tooltip("Which axis to spin around (e.g., Vector3.up).")]
    public Vector3 spinAxis = Vector3.up;

    void Update()
    {
        
        transform.Rotate(spinAxis, spinSpeed * Time.deltaTime, Space.World);
    }
}
