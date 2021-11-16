using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temple7Generator : MonoBehaviour
{
    public GameObject temple7;
    float span = 2.5f;
    float delta = 0;
    int times = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if(this.delta > this.span)
        {
            this.delta = 0;
            times = Random.Range(0, 3);

            for(int i = 0; i <= times; ++i)
            {
                GameObject go = Instantiate(temple7) as GameObject;
                int pz = Random.Range(90, 101);
                int px = Random.Range(-6, 4);
                go.transform.position = new Vector3(px, -9.2f, pz);
            }
        }
    }
}
