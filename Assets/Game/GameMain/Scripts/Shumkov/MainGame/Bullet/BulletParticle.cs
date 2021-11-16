using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour
{
    [Header("Damage Particles")]
    public ParticleSystem damageParticle;
    bool isQuitting;
    public bool normalCollision = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if(!isQuitting&&!normalCollision)
            Instantiate(damageParticle, transform.position,transform.rotation);
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            normalCollision = true;
            Destroy(this.gameObject);
        }
    }

}
