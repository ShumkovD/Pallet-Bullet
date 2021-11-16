using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [Header("Particles On Hit")]
    public ParticleSystem particleOnHit;
    [Header("Game Manager Time")]
    public UI_Time UITime;

    public AudioSource damageAudio;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            damageAudio.Play();
            float damage = other.GetComponent<AttackDamage>().damage;
            particleOnHit.Play();
            UITime.currentTime += damage;
        }
    }
}
