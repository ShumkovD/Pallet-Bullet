using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject particle;

    public float powerUpCoef;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            other.GetComponent<PlayerShooting>().attackDamage *= powerUpCoef;
            other.GetComponent<PlayerShooting>().poweredUp = true;
            Destroy(gameObject);
        }
    }
}
