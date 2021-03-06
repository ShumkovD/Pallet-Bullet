using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    //GĚíŢ
    [Header("Enemy Color Type")]
    [SerializeField] ColorTypes.ColorType colorType;
    //GĚHP
    [Header("Enemy Health")]
    public float hp;
    

    public ParticleSystem[] damageParticles;

    public AudioSource damageGot;

    //OËśÖW
    UI_Time UI_Time;
    SpawnScript spawnLogic;
    ResultDirector UIresults;

    private void Start()
    {
        damageGot = GetComponent<AudioSource>();
        UI_Time = GameObject.Find("GameManager").GetComponent<UI_Time>();
        UIresults = GameObject.Find("GameManager").GetComponent<ResultDirector>();
        spawnLogic = GameObject.Find("GameManager").GetComponent<SpawnScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BulletParticle bulletParticle = other.GetComponent<BulletParticle>();
            AttackDamage bulletAttack = other.GetComponent<AttackDamage>();
            damageGot.Play();
            //Ę[eĆGĚíŢŞ¤
            if (colorType == bulletAttack.bulletColor)
            {

                bulletParticle.damageParticle = damageParticles[0];
                hp -= bulletAttack.damage * 2;

            }
            else
            {
                //íČ˘
                if (bulletAttack.bulletColor != ColorTypes.ColorType.Super)
                {
                    bulletParticle.damageParticle = damageParticles[1];
                    hp -= bulletAttack.damage;
                }
                else
                //p[Abv
                {
                    bulletParticle.damageParticle = damageParticles[2];
                    hp -= bulletAttack.damage * 3;
                    UIresults.criticalShots++;
                }
            }
            //eĚjü
            Destroy(other.gameObject);
            //GĚjü
            if (hp<=0)
            {
                spawnLogic.enemyWasKilled = true;
                spawnLogic.periodTimer = 0;
                UI_Time.currentTime--;
                UIresults.eliminated++;
                Destroy(this.gameObject);
            }
        }
    }

}