using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCorpse : MonoBehaviour
{
    UI_Time gameCheck;

    // Start is called before the first frame update
    void Start()
    {
        gameCheck = GameObject.Find("GameManager").GetComponent<UI_Time>();
        //敵の鳴き声
        if (!gameCheck.gameOver)
            GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //死んだ敵のスケールを変える
        if (transform.localScale.magnitude > 1)
            transform.localScale = Vector3.Min(transform.localScale, transform.localScale - new Vector3(0.065f, 0.065f, 0.065f));
        else transform.localScale = Vector3.zero;

    }
}
