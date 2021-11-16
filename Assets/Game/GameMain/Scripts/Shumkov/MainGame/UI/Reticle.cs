using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reticle : MonoBehaviour
{


    //UIスプライトの種類
    public Sprite[] reticle;
    public Sprite[] indicator;



    //破棄するオブジェクト
    public Image reticleImage;
    public Image indicatorImage;
    public GameObject hpBar;
    public GameObject waveCnt;

    //外部依存関係
    UI_Time uiTime;
    public PlayerShooting playerShoot;

    private void Start()
    {
        uiTime = GameObject.Find("GameManager").GetComponent<UI_Time>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!uiTime.gameOver)
        {
            //撃っている弾によって
            ColorTypes.ColorType color = playerShoot.shootingBullet.GetComponent<AttackDamage>().bulletColor;
            //UIの色が違う
            //赤
            if (color == ColorTypes.ColorType.Red)
            {
                indicatorImage.sprite = indicator[0];
                reticleImage.sprite = reticle[0];
            }
            //青
            if (color == ColorTypes.ColorType.Blue)
            {
                indicatorImage.sprite = indicator[1];
                reticleImage.sprite = reticle[1];
            }
            //緑
            if (color == ColorTypes.ColorType.Green)
            {
                indicatorImage.sprite = indicator[2];
                reticleImage.sprite = reticle[2];
            }
            //虹
            if (color == ColorTypes.ColorType.Super)
            {
                indicatorImage.sprite = indicator[3];
                reticleImage.sprite = reticle[3];
            }
        }
        else
        {
            //破棄
            if(waveCnt != null)
            {
                Destroy(waveCnt);
            }
            if(hpBar != null)
            {
                Destroy(hpBar);
            }
            if (reticleImage != null)
            {
                Destroy(reticleImage.gameObject);
                reticleImage = null;
            }
            if (indicatorImage != null)
            {
                Destroy(indicatorImage.gameObject);
                indicatorImage = null;
            }
        }
    }
}
