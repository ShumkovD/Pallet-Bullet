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

    //スコア表示用
    struct Score
    {
        public int enemy;       //敵の持つポイント
        public int time;        //残り秒数が持つポイント
        public int critical;    //色システムのボーナス点数
        public int total;       //合計点数
        public int timeCnt;     //ゲームオーバーになってからリザルト表示までの猶予時間
        public int opTime;      //出力の間隔

        public void SetValue(int v1, int v2, int v3, int v4, int v5, int v6)
        {
            enemy = v1;
            time = v2;
            critical = v3;
            total = v4;
            timeCnt = v5;
            opTime = v6;
        }
    }

    Score score = new Score();
    bool isCalledOutput = false; //出力が呼ばれたかどうか

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

    void Start()
    {
        score.SetValue(100, 10, 50, 0, 3, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (uiTime.gameOver)
        {
            if (!isCalledOutput)
            {
                StartCoroutine("ResultOutput");
                isCalledOutput = true;
            }
        }

    }

    void ScoreInitialize(Score s_)
    {
        s_.enemy = 100;
        s_.time = 10;
        s_.critical = 50;
        s_.total = 0;
        s_.timeCnt = 3;
        s_.opTime = 1;
    }
    //出力
    IEnumerator ResultOutput()
    {
        yield return new WaitForSeconds(score.timeCnt);
        Panel.SetActive(true);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text = Source(0, 0);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text += Source(0, 1);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text += Source(1, 0);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text += Source(1, 1);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text += Source(2, 0);

        yield return new WaitForSeconds(score.opTime);
        resultSrc.text += Source(2, 1);

        yield return new WaitForSeconds(score.opTime);
        totalResult.text += Source(3, 0);

        yield return new WaitForSeconds(score.opTime);
        totalResult.text += Source(3, 1);
    }

    //スコアの計算
    int ScoreCalc()
    {
        int enemyTotal = eliminated * score.enemy;
        int timeTotal = remainingTime * score.time;
        int bonusTotal = criticalShots * score.critical;

        score.total = enemyTotal + timeTotal + bonusTotal;

        return score.total;
    }

    //リザルトの内訳
    string Source(int v1, int v2)
    {
        string[,] src = new string[,]
        {
            {"倒した敵" + "     ", eliminated.ToString() + " × " + score.enemy.ToString() + "点" + "\n\n"},
            {"残り時間" + "     ", remainingTime.ToString() + " × " + score.time.ToString() + "点" + "\n\n"},
            {"ボーナス" + "       ", criticalShots.ToString() + " × " + score.critical.ToString() + "点"},
            {"合計" + "     ", ScoreCalc().ToString() + "点"}
        };

        return src[v1, v2];

    }

}
