using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimationSpawn: MonoBehaviour
{
    public GameObject deadEnemy;
    bool isQuitting;
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        if(!isQuitting)
            Instantiate(deadEnemy, transform.position, transform.rotation);
    }
}
