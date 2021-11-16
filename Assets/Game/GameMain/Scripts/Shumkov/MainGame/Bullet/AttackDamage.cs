using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamage : MonoBehaviour
{
    //’e‚Ìí—Ş
    [Header("Bullet Properties")]
    public ColorTypes.ColorType bulletColor;
    public float damage;
    public bool isPlayer;
    
    //Õ“Ë‚Ü‚Å‚Ì‹——£
    public Vector3 moveVector;
    public GameObject target;



    private void Update()
    {
 
        if(isPlayer)
        {
            if (Vector3.Distance(transform.position, moveVector) <= 1.1f)
            {
                if (target != null)
                {
                    transform.position = target.transform.position;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
