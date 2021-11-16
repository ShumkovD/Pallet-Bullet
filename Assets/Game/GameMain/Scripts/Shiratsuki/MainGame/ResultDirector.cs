using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDirector : MonoBehaviour
{
    UI_Time uiTime;



    //リザルト画面
    public GameObject Panel;
    public Text resultSrc;
    public Text totalResult;

    //仮のスコア表示用
    int enemyScore = 100;      //敵の持つポイント
    int timeScore = 10;        //残り秒数が持つポイント
    int criticalBonus = 50;    //色システムのボーナス点数
    float timeCnt = 3.0f;      //ゲームオーバーになってからリザルト表示までの猶予時間
    int totalScore;            //合計点数
    float delta = 0f;

    [Header("Changed In Another Code")]
    public int eliminated = 0;      //敵を倒した数 - EnemyDeath.cs
    public int remainingTime = 0;            //残り時間（小数点以下切り捨て - UI_Time.cs
    public int criticalShots = 0;     //色システムの発動回数


    // Start is called before the first frame update
    void Awake()
    {
        uiTime = GetComponent<UI_Time>();
        //リザルト画面
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (uiTime.gameOver)
         Result();

    }

    //リザルト画面の表示
    void Result()
    {
        timeCnt -= Time.deltaTime;
        Debug.Log(timeCnt);
          if (timeCnt < 0)
          {
              timeCnt = 0;
              delta += Time.deltaTime;
              Panel.SetActive(true);
              Output(); 
        }
    }

    //スコアの計算
    int ScoreCalc()
    {
        int enemyTotal = eliminated * enemyScore;
        int timeTotal = remainingTime * timeScore;
        int bonusTotal = criticalShots * criticalBonus;

        totalScore = enemyTotal + timeTotal + bonusTotal;

        return totalScore;
    }

    //出力
    void Output()
    {
        if (delta > 1)
        {
            resultSrc.text = Source(0, 0);
        }
        if (delta > 2)
        {
            resultSrc.text += Source(0, 1);
        }

        if (delta > 3)
        {
            resultSrc.text += Source(1, 0);
        }

        if (delta > 4)
        {
            resultSrc.text += Source(1, 1);
        }

        if (delta > 5)
        {
            resultSrc.text += Source(2, 0);
        }

        if (delta > 6)
        {
            resultSrc.text += Source(2, 1);
        }

        if (delta > 7)
        {
            totalResult.text = Source(3, 0);
        }

        if (delta > 8)
        {
            totalResult.text += Source(3, 1);
        }
        
        Debug.Log(delta);
    }

    //リザルトの内訳
    string Source(int v1,int v2)
    {
        string[,] src = new string[,]
        {
            {"倒した敵" + "     ", eliminated.ToString() + " × " + enemyScore.ToString() + "点" + "\n\n"},
            {"残り時間" + "     ", remainingTime.ToString() + " × " + timeScore.ToString() + "点" + "\n\n"},
            {"ボーナス" + "       ", criticalShots.ToString() + " × " + criticalBonus.ToString() + "点"},
            {"合計" + "     ", ScoreCalc().ToString() + "点"}
        };

        return src[v1, v2];

    }

}
