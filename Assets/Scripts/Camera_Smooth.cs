using UnityEngine;

public class Camera_Smooth : MonoBehaviour
{
    public static Camera_Smooth instance;

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float shakeDuration = 1f;
    public float shakeAmount = 0.15f; 
    public float decreaseFactor = 1.0f; 

    private Vector3 originalPosition;
    private float currentShakeAmount = 0f;

    private void Start()
    {
        instance=this ;
    }
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            if (shakeDuration > 0)
            {
               
                Vector3 randomShake = Random.insideUnitSphere * shakeAmount * currentShakeAmount;
                desiredPosition += randomShake;

              
                shakeDuration -= Time.deltaTime * decreaseFactor;
                currentShakeAmount = shakeDuration / shakeDuration; 
            }
            else
            {
                shakeDuration = 0f;
            }

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void ShakeCamera()
    {
        shakeDuration = 1f;
    }
}
