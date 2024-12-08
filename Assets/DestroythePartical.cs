using UnityEngine;

public class DestroythePartical : MonoBehaviour
{
    public GameObject particleEffectPrefab; 
    public Camera_Smooth  cameraShake;

    public bool playerDie;

    public static DestroythePartical instance;
    public void Start()
    {
        instance = this;
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera_Smooth>();
    }
    void Update()
    {
        Invoke("DeleteWithParticle", 2f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerDie = true;
        }
    }
    void DeleteWithParticle()
    {
        if (particleEffectPrefab != null)
        {
           
            GameObject particleEffect = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);

           
            ParticleSystem ps = particleEffect.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                Destroy(particleEffect, ps.main.duration);
            }
            else
            {
                Debug.Log("Particle no.");
            }

            
            if (cameraShake != null)
            {

                Camera_Smooth.instance.ShakeCamera();

            }
        }

       
        Destroy(gameObject);
    }
}
