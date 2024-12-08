//using UnityEngine;

//public class CameraShake : MonoBehaviour
//{
//    public float shakeDuration = 0.5f;
//    public float shakeAmount = 0.7f; 

//    private Vector3 originalPosition; 
//    private float shakeTimer = 0f; 

//    void Update()
//    {
        
//        if (shakeTimer > 0)
//        {
//            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
//            shakeTimer -= Time.deltaTime;
//        }
//        else
//        {
//            shakeTimer = 0f;
//            transform.localPosition = originalPosition;
//        }
//    }

    
//    public void ShakeCamera()
//    {
//        originalPosition = transform.localPosition;
//        shakeTimer = shakeDuration;
//    }
//}
