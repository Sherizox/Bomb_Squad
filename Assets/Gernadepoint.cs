using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePoint : MonoBehaviour
{
   public bool GernadeforEnemy = false;

    public static GrenadePoint instance;

   public int gernade = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Player collision detected");

           // Gernade = true;
            Destroy(this.gameObject);
        }
        
        
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!GernadeforEnemy) 
            {
                GernadeforEnemy = true;
             

               // print("Enemy collision detected");

            }
            print("Enemy collision detected");
           // Destroy(this.gameObject);
        }
    }
}
