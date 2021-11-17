using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [Header("Damage Particles")]
    public ParticleSystem damageParticle;
    bool isQuitting;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if(!isQuitting)
            Instantiate(damageParticle, transform.position,transform.rotation);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 8)
            Destroy(this.gameObject);
    }

}
