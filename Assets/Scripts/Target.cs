using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    public ParticleSystem destructionEffect;
    public float destrutionTime = 0;
    

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            if( destructionEffect != null)
            { 
                Instantiate(destructionEffect, transform.position, transform.rotation);
            }
            Destroy(gameObject, destrutionTime);
            
        }
    }

    
}
